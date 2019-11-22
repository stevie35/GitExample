using DAL_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using static Ex3console.Module18TP1;
using DAL_Library.Entities;

namespace Module18TP1.Menus
{
    public class Menu
    {
        public void MainMenu()
        {
            int? subChoice = null;

            do
            {
                int? choice = MenuUtils.GetIntChoice(MenuUtils.BaseChoiceMenu(), 1, 2);
                switch (choice)
                {
                    case 1:
                        subChoice = MenuUtils.GetIntChoice(MenuUtils.CrudMenuFor("employee"), 1, 5);
                        switch (subChoice)
                        {
                            case 1:
                                using (var db = new EmployeeContext())
                                {
                                    Employee employee = MenuUtils.BuildEmployeeWithService();

                                    db.Employees.Add(employee);

                                    db.SaveChanges();
                                }
                                break;
                            case 2:
                                using (var db = new EmployeeContext())
                                {
                                    foreach (var item in db.Employees.Include(x => x.Department).ToList())
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                break;
                            case 3:
                                using (var db = new EmployeeContext())
                                {
                                    string firstname = MenuUtils.GetString("Firstname");
                                    string lastname = MenuUtils.GetString("Lastname");
                                    foreach (var item in db.Employees.Where(x => x.Firstname.Contains(firstname) && x.Lastname.Contains(lastname)).Include(x => x.Department).ToList())
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                break;
                            case 4:
                                int id = MenuUtils.GetIntChoice("Choose an id", 0, int.MaxValue).Value;
                                using (var db = new EmployeeContext())
                                {
                                    Employee employee = new Employee() { EmployeeId = id };

                                    db.Employees.Attach(employee);
                                    db.Employees.Remove(employee);

                                    db.SaveChanges();
                                }
                                break;
                            case 5:
                                int idUpdate = MenuUtils.GetIntChoice("Choose an id", 0, int.MaxValue).Value;
                                Employee empToUpdate = MenuUtils.BuildEmployee();
                                using (var db = new EmployeeContext())
                                {
                                    Employee empUpdate = db.Employees.Find(idUpdate);

                                    db.Employees.Attach(empUpdate);
                                    empUpdate.City = empToUpdate.City;
                                    empUpdate.DateOfBirth = empToUpdate.DateOfBirth;
                                    empUpdate.Firstname = empToUpdate.Firstname;
                                    empUpdate.Lastname = empToUpdate.Lastname;
                                    empUpdate.Salary = empToUpdate.Salary;
                                    empUpdate.Function = empToUpdate.Function;

                                    db.Entry(empUpdate).State = EntityState.Modified;

                                    db.SaveChanges();
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        subChoice = MenuUtils.GetIntChoice(MenuUtils.CrudMenuFor("service"), 1, 5);
                        switch (subChoice)
                        {
                            case 1:
                                MenuUtils.Insert<Service>(MenuUtils.BuildService());
                                break;
                            case 2:
                                using (var db = new EmployeeContext())
                                {
                                    foreach (var item in db.Services.ToList())
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                break;
                            case 3:
                                string name = MenuUtils.GetString("Name");
                                using (var db = new EmployeeContext())
                                {
                                    foreach (var item in db.Services.Where(x => x.Name.Contains(name)).ToList())
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                break;
                            case 4:
                                int id = MenuUtils.GetIntChoice("Choose an id", 0, int.MaxValue).Value;
                                using (var db = new EmployeeContext())
                                {
                                    Service service = new Service() { ServiceId = id };

                                    db.Services.Attach(service);
                                    db.Services.Remove(service);

                                    foreach (var item in db.Employees.Include(x => x.Department).Where(x => x.Department.ServiceId == id))
                                    {
                                        db.Employees.Attach(item);
                                        item.Department = new Service() { Name = "changed" };
                                        db.Entry(item).State = EntityState.Modified;
                                    }

                                    db.SaveChanges();
                                }
                                break;
                            case 5:
                                int idUpdate = MenuUtils.GetIntChoice("Choose an id", 0, int.MaxValue).Value;
                                Service servToUpdate = MenuUtils.BuildService();
                                using (var db = new EmployeeContext())
                                {
                                    Service servUpdate = db.Services.Find(idUpdate);

                                    db.Services.Attach(servUpdate);
                                    servUpdate.Name = servToUpdate.Name;

                                    db.Entry(servUpdate).State = EntityState.Modified;

                                    db.SaveChanges();
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            } while (!MenuUtils.GetStringChoice("y to quit else n", "y", "n").Equals("y"));
        }
    }
}

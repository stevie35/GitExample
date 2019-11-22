using DAL_Library;
using DAL_Library.Entities;
using DAL_Library2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleAppExLINQ
{
    public class Menu
    {
        public void MainMenu()
        {
            int? choice = null;
            do
            {
                choice = MenuUtils.GetIntChoice(MenuUtils.BaseChoiceMenu(), 1, 4);
                switch (choice)
                {
                    case 1:
                        EmployeeMenu();
                        break;
                    case 2:
                        ServiceMenu();
                        break;
                    case 3:
                        using (var db = new EmployeeContext())
                        {
                            Console.WriteLine(db.Employees.AsNoTracking().Sum(x => x.Salary));
                        }
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        public void SalaryChargeMenu(Func<EmployeeContext, float> func, Action backMenu)
        {
            int? choice = MenuUtils.GetIntChoice(MenuUtils.SalaryChargeMenu(), 1, 2);

            switch (choice)
            {
                case 1:
                    using (var db = new EmployeeContext())
                    {
                        try
                        {
                            Console.WriteLine(func.Invoke(db) + "€");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    break;
                case 2:
                    backMenu.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void PrintFromDb<T>(Func<EmployeeContext, List<T>> func)
        {
            using (var db = new EmployeeContext())
            {
                List<T> items = new List<T>();
                try
                {
                    items = func.Invoke(db);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void EmployeeMenu()
        {
            int? choice = MenuUtils.GetIntChoice(MenuUtils.EmployeeMenu(), 1, 3);

            PrintFromDb<Employee>((EmployeeContext db) => {
                return db.Employees.AsNoTracking().ToList();
            });

            switch (choice)
            {
                case 1:
                    SalaryChargeMenu((EmployeeContext db) =>
                    {
                        return db.Employees.AsNoTracking().Sum(x => x.Salary);
                    }, EmployeeMenu);
                    break;
                case 2:
                    EmployeeMenuFiltered();
                    break;
                case 3:
                    MainMenu();
                    break;
                default:
                    break;
            }
        }

        public void EmployeeMenuFiltered()
        {
            int? choice = MenuUtils.GetIntChoice(MenuUtils.EmployeeFilterMenu(), 1, 3);

            switch (choice)
            {
                case 1:
                    EmployeeMenuFilteredByLastname();
                    break;
                case 2:
                    EmployeeMenuFilteredByFunction();
                    break;
                case 3:
                    EmployeeMenu();
                    break;
                default:
                    break;
            }
        }

        private void EmployeeMenuFilteredByLastname()
        {
            string choice = MenuUtils.GetString("Lastname");
            PrintFromDb<Employee>((EmployeeContext db) => {
                return db.Employees.AsNoTracking().Where(x => x.Lastname.Contains(choice)).ToList();
            });
            SalaryChargeMenu((EmployeeContext db) =>
            {
                return db.Employees.Where(x => x.Lastname.Contains(choice)).Sum(x => x.Salary);
            }, EmployeeMenuFiltered);
        }

        private void EmployeeMenuFilteredByFunction()
        {
            string choice = MenuUtils.GetString("Function");
            PrintFromDb<Employee>((EmployeeContext db) => {
                return db.Employees.AsNoTracking().Where(x => x.Function.Contains(choice)).ToList();
            });
            SalaryChargeMenu((EmployeeContext db) =>
            {
                return db.Employees.Where(x => x.Function.Contains(choice)).Sum(x => x.Salary);
            }, EmployeeMenuFiltered);
        }

        private void ServiceMenu()
        {
            PrintFromDb<Service>((EmployeeContext db) => {
                return db.Services.AsNoTracking().ToList();
            });

            int? serviceId = MenuUtils.GetIntChoice("Choose service by id", 1, int.MaxValue);

            int? choice = MenuUtils.GetIntChoice(MenuUtils.ServiceMenu(), 1, 3);

            Console.WriteLine("Service " + serviceId + " selected");
            PrintFromDb<Service>((EmployeeContext db) => {
                return db.Services.AsNoTracking().Where(x => x.ServiceId == serviceId).ToList();
            });

            switch (choice)
            {
                case 1:
                    PrintFromDb<Employee>((EmployeeContext db) => {
                        return db.Employees.AsNoTracking().Where(x => x.Department.ServiceId == serviceId).ToList();
                    });
                    SalaryChargeMenu((EmployeeContext db) =>
                    {
                        return db.Employees.AsNoTracking().Include(x => x.Department).Where(x => x.Department.ServiceId == serviceId).Sum(x => x.Salary);
                    }, ServiceMenu);
                    break;
                case 2:
                    using (var db = new EmployeeContext())
                    {
                        if (db.Employees.Include(x => x.Department).Where(x => x.Department.ServiceId == serviceId).Count() > 0)
                        {
                            Console.WriteLine(db.Employees.Include(x => x.Department).Where(x => x.Department.ServiceId == serviceId).Sum(x => x.Salary));
                        }
                    }
                    break;
                case 3:
                    MainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}

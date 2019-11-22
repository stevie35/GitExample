using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
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
                        Console.WriteLine(func.Invoke(db) + "$");
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
        int? choice = MenuUtils.GetIntChoice(MenuUtils.EmployeeMenu(), 1, 4);

        switch (choice)
        {
            case 1:
                PrintFromDb<Employee>((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().ToList();
                });
                SalaryChargeMenu((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().Sum(x => x.Salary);
                }, EmployeeMenu);
                break;
            case 2:
                EmployeeMenuFiltered();
                break;
            case 3:
                EmployeeCudMenu();
                break;
            case 4:
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
        PrintFromDb<Employee>((EmployeeContext db) =>
        {
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
        PrintFromDb<Employee>((EmployeeContext db) =>
        {
            return db.Employees.AsNoTracking().Where(x => x.Function.Contains(choice)).ToList();
        });
        SalaryChargeMenu((EmployeeContext db) =>
        {
            return db.Employees.Where(x => x.Function.Contains(choice)).Sum(x => x.Salary);
        }, EmployeeMenuFiltered);
    }

    private void ServiceMenu()
    {
        int? choice = MenuUtils.GetIntChoice(MenuUtils.ServiceMenu(), 1, 3);
        switch (choice)
        {
            case 1:
                ServiceMenuByService();
                break;
            case 2:
                ServiceCudMenu();
                break;
            case 3:
                MainMenu();
                break;
        }
    }

    private void ServiceMenuByService()
    {
        PrintFromDb<Service>((EmployeeContext db) =>
        {
            return db.Services.AsNoTracking().ToList();
        });

        int? serviceId = MenuUtils.GetIntChoice("Choose service by id", 1, int.MaxValue);

        int? choice = MenuUtils.GetIntChoice(MenuUtils.ServiceMenuByService(), 1, 3);

        Console.WriteLine("Service " + serviceId + " selected");
        PrintFromDb<Service>((EmployeeContext db) =>
        {
            return db.Services.AsNoTracking().Where(x => x.ServiceId == serviceId).ToList();
        });

        switch (choice)
        {
            case 1:
                PrintFromDb<Employee>((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().Where(x => x.Department.ServiceId == serviceId).ToList();
                });
                SalaryChargeMenu((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().Include(x => x.Department).Where(x => x.Department.ServiceId == serviceId).Sum(x => x.Salary);
                }, ServiceMenuByService);
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
                ServiceMenu();
                break;
            default:
                break;
        }
    }

    private void EmployeeCudMenu()
    {
        int? choice = MenuUtils.GetIntChoice(MenuUtils.CudMenu(), 1, 4);

        switch (choice)
        {
            case 1:
                using (var db = new EmployeeContext())
                {
                    db.Employees.Add(MenuUtils.BuildEmployeeWithService(db));
                    db.SaveChanges();
                }
                break;
            case 2:
                PrintFromDb<Employee>((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().ToList();
                });
                using (var db = new EmployeeContext())
                {
                    Employee empToUpdate = null;
                    do
                    {
                        empToUpdate = db.Employees.Find(MenuUtils.GetIntChoice("Select employee to update by id", 1, int.MaxValue));
                    } while (empToUpdate == null);

                    Employee newValues = MenuUtils.BuildEmployeeWithService(db);
                    newValues.EmployeeId = empToUpdate.EmployeeId;
                    db.Entry(empToUpdate).CurrentValues.SetValues(newValues);
                    db.SaveChanges();
                }
                break;
            case 3:
                PrintFromDb<Employee>((EmployeeContext db) =>
                {
                    return db.Employees.AsNoTracking().ToList();
                });
                using (var db = new EmployeeContext())
                {
                    Employee empToDelete = null;
                    do
                    {
                        empToDelete = db.Employees.Find(MenuUtils.GetIntChoice("Select employee to delete by id", 1, int.MaxValue));
                    } while (empToDelete == null);

                    db.Entry(empToDelete).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                break;
            case 4:
                EmployeeMenu();
                break;
            default:
                break;
        }
    }

    private void ServiceCudMenu()
    {
        int? choice = MenuUtils.GetIntChoice(MenuUtils.CudMenu(), 1, 4);

        switch (choice)
        {
            case 1:
                using (var db = new EmployeeContext())
                {
                    db.Services.Add(MenuUtils.BuildService());
                    db.SaveChanges();
                }
                break;
            case 2:
                PrintFromDb<Service>((EmployeeContext db) =>
                {
                    return db.Services.AsNoTracking().ToList();
                });
                using (var db = new EmployeeContext())
                {
                    Service srvToUpdate = null;
                    do
                    {
                        srvToUpdate = db.Services.Find(MenuUtils.GetIntChoice("Select service to update by id", 1, int.MaxValue));
                    } while (srvToUpdate == null);

                    Service newValues = MenuUtils.BuildService();
                    newValues.ServiceId = srvToUpdate.ServiceId;
                    db.Entry(srvToUpdate).CurrentValues.SetValues(newValues);
                    db.SaveChanges();
                }
                break;
            case 3:
                PrintFromDb<Service>((EmployeeContext db) =>
                {
                    return db.Services.AsNoTracking().ToList();
                });
                using (var db = new EmployeeContext())
                {
                    Service srvToDelete = null;
                    do
                    {
                        srvToDelete = db.Services.Find(MenuUtils.GetIntChoice("Select service to delete by id", 1, int.MaxValue));
                    } while (srvToDelete == null);

                    db.Entry(srvToDelete).State = EntityState.Deleted;

                    //Check if services remaining
                    if (db.Employees.AsNoTracking().Include(x => x.Department).Where(x => x.Department.ServiceId == srvToDelete.ServiceId).Count() > 0)
                    {
                        PrintFromDb<Employee>((EmployeeContext db1) =>
                        {
                            return db.Employees.AsNoTracking().Include(x => x.Department).Where(x => x.Department.ServiceId == srvToDelete.ServiceId).ToList();
                        });

                        Console.WriteLine("------------------------------------");

                        int? changeId;
                        Service newService;
                        do
                        {
                            PrintFromDb<Service>((EmployeeContext db1) =>
                            {
                                return db.Services.AsNoTracking().Where(x => x.ServiceId != srvToDelete.ServiceId).ToList();
                            });

                            changeId = MenuUtils.GetIntChoice("Select new service id for linked employees", 1, int.MaxValue);
                            newService = db.Services.Find(changeId);
                        } while (newService == null || changeId == srvToDelete.ServiceId);

                        foreach (var item in db.Employees.Include(x => x.Department).Where(x => x.Department.ServiceId == srvToDelete.ServiceId).ToList())
                        {
                            item.Department = newService;
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        //Delete linked employees
                        foreach (var item in db.Employees.Include(x => x.Department).Where(x => x.Department.ServiceId == srvToDelete.ServiceId).ToList())
                        {
                            db.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    db.SaveChanges();
                }
                break;
            case 4:
                ServiceMenu();
                break;
            default:
                break;
        }
    }
}
}
}
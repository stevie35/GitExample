using DAL_Library;
using DAL_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Library2
{
   
        public static class MenuUtils
        {
            public static string BaseChoiceMenu()
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("Choose tables to play with\n");
                builder.Append("1 - employee\n");
                builder.Append("2 - service\n");

                return builder.ToString();
            }

            public static string CrudMenuFor(string table)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("For table table : " + table + "\n");
                builder.Append("1 - Insert new row\n");
                builder.Append("2 - Show all rows\n");
                if (table.Equals("employee"))
                {
                    builder.Append("3 - Show row by firstname and lastname\n");
                }
                else if (table.Equals("service"))
                {
                    builder.Append("3 - Show row by name\n");
                }
                else
                {
                    builder.Append("3 - Show row by id\n");
                }

                builder.Append("4 - Delete row by id\n");
                builder.Append("5 - Update row by id\n");

                return builder.ToString();
            }

            public static int? GetIntChoice(string question, int min, int max)
            {
                int? result = null;
                int outResult = 0;
                string userChoice;

                do
                {
                    Console.WriteLine(question);
                    userChoice = Console.ReadLine();
                } while (!int.TryParse(userChoice, out outResult) || result < min || result > max);

                result = outResult;

                return result;
            }

            public static string GetStringChoice(string question, params string[] options)
            {
                string result;
                bool retry = true;

                do
                {
                    Console.WriteLine(question);
                    result = Console.ReadLine();

                    foreach (var item in options)
                    {
                        if (item.Equals(result))
                        {
                            result = item;
                            retry = false;
                            break;
                        }
                    }
                } while (retry);

                return result;
            }

            public static string GetString(string question)
            {
                string result;

                Console.WriteLine(question);
                result = Console.ReadLine();

                return result;
            }

            public static Employee BuildEmployeeWithService()
            {
                Employee result = new Employee();

                Console.WriteLine("Build Employee");
                result.Firstname = GetString("Firstname");
                result.Lastname = GetString("Lastname");
                result.Salary = GetIntChoice("Salary", 0, 100000).Value;
                result.DateOfBirth = DateTime.Now;
                result.Function = GetString("Function");
                result.Department = BuildService();

                return result;
            }

            public static Employee BuildEmployee()
            {
                Employee result = new Employee();

                Console.WriteLine("Build Employee");
                result.Firstname = GetString("Firstname");
                result.Lastname = GetString("Lastname");
                result.Salary = GetIntChoice("Salary", 0, 100000).Value;
                result.DateOfBirth = DateTime.Now;
                result.Function = GetString("Function");

                return result;
            }

            public static Service BuildService()
            {
                Service result = new Service();

                Console.WriteLine("Build Service");
                result.Name = GetString("Name");

                return result;
            }

            public static void Insert<T>(T item) where T : class
            {
                using (var db = new EmployeeContext())
                {
                    db.Set<T>().Add(item);
                    db.SaveChanges();
                }
            }

            //public static void ShowAll<T>() where T : class
            //{
            //    using (var db = new EmployeeContext())
            //    {
            //        foreach (var item in db.Set<T>().ToList())
            //        {
            //            Console.WriteLine(item);
            //        }
            //    }
            //}

            //public static void DeleteByRowId<T>(long id) where T : class
            //{
            //    using (var db = new EmployeeContext())
            //    {
            //        //db.Set<T>().Remove(db.Set<T>().Find(id));
            //        T item = db.Set<T>().Find(id);
            //        db.Entry<T>(item).State = System.Data.Entity.EntityState.Deleted;
            //        db.SaveChanges();
            //    }
            //}

            //public static void UpdateByRowId<T>(long id, T item) where T : class
            //{
            //    using (var db = new EmployeeContext())
            //    {
            //        T frommDb = db.Set<T>().Find(id);
            //        frommDb = item;
            //        db.Entry<T>(frommDb).State = System.Data.Entity.EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //}
        }
    }


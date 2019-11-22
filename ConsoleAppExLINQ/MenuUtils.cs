using DAL_Library;
using DAL_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppExLINQ
{
    public static class MenuUtils
    {
        public static string BaseChoiceMenu()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Choose between\n");
            builder.Append("1 - employee\n");
            builder.Append("2 - service\n");
            builder.Append("3 - salary charge\n");
            builder.Append("4 - quit\n");

            return builder.ToString();
        }

        public static string SalaryChargeMenu()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("1 - Salary charge\n");
            builder.Append("2 - Back\n");

            return builder.ToString();
        }

        public static string EmployeeMenu()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Employee :\n");
            builder.Append("1 - List employees\n");
            builder.Append("2 - Filter employee\n");
            builder.Append("3 - Back\n");

            return builder.ToString();
        }

        public static string EmployeeFilterMenu()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Employee filtered by :\n");
            builder.Append("1 - Lastname\n");
            builder.Append("2 - Function\n");
            builder.Append("3 - Back\n");

            return builder.ToString();
        }

        public static string ServiceMenu()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Employee filtered by :\n");
            builder.Append("1 - See employees\n");
            builder.Append("2 - Salary charge\n");
            builder.Append("3 - Back\n");

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
    }
}


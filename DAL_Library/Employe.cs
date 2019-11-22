using System;

namespace DAL_Library
{
    public class Employe
    {

        private int employeId;
        public int EmployeId
        {
            get { return employeId; }
            set { employeId = value; }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }

        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private int salary;
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        private int function;
        public int Function
        {
            get { return function; }
            set { function = value; }
        }

        private int serviceId;

        public int ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; }
        }

        public Service Department { get; set; }








    }
}

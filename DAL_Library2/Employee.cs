using DAL_Library;
using DAL_Library2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Library.Entities

{
    public class Employee
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private long employeeId;
        private string firstname;
        private string lastname;
        private DateTime dateOfBirth;
        private string city;
        private float salary;
        private string function;
        private Service department;
        #endregion

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        [Required]
        public Service Department
        {
            get { return department; }
            set { department = value; }
        }

        [Required]
        public string Function
        {
            get { return function; }
            set { function = value; }
        }

        [SalaryValidator]
        public float Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        [Required]
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        [Required]
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Employee()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        #endregion

        #region Events
        #endregion
    }
}

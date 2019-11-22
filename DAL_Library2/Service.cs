using DAL_Library.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Library
{
    public class Service
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private long serviceId;
        private string name;
        private string description;
        private List<Employee> employees;
        #endregion

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonIgnore]
        public List<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Service()
        {
            this.Employees = new List<Employee>();
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

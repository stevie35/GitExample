using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Library2
{
    public class SalaryValidator : ValidationAttribute
    {
        public SalaryValidator() : base("Le salaire doit être compris entre 0 et 100000")
        {
        }

        public override bool IsValid(object value)
        {
            bool result = false;
            float data;
            if (float.TryParse(value.ToString(), out data))
            {
                if (data >= 0 && data <= 100000)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}

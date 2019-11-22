using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL_Library2
{
    class SalaireValid : ValidationAttribute
    {
        public SalaireValid() : base()

        {
            this.ErrorMessage = " Le salaire doit être compris entre 0 et 100000. ";
        }

        public override bool IsValid(object value)
        {
            if (!int.TryParse(value.ToString(), out int val) || val < 0 || val > 100000)
                {
                return false;
            } 
            else
            {
                return true;
            }






        }
    }
}

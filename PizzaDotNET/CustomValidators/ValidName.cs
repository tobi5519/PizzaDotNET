using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PizzaDotNET.CustomValidators
{
    public class ValidName : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = false;

            string name;
            name = Convert.ToString(value);

            if (name[0].ToString().Any(char.IsUpper))
            {
                isValid = true;
            }
            
            return isValid;
        }


    }
}
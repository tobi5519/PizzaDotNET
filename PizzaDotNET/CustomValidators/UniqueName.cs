using PizzaDotNET.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PizzaDotNET.CustomValidators
{
    public class UniqueName : ValidationAttribute
    {

        private ApplicationDbContext _context;

        public UniqueName()
        {
            _context = new ApplicationDbContext();
        }

        public override bool IsValid(object value)
        {
            bool isValid = true;

            string name = Convert.ToString(value);

            var pizzaer = _context.Pizzas.ToList();

            foreach (Pizza pizza in pizzaer)
            {
                // !string.Equals(pizza.Name, "value", StringComparison.OrdinalIgnoreCase)
                if (pizza.Name.ToLower().Equals(name.ToLower()))
                {
                    return false;
                }
            }
           

            

            return isValid;
        }

    }

}
using System.Collections.Generic;

namespace PizzaDotNET.Models
{
    public class SessionAndTotalPizzasViewModel
    {
        public IEnumerable<Pizza> AllPizzas { get; set; }
        public IEnumerable<Pizza> SessionPizzas { get; set; }
    }
}
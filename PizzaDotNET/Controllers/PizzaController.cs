using PizzaDotNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PizzaDotNET.Controllers
{
    public class PizzaController : Controller
    {
        private ApplicationDbContext _context;

        public PizzaController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Pizza pizzamodel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizzamodel);
            }
            var pizza = new Pizza
            {
                Name = pizzamodel.Name,
                Toppings = pizzamodel.Toppings,
                NormPrice = pizzamodel.NormPrice,
                FamPrice = pizzamodel.FamPrice
            };

            _context.Pizzas.Add(pizza);
            _context.SaveChanges();

            return RedirectToAction("Menu", "Pizza");
        }

        public ActionResult Menu()
        {
            
            var pizzaer = _context.Pizzas.ToList();

            return View(pizzaer);
        }

        public ActionResult EmptyCart()
        {
            Session["pizzaCart"] = null;

            return RedirectToAction("Menu");
        }
        
        public ActionResult OrderAPizza(int id, string size)
        {
            var pizzaCart = Session["pizzaCart"] as List<Pizza> ?? new List<Pizza>();
            Pizza orderedPizza = _context.Pizzas.Find(id);

            if (size == "Normal")
            {
                orderedPizza.FamPrice = 0;
                pizzaCart.Add(orderedPizza);
                Session["pizzaCart"] = pizzaCart;

            }
            else if (size == "Family")
            {

                orderedPizza.NormPrice = 0;
                pizzaCart.Add(orderedPizza);
                Session["pizzaCart"] = pizzaCart;

            }

            return RedirectToAction("Menu");

        }

        public ViewResult Order()
        {
            var pizzaCart = Session["pizzaCart"] as List<Pizza> ?? new List<Pizza>();
            return View(pizzaCart);
        }








        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                Pizza pizza = _context.Pizzas.Find(id);
                _context.Pizzas.Remove(pizza);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Menu", "Pizza");
        }


    }
}
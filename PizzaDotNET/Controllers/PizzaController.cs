using PizzaDotNET.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var viewModel = new SessionAndTotalPizzasViewModel
            {
                AllPizzas = _context.Pizzas.ToList(),
                SessionPizzas = Session["pizzaCart"] as List<Pizza> ?? new List<Pizza>()
            };

            return View(viewModel);
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

        public ActionResult RemoveFromOrder(int id)
        {
            var pizzaCart = Session["pizzaCart"] as List<Pizza> ?? new List<Pizza>();
            Pizza order = _context.Pizzas.Find(id);

            var pizza = new Pizza
            {
                Name = order.Name,
                Toppings = order.Toppings,
                NormPrice = order.NormPrice,
                FamPrice = order.FamPrice
            };

            pizzaCart.Remove(pizzaCart.FirstOrDefault(d => d.Id == order.Id));
            Session["CartObjects"] = pizzaCart;
            return RedirectToAction("Order", pizzaCart);


        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Pizza pizza = _context.Pizzas.Find(id);
            
            return View(pizza);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id, Name, Toppings, NormPrice, FamPrice")] Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", new Pizza());
            }

            Pizza p = new Pizza
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Toppings = pizza.Toppings,
                NormPrice = pizza.NormPrice,
                FamPrice = pizza.FamPrice
            };

            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();
            
            return RedirectToAction("Menu", "Pizza");
        }


        [Authorize]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Pizza pizza = _context.Pizzas.Find(id);

                if (pizza == null)
                {
                    return HttpNotFound();
                }

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
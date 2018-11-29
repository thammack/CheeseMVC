using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>()
        {
            { "cheddar", "cheese 01" },
            { "muenster", "cheese 02" },
            { "blue", "cheese 03" },
            { "limburger", "cheese 04" },
            { "swiss", "cheese 05" },
            { "American", "cheese 06" },
            { "goat milk", "cheese 07" }
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            // Add the new cheese to the cheese list.
            Cheeses.Add(name, description);

            return Redirect("/Cheese");
        }

        public IActionResult Remove()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult RemoveCheese(string[] yourDeleteList)
        {
            foreach(var name in yourDeleteList)
            if (Cheeses.ContainsKey(name))
            {
                Cheeses.Remove(name);
            }

            return Redirect("/Cheese");
        }

    }
}

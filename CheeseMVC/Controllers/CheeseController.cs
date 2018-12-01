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

        static public readonly string AddErrorString = "AddError";
        static public readonly KeyValuePair<string, object> AddErrorDuplicate = new KeyValuePair<string, object>(AddErrorString, "Duplicate name");
        static public readonly KeyValuePair<string, object> AddErrorInvalid = new KeyValuePair<string, object>(AddErrorString, "Invalid Name");
        static public readonly KeyValuePair<string, object> AddErrorNone = new KeyValuePair<string, object>(AddErrorString, null);
        static private KeyValuePair<string, object> AddErrorState = AddErrorNone;

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }


        public IActionResult Add()
        {
            ViewData.Add(AddErrorState);
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            if (IsAlpha(name))
            {
                if (Cheeses.ContainsKey(name))
                {
                    SetAddErrorFlag(AddErrorDuplicate);
                    return Redirect("/Cheese/Add");
                }
                else
                {
                    SetAddErrorFlag(AddErrorNone);

                    // Add the new cheese to the cheese list.
                    Cheeses.Add(name, description);
                    return Redirect("/Cheese");
                }
            }
            else
            {
                SetAddErrorFlag(AddErrorInvalid);
                return Redirect("/Cheese/Add");
            }

            
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

        public static bool IsAlpha(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                    return false;
            }

            return true;
        }

        private void SetAddErrorFlag(KeyValuePair<string, object> value)
        {
            AddErrorState = value;
        }
    }
}

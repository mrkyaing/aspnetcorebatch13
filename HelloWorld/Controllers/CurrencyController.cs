using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class CurrencyController : Controller
    {
        public IActionResult CurrencyConvertorV1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CurrencyConvertorV1(string fromCurrency,decimal amount)
        {
            if("x".Equals(fromCurrency))
            {
                ViewBag.Error = "please choose from Currency value.";
                return View();
            }
            decimal results = 0;
            switch (fromCurrency)
            {
                case "usd":results = amount * 2100;break;
                case "sdg": results = amount * 1100; break;
                case "baht": results = amount * 90; break;
            }
            ViewBag.CalculatedAmt= results;
            return View();
        }

        public IActionResult CurrencyConvertorV2() => View();
        [HttpPost]
        public IActionResult CurrencyConvertorV2(string fromCurrency, decimal amount)
        {
            if (fromCurrency is null)
            {
                ViewBag.Error = "please choose from Currency value.";
                return View();
            }
            decimal results = 0;
            switch (fromCurrency)
            {
                case "usd": results = amount * 3650; break;
                case "sdg": results = amount * 1100; break;
                case "baht": results = amount * 103; break;
            }
            ViewBag.SelectedCurrency= fromCurrency;
            ViewBag.Amount = amount;
            ViewBag.CalculatedAmt = results;
            return View();
        }
    }
}

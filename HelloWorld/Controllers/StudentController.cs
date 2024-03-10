using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(StudentModel studentModel)
        {
            return View();
        }
    }
}

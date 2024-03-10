using Microsoft.AspNetCore.Mvc;
namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        //http://localhost:2024/home/getmyname
       public string GetMyName()
        {
            return "Hi, Mr Kyaing";
        }
        //http://localhost:2024/home/TellMeNow
        public string TellMeNow() => DateTime.Now.ToString();

        public int Sum() => 10 + 10;

         //http://localhost:2024/home/index
        public IActionResult Index()
        {
            var now=DateTime.Now;
            string mesage=now.Hour<12?"Good Morning":"Good Afternoon time is :"+now.ToShortTimeString();
            ViewBag.Info=mesage;
            return View();
        }
        [NonAction]
       public ViewResult Friends()
        {
            IList<string> friends = new List<string>()
            {
                "Aung Aung","Min Min","Hla Hla"
            };
            ViewBag.Myfriends = friends;  
            return   View();
        }

        ///http://localhost:2024/home/add?n1=10&n2=10
        public IActionResult Add(int n1,int n2)
        {
            int reuslt = n1 + n2;
            ViewBag.Result = reuslt;
            return View();
        }
        [HttpGet]
        public IActionResult FullName(string firstName,string lastName)
        {
            ViewBag.MyFullName = $"Hello,{firstName} {lastName} , Nice to see you!";
            return View();
        }
        [HttpPost]
        public IActionResult Register(string firstName, string lastName,int CourseFee)
        {
            ViewBag.RegisterInfo = $"Register Successfully,{firstName} {lastName} with amount:{CourseFee}";
            return View();
        }
        public FileResult DownloadPdf()
        {
            string fileName = "Database Questions.pdf";
            string filePath = Path.Combine("wwwroot", "downloadfiles/" + fileName);
            byte[] fileContexts=System.IO.File.ReadAllBytes(filePath);
            return File(fileContexts, "text/pdf",fileName);
        }
        //
        public IActionResult Family()
        {
            ViewData["myFamily"] = "Father:U Ba & Mother:Daw Hla";
            TempData["myFamily"] = "Father:U Ba & Mother:Daw Hla";
            return View();
        }
    }
}

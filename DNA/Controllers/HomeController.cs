using DNA.DBContext;
using DNA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DNA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(User user)
        {
            try
            {
                var duplicate = await _context.User.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
                if (duplicate != null)
                {
                    throw new Exception("You have already subscribed");
                }

                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                //return new
                //{
                //    Message = "Success",
                //    StatusCode = 200
                //};

                TempData["Message"] = "Success";

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                //throw ex;
            }

            return RedirectToAction("Index", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

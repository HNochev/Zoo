using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Models.Home;
using Zoo.Models;
using System.Diagnostics;

namespace Zoo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService news;
        private readonly IAnimalFeedingService feedings;

        public HomeController(ILogger<HomeController> logger, INewsService news, IAnimalFeedingService feedings)
        {
            _logger = logger;
            this.news = news;
            this.feedings = feedings;
        }

        public IActionResult Index()
        {
            ViewData[MessageConstants.SuccessMessage] = "Добре дошли!";

            return View(new HomePageModel
            {
                 News = this.news.GetTopThreeNews(),
                 Feedings = this.feedings.GetTopSevenFeedings(),
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CardsAndTickets()
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
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            var piesListViewModel = new PiesListViewModel();
            piesListViewModel.Pies = pieRepository.AllPies;
            piesListViewModel.CurrentCategory = "Cheese cake";

            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = pieRepository.GetPieById(id);
            if(pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }
    }
}

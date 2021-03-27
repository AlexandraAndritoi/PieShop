using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using System.Linq;

namespace PieShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}

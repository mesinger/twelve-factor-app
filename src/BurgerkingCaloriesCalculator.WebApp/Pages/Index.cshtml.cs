using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Application.UseCases;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BurgerkingCaloriesCalculator.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFindAllProducts _findAllProducts;
        private readonly ICreateMenu _createMenu;
        private readonly IMenuRepository _menuRepository;

        public IndexModel(IFindAllProducts findAllProducts, ICreateMenu createMenu, IMenuRepository menuRepository)
        {
            _findAllProducts = findAllProducts;
            _createMenu = createMenu;
            _menuRepository = menuRepository;
        }
        
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<Menu> Menus { get; set; }

        public async Task OnGet()
        {
            Products = (await _findAllProducts.FindAll()).Products;
            Menus = await _menuRepository.FindAll();
        }

        [DisplayName("Product Ids")]
        [Required]
        [BindProperty]
        public string ProductIdsForMenu { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Products = (await _findAllProducts.FindAll()).Products;
                Menus = await _menuRepository.FindAll();
                return Page();
            }

            var productIds = ProductIdsForMenu.Split(' ');

            var createMenuResult = await _createMenu.CreateMenu(new CreateMenuRequest(productIds));

            if (createMenuResult.Successful)
            {
                Products = (await _findAllProducts.FindAll()).Products;
                Menus = await _menuRepository.FindAll();
                return Page();
            }

            return RedirectToPage("/Error");
        }
    }
}
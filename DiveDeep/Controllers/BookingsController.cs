using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DiveDeep.Controllers
{
    public class BookingsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public BookingsController(UserManager<ApplicationUser> userManager, ICartService cartService, IProductRepository productRepository)
        {

            _userManager = userManager;
            _cartService = cartService;
            _productRepository = productRepository;
        }
    }
}

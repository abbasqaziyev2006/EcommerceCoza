﻿using EcommerceCoza.BLL.Services;
using EcommerceCoza.BLL.Services.Contracts;
using EcommerceCoza.BLL.ViewModels;
using ECommerceCoza.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCoza.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly BasketManager _basketManager;


        public OrderController(IOrderService orderService, UserManager<AppUser> userManager, IOrderDetailService orderDetailService, BasketManager basketManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _orderDetailService = orderDetailService;
            _basketManager = basketManager;
        }

        public async Task<IActionResult> Checkout()
        {
            var addressViewModel = new AddressViewModel();

            var model = new OrderCreateViewModel();

            var basketViewModel = await _basketManager.GetBasketAsync();

            model.BasketViewModel = basketViewModel;

            model.OrderDetails = await _orderDetailService.GetOrderDetailCreateViewModels();
            model = await _orderService.GetUserAndAddressViewModel(model);
            model.TotalPrice = basketViewModel.TotalPrice;

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity!.Name ?? "";

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return BadRequest();

            var models = await _orderService.GetOrderViewModelsAsync(user.Id);

            foreach(var model in models)
            {
                model.TotalCount = model.OrderDetails.Sum(x => x.Quantity);
            }

            return View(models);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _orderService.GetAsync(predicate: x=>x.Id==id && !x.IsDeleted,
                include: x=>x.Include(od=>od.OrderDetails).ThenInclude(pv=>pv.ProductVariant).ThenInclude(p=>p.Product!)
                .Include(od=>od.OrderDetails).ThenInclude(pv=>pv.ProductVariant).ThenInclude(c=>c.Color!)
                .Include(a=>a.Address));

            if(model== null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout (OrderCreateViewModel model)
        {
            if(model.AddressCreateViewModel == null)
            {
                ModelState.AddModelError("", "Unvan qeyd edilmeyib");

                return View(model);
            }

            if (model.AcceptTermsConditions == false)
            {
                ModelState.AddModelError("", "Terms and conditions must be accepted");

                return View(model);
            }

            var basketViewModel = await _basketManager.GetBasketAsync();
            model.OrderDetails =await _orderDetailService.GetOrderDetailCreateViewModels();

            model.BasketViewModel= basketViewModel;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Discount!=null && model.HasAppliedDiscount)
            {
                var discount = await _orderService.GetDiscount(model.Discount);

                if (discount != null)
                { 
                    model.DiscountCodeId = discount.Id;
                    model.DiscountAmount= (model.TotalPrice * discount.SalePercentage) / 100;
                    model.EndPrice = model.TotalPrice - model.DiscountAmount;
                }
            }

            await _orderService.CreateAsync(model);
            _basketManager.CleanBasket();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(string discountCode)
        {
            var discount = await _orderService.GetDiscount(discountCode);
            var result = 0;

            if (discount != null)
            {
                result = discount.SalePercentage;
            }

            return Json(new
            {
                success = true,
                result,
            });
        }

    }
}

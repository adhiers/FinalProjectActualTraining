using Microsoft.AspNetCore.Mvc;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services;
using Microsoft.AspNetCore.Http;

namespace FinalProject.MVC.Controllers
{
    public class DealerCarsController : Controller
    {
        private readonly IDealerCarService _dealerCarService;
        public DealerCarsController(IDealerCarService dealerCarService)
        {
            _dealerCarService = dealerCarService;
        }

        // GET: DealerCarsController
        public async Task<ActionResult> Index()
        {
            //check login
            var account = HttpContext.Session.GetString("account");
            if (string.IsNullOrEmpty(account))
            {
                return RedirectToAction("Login", "Accounts");
            }
            //convert to UserViewModel
            var user = System.Text.Json.JsonSerializer.Deserialize<UserViewModel>(account);
            var token = user?.Token.ToString();
            var models = await _dealerCarService.GetAllDealerCarsAsync(token);
            return View(models);
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DealerCarInsert dealerCarInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dealer = await _dealerCarService.AddDealerCarAsync(dealerCarInsert);
                    if (dealer != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(dealerCarInsert);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var dealer = await _dealerCarService.GetDealerCarByIdAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            var dealerCarUpdate = new DealerCarUpdate
            {
                DealerCarId = dealer.DealerCarId,
                DealerId = dealer.DealerId,
                CarId = dealer.CarId,
                DealerCarPrice = dealer.DealerCarPrice
            };

            return View(dealerCarUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, DealerCarUpdate dealerCarUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dealerCarUpdate.DealerCarId = id;
                    var updatedDealerCar = await _dealerCarService.UpdateDealerCarAsync(dealerCarUpdate);
                    if (updatedDealerCar != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(dealerCarUpdate);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var dealerCar = await _dealerCarService.GetDealerCarByIdAsync(id);
            if (dealerCar == null)
            {
                return NotFound();
            }

            return View(dealerCar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _dealerCarService.DeleteDealerCarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services;
using Microsoft.AspNetCore.Http;

namespace FinalProject.MVC.Controllers
{
    public class DealersController : Controller
    {
        private readonly IDealerService _dealerService;

        public DealersController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }
        // GET: DealersController
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

            var models = await _dealerService.GetDealersAsync(token);
            return View(models);
        }

        public ActionResult Details(string id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DealerInsert dealerInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dealer = await _dealerService.CreateDealerAsync(dealerInsert);
                    if (dealer != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(dealerInsert);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var dealer = await _dealerService.GetDealerByIdAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            var dealerUpdate = new DealerUpdate
            {
                DealerId = dealer.DealerId,
                DealerName = dealer.DealerName,
                DealerAddress = dealer.DealerAddress,
                City = dealer.City,
                Province = dealer.Province,
                TaxRate = dealer.TaxRate
            };

            return View(dealerUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, DealerUpdate dealerUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dealerUpdate.DealerId = id;
                    var updatedDealer = await _dealerService.UpdateDealerAsync(dealerUpdate);
                    if (updatedDealer != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(dealerUpdate);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var dealer = await _dealerService.GetDealerByIdAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _dealerService.DeleteDealerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

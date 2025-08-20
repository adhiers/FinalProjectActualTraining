using Microsoft.AspNetCore.Mvc;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services;
using Microsoft.AspNetCore.Http;

namespace FinalProject.MVC.Controllers
{
    public class SalesPersonsController : Controller
    {
        private readonly ISalesPersonService _salesPersonService;
        public SalesPersonsController(ISalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }
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

            var models = await _salesPersonService.GetSalesPersonsAsync(token);
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
        public async Task<ActionResult> Create(SalesPersonInsert salesPersonInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var salesPerson = await _salesPersonService.CreateSalesPersonAsync(salesPersonInsert);
                    if (salesPerson != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(salesPersonInsert);
            }
            catch
            {
                return View(salesPersonInsert);
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var dealer = await _salesPersonService.GetSalesPersonByIdAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            var salesPersonUpdate = new SalesPersonUpdate
            {
                SPId = dealer.SPId,
                SalesName = dealer.SalesName,
                DealerId = dealer.DealerId,
                Email = dealer.Email,
                PhoneNumber = dealer.PhoneNumber
            };
            return View(salesPersonUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, SalesPersonUpdate salesPersonUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    salesPersonUpdate.SPId = id; // Ensure the ID is set for the update
                    var updatedSalesPerson = await _salesPersonService.UpdateSalesPersonAsync(salesPersonUpdate);
                    if (updatedSalesPerson != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(salesPersonUpdate);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var dealer = await _salesPersonService.GetSalesPersonByIdAsync(id);
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
                await _salesPersonService.DeleteSalesPersonAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

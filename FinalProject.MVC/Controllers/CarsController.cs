using FinalProject.MVC.Models;
using FinalProject.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVC.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
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

            var models = await _carService.GetCarsAsync(token);
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
        public async Task<ActionResult> Create(CarInsert carInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dealer = await _carService.CreateCarAsync(carInsert);
                    if (dealer != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(carInsert);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var carUpdate = new CarUpdate
            {
                CarId = car.CarId,
                ModelType = car.ModelType,
                FuelType = car.FuelType,
                BasePrice = car.BasePrice,
                Stock = car.Stock
            };

            return View(carUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, CarUpdate carUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carUpdate.CarId = id;
                    var updatedDealer = await _carService.UpdateCarAsync(carUpdate);
                    if (updatedDealer != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(carUpdate);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _carService.DeleteCarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

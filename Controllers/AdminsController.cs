using CargoManagement.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoManagement.Admin.Controllers
{
    public class AdminsController : Controller
    {
        //private readonly string ApiUrl = "https://localhost:44333/api/";
        private readonly IConfiguration _configuration;
        public AdminsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<AdminViewModel> admins = new();
            using(var client=new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Admins/GetAllAdmin");
                if (result.IsSuccessStatusCode)
                {
                    admins = await result.Content.ReadAsAsync<List<AdminViewModel>>();
                }
                
            }
            return View(admins);
        }
        public async Task<IActionResult> Details(int id)
        {
            AdminViewModel admin = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Admins/GetAdminById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    admin = await result.Content.ReadAsAsync<AdminViewModel>();
                }
            }
            return View(admin);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminViewModel admin)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Admins/CreateAdmin", admin);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(admin);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                AdminViewModel admin = null;
                using(var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.GetAsync($"Admins/GetAdminById/{id}");
                    if (result.IsSuccessStatusCode)
                    {
                        admin = await result.Content.ReadAsAsync<AdminViewModel>();
                        return View(admin);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Admin Doesn't Exist");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminViewModel admin)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PutAsJsonAsync($"Admins/UpdateAdmin/{admin.Id}", admin);
                    if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Server Error. Please try later");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AdminViewModel admin = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Admins/GetAdminById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    admin = await result.Content.ReadAsAsync<AdminViewModel>();
                }
            }
            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminViewModel admin)
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"Admins/DeleteAdmin/{admin.Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Server Error. Please try Later");
                }
            }
            return View();
        }
    }
}

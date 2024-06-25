using Microsoft.AspNetCore.Mvc;
using ProductListMvc.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ProductListMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProductController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"{_apiBaseUrl}/products");
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"{_apiBaseUrl}/products/{id}");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/products", product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"{_apiBaseUrl}/products/{id}");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/products/{id}", product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"{_apiBaseUrl}/products/{id}");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _httpClient.DeleteAsync($"{_apiBaseUrl}/products/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

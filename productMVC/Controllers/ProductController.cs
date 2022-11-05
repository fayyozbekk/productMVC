using Microsoft.AspNetCore.Mvc;
using productMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace productMVC.Controllers
{
    public class ProductController : Controller
    {

        Uri baseAddress = new Uri("http://ec2-54-189-47-51.us-west-2.compute.amazonaws.com/api/");
        HttpClient client;

        public ProductController()
        { 
        client = new HttpClient();
        client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();    
            HttpResponseMessage response = client.GetAsync(baseAddress + "product/").Result;

            if (response.IsSuccessStatusCode)
            { 
                string data = response.Content.ReadAsStringAsync().Result;
            
                modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(modelList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "product/", content).Result;

            if (response.IsSuccessStatusCode)
            { 
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(baseAddress + "product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model); 
        }
          
        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "product/" + model.id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }


        public IActionResult Delete(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(baseAddress + "product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model);
        }



        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "product/" + model.id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", model);
        }


        public IActionResult Details(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(baseAddress + "product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model);
        }


    }
}

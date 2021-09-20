using GIRA_KNX_Helper.Models.VM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GIRA_KNX_Helper.Controllers
{
    public class GAEditController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public GAEditController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public List<GAVM> GaList { get; set; } = new List<GAVM>();

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // read json & deserialize
            //using (var fs = new FileStream(Path.Combine(_env.WebRootPath, "json", "GAlist.json"), FileMode.OpenOrCreate))
            //{
            //    GaList = await JsonSerializer.DeserializeAsync<List<GAVM>>(fs);
            //}
            ViewBag.GAList = GaList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(GAVM gAVM)
        {
            GaList.Add(gAVM);
            int i = 0;
            foreach(var ga in GaList)
            {
                ga.Id = i;
                i++;
            }
            using var fs = new FileStream(Path.Combine(_env.WebRootPath, "json", "GAlist.json"), FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fs, GaList);
            return RedirectToAction("Index", "GAEdit");
        }
    }
}

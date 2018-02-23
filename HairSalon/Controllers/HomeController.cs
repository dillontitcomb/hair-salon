using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }
				[HttpGet("/stylists")]
        public ActionResult Stylists()
        {
					List<Stylist> stylistList = Stylist.GetAll();
          return View(stylistList);
        }
				[HttpGet("/stylists/{id}")]
        public ActionResult StylistInfo(int id)
        {
					Stylist foundStylist = Stylist.Find(id);
          return View(foundStylist);
        }
				[HttpGet("/stylists/add")]
        public ActionResult StylistForm()
        {
          return View();
        }
				[HttpPost("/stylists")]
        public ActionResult AddStylist()
        {
					Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
					newStylist.Save();
					List<Stylist> stylistList = Stylist.GetAll();
          return View("Stylists", stylistList);
        }
				[HttpGet("/clients/add")]
        public ActionResult ClientForm()
        {
					List<Stylist> stylistList = Stylist.GetAll();
          return View(stylistList);
        }
    }
}

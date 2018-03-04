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
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist selectedStylist = Stylist.Find(id);
          List<Client> allClients = Client.GetAll();
          List<Client> stylistClients = selectedStylist.GetClients();
          List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
          model.Add("selectedStylist", selectedStylist);
          model.Add("stylistClients", stylistClients);
          model.Add("stylistSpecialties", stylistSpecialties);
          model.Add("allClients", allClients);
          return View(model);
        }
        [HttpGet("/stylists/edit/{id}")]
        public ActionResult StylistEdit(int id)
        {
					Stylist foundStylist = Stylist.Find(id);
          return View(foundStylist);
        }
        [HttpPost("/stylists/{id}")]
        public ActionResult UpdateStylist(int id)
        {
					Stylist newStylist = Stylist.Find(id);
          newStylist.UpdateStylistName(Request.Form["new-stylist-name"]);
          return View("StylistInfo", newStylist);
        }
        // [HttpPost("/stylists/add/client")]
        // public ActionResult AddStylistClient(int id)
        // {
				// 	Stylist newStylist = Stylist.Find(id);
        //   Client newClient = new Client(Request.Form("add-client-name"));
        //   return View("StylistInfo", newStylist);
        // }
        // [HttpPost("/stylists/add/specialty")]
        // public ActionResult AddStylistSpecialty(int id)
        // {
				// 	Stylist newStylist = Stylist.Find(id);
        //   newStylist.UpdateStylistName(Request.Form["new-stylist-name"]);
        //   return View("StylistInfo", newStylist);
        // }
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
				[HttpPost("/clients")]
        public ActionResult AddClient()
        {
					Client newClient = new Client(Request.Form["client-name"]);
          newClient.Save();
					Stylist newStylist = Stylist.Find(int.Parse(Request.Form["assigned-stylist"]));
          newClient.AddStylist(newStylist);
					List<Client> clientList = Client.GetAll();
          return View("Clients", clientList);
				}
				[HttpGet("/clients")]
        public ActionResult Clients()
        {
					List<Client> clientList = Client.GetAll();
					return View(clientList);
        }
				[HttpGet("/clients/{id}")]
        public ActionResult Clients(int id)
        {
					Client foundClient = Client.Find(id);
					return View(foundClient);
        }
        [HttpGet("/specialties")]
        public ActionResult Specialties()
        {
					List<Specialty> specialtiesList = Specialty.GetAll();
          return View(specialtiesList);
        }
        [HttpPost("/specialties")]
        public ActionResult AddSpecialty()
        {
  				Specialty newSpecialty = new Specialty(Request.Form["specialty-name"]);
  				newSpecialty.Save();
  				List<Specialty> specialtyList = Specialty.GetAll();
          return View("Specialties", specialtyList);
        }
        [HttpGet("/specialties/add")]
        public ActionResult SpecialtyForm()
        {
          return View();
        }
				[HttpGet("/specialties/{id}")]
        public ActionResult SpecialtyInfo(int id)
        {
					Specialty foundSpecialty = Specialty.Find(id);
          return View(foundSpecialty);
        }
    }
}

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
          List<Specialty> allSpecialties = Specialty.GetAll();
          model.Add("selectedStylist", selectedStylist);
          model.Add("stylistClients", stylistClients);
          model.Add("stylistSpecialties", stylistSpecialties);
          model.Add("allClients", allClients);
          model.Add("allSpecialties", allSpecialties);
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
          return RedirectToAction("StylistInfo", new {id = id});
        }
        [HttpPost("/stylists/add/client/{id}")]
        public ActionResult AddStylistClient(int id)
        {
					Stylist newStylist = Stylist.Find(id);
          Client newClient = Client.Find(int.Parse(Request.Form["add-new-client"]));
          newStylist.AddClient(newClient);
          return RedirectToAction("StylistInfo", new {id = id});
        }
        [HttpPost("/stylists/add/specialty/{id}")]
        public ActionResult AddStylistSpecialty(int id)
        {
          Stylist newStylist = Stylist.Find(id);
          Specialty newSpecialty = Specialty.Find(int.Parse(Request.Form["add-new-specialty"]));
          newStylist.AddSpecialty(newSpecialty);
          return RedirectToAction("StylistInfo", new { id = id});
        }
        [HttpPost("/stylists/delete/{id}")]
        public ActionResult DeleteStylist(int id)
        {
          Stylist newStylist = Stylist.Find(id);
          newStylist.Delete();
          List<Stylist> allStylists = Stylist.GetAll();
          return RedirectToAction("Stylists", allStylists);
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
        public ActionResult ClientInfo(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Client selectedClient = Client.Find(id);
          List<Stylist> clientStylists = selectedClient.GetStylists();
          model.Add("selectedClient", selectedClient);
          model.Add("clientStylists", clientStylists);
          return View(model);
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

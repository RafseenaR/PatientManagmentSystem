using COVID.Models;
using COVID.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace COVID.Controllers
{
    // [Route("api/home")]
    // [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IPatientRepository _patient;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IPatientRepository patient)
        {
            _logger = logger;
            _db = db;
            _patient = patient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id=0)
        {
            if (id == 0)
                return View(new Patient());
            else
            {
              
               //_db.PatientVaccinationHistory.Find(id);
                return View(_db.Patients.Find(id));
            }
        }
       
        public IActionResult Delete(int id)
        {
            Patient pat = _db.Patients.FirstOrDefault(s => s.PatientID == id);
            PatientHistory per = _db.PatientVaccinationHistory.FirstOrDefault(s => s.PatientID == id);
            if (pat != null)
            {
                // PatientHistory history = new();
                //// history.IsDeleted = true;
                // _db.PatientVaccinationHistory.Update(history);
                _db.Remove(per);
                _db.SaveChanges();
                _db.Remove(pat);
                _db.SaveChanges();

                //Patient pt = new();
                //pt.IsDeleted = true;
                //_db.PatientVaccinationHistory.Update(pat);
                _db.SaveChanges();
                return RedirectToAction("Registrations");
            }
            return View();
        }




        //[HttpGet("all-registrations")]
        ////public async Task<IActionResult> Registrations(int id)
        //public IActionResult Registrations()
        //{
        //    IEnumerable<Patients> Patients = _db.Patients.Select(s => s).ToList();
        //    return View(Patients);
        //}
        //[HttpPost("delete")]
        ////public async Task<IActionResult> Delete(int id)
        //public IActionResult Delete(int id)
        //{
        //    PatientInformationModel pat = _db.Patients.FirstOrDefault(s => s.PatientID == id);
        //    if (pat != null)
        //    {
        //        _db.Remove(pat);
        //        _db.SaveChanges();
        //        return RedirectToAction("Registrtaions");
        //    }
        //    return View();
        //}

        public IActionResult Registrations()
        {
            IEnumerable<Patient> Patients = _db.Patients.Select(s => s).ToList();
            //IEnumerable<Patient> Patients = _db.Patients.Where(s => s.IsDeleted).ToList();
            return View(Patients);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Patient model)
        {
            if (ModelState.IsValid)
            {
                await _patient.Create(model);
                return Ok("You Have Done");
            }

            return View();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44366/api/patient");

            //    //HTTP POST
            //    var postTask = client.PostAsJsonAsync<Patient>("patient-registration", model);
            //    postTask.Wait();

            //    var result = postTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //}

            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        }


        //[HttpPost("create")]
        //[AllowAnonymous]
        //[HttpPost("patient-registration")]
        //public IActionResult Create([Bind("PatientID,FirstName,LastName,Email,MobileNo,DOB")]Patient Model)

        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Model.PatientID == 0)
        //        {

        //            _db.Patients.Add(Model);
        //        }

        //        else
        //        {
        //            _db.Patients.Update(Model);
        //        }
        //            _db.SaveChanges();
        //        // return RedirectToAction("");
        //        return Ok("You Have Done");
        //       }
        //        return View(Model);
        //    }

    }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public async Task<IActionResult> Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }


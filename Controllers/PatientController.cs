using COVID.Models;
using COVID.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patient;
        private readonly ApplicationDbContext _db;


        public PatientController(IPatientRepository patient, ApplicationDbContext db)
        {
            _patient = patient;
            _db = db;
        }

        [HttpPost("patient-registration")]
        public async Task<IActionResult> Create(Patient model)
        {
            if (ModelState.IsValid)
            {
                await _patient.Create(model);
                return Ok("You Have Done");
            }
            return Ok();
        }

        [HttpPost("patients-vaccination-current-status")]
        public async Task<IActionResult> PatientInfo()
        {

            //var current = _db.PatientVaccinationHistory
            //     .GroupBy(v => v.PatientID)
            //       .Select(x => x.OrderByDescending(pv => pv.StatusID).First());
            return Ok(_db.Patients
                .Join(_db.PatientVaccinationHistory,
                             pat => pat.PatientID,
                             per => per.PatientID,
                             (pat, per) => new
                             {
                                 pat.PatientID,
                                 pat.FirstName,
                                 pat.LastName,
                                 pat.DOB,
                                 pat.Status

                             }).ToList());
        }

        [HttpGet("patients-vaccinationHistory")]
        public async Task<IActionResult> PatientVaccinationHistory()
        {
            return Ok(_db.Patients
                .Join(_db.PatientVaccinationHistory,
                             pat => pat.PatientID,
                             per => per.PatientID,
                             (pat, per) => new
                             {
                                 pat.PatientID,
                                 pat.FirstName,
                                 pat.LastName,
                                 pat.DOB,
                                 per.AddedOn,
                                 per.StatusID
                             }).ToList());
        }

        [HttpPost("patient-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _patient.Delete(id);
                return Ok("You Have Done");
            }
            return Ok();
        }

    }
}

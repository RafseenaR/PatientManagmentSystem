using COVID.Enum;
using COVID.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace COVID.Repository
{
    public interface IPatientRepository
    {
        Task<bool> Create(Patient Model);
        Task<bool> Delete(int id);

    }
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _db;


        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Patient Model)
        {

            var Pnt = _db.Patients.Where(x => x.MobileNo == Model.MobileNo).FirstOrDefault();
            if (Pnt == null) 
            {
               if (Model.PatientID == 0)
            {
                    //_db.Patients.Add(Model);
                   
                    var pat = new Patient()
                    {
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        MobileNo = Model.MobileNo,
                        DOB = Model.DOB,
                        PassportNo = Model.PassportNo,
                        AddedOn = DateTime.UtcNow,
                        EditedOn = DateTime.UtcNow,
                        AddressLine1 = Model.AddressLine1,
                        AddressLine2 = Model.AddressLine2,
                    };
                    _db.Patients.Add(pat);
                    _db.SaveChanges();
                     Model.PatientID=pat.PatientID;
                   
                    PatientHistory history = new();
                    history.PatientID = Model.PatientID;
                    history.StatusID = Model.Status;
                    history.AddedOn = DateTime.UtcNow;
                    _db.PatientVaccinationHistory.Add(history);
                    _db.SaveChanges();
                

            }
               }
            else
            {
                 var updatedItem = await _db.Patients.AsNoTracking().FirstOrDefaultAsync();
                _db.Patients.Update(Model);
               
                _db.SaveChanges();
                PatientHistory ph = new()
                 {
                    PatientID = Model.PatientID,
                    StatusID = Model.Status,
                    AddedOn = DateTime.UtcNow,
                 };

                _db.PatientVaccinationHistory.Add(ph);
                _db.SaveChanges();


            }
            await _db.SaveChangesAsync();
            return true;
        }



        public async Task<bool> Delete(int id)
        {
           
            Patient pat = _db.Patients.FirstOrDefault(s => s.PatientID == id);
            PatientHistory per = _db.PatientVaccinationHistory.FirstOrDefault(s => s.PatientID == id);
            if (pat != null)
            {
                _db.Remove(per);
                _db.SaveChanges();
                _db.Remove(pat);
                _db.SaveChanges();
                
            }
            return true;
        }


    }
}

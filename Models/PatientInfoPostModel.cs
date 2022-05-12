using COVID.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID.Models
{
    public class Patient
    {
       
        public int PatientID { get; set; }
        //[Required(ErrorMessage = "Enter the Firstname")]
        public string FirstName { get; set; }
        // [Required(ErrorMessage = "Enter the Lastname")]
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Nationality { get; set; }
        public string PassportNo { get; set; }
        public bool? IsDeleted { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public  int Status{ get; set; }
    }
}

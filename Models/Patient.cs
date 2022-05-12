using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COVID.Models
{
    public class PatientInfoPostModel
    {
        [Key]
        public int PatientID { get; set; }
       // [Required(ErrorMessage = "Enter the Firstname")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Enter the Lastname")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Enter the DOB")]
        public DateTime? DOB { get; set; }
       // [Required(ErrorMessage = "Enter the MobileNo")]
        public string MobileNo { get; set; }
       // [Required(ErrorMessage = "Enter the Email")]
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

    }
}

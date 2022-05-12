using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COVID.Models
{
    public class PatientHistory
    {
        [Key]
        public int ID { get; set; }
        public int? PatientID { get; set; }
        public int StatusID { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? AddedBy { get; set; }
        //public bool IsDeleted { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ToneHouseMvc.Models
{
    public class PersoanlDetailsModel
    {
        [Key]
        //public string UserId { get; set; }
        //public string SignUpId { get; set; }
        [Required(ErrorMessage = "Age cannot be empty")]
        [Range(15, 70, ErrorMessage = "Age must be between 15 to 70")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Height cannot be empty")]
        [RegularExpression(@"^([5-8][0-9]|9[0-9]|1[0-9]{2}|2[0-4][0-9]|250)$", ErrorMessage = "Height should only be between 50cm and 250cm.")]
        public string Height { get; set; }
        [Required(ErrorMessage = "Weight can't be empty")]
        [RegularExpression(@"^([1-8][0-9]|9[0-9]|1[0-9]{2}|2[0-4][0-9]|250)$", ErrorMessage = "Weight should only be between 10kg and 255kg.")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "Select a blood group")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Phone number can't be empty")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Mobile number must be a 10 digit number")]
        public string PhoneNo { get; set; }
        //[Required(ErrorMessage = "Photo path can't be empty")]
        public string PhotoPath { get; set; }
    }
    
    }


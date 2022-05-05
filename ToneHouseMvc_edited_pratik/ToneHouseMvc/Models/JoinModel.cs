using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToneHouseMvc.Models
{
    public class JoinModel
    {
        //need to be changed
        [Key]
        [Required(ErrorMessage = "First name can't be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Should contain only letters.")]
        public string Firstname { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Should contain only letters.")]
        public string Middlename { get; set; }
        [Required(ErrorMessage = "Last name can't be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Should contain only letters.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Email Address can't be empty!")]
        [EmailAddress(ErrorMessage = "Email Address not in correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = " Password can't be empty")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}

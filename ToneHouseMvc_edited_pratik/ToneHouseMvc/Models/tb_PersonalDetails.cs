//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToneHouseMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_PersonalDetails
    {
        public int UserId { get; set; }
        public int SignUpId { get; set; }
        public byte Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string PhotoPath { get; set; }
    
        public virtual tb_SignUp tb_SignUp { get; set; }
    }
}

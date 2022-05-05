using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToneHouseMvc.Models
{
    public class CaloriesModel
    {
   
        public string ddlExercise { get; set; }
       
        public float TBTime { get; set; }
     
        public float TBDistance { get; set; }
       
        public float TbCalorie { get; set; }
        public float SumCalorie { get; set; }

    }
}
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
    
    public partial class tb_Exercise
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseDescription { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
    
        public virtual tb_ExerciseGroup tb_ExerciseGroup { get; set; }
        public virtual tb_Type tb_Type { get; set; }
        public virtual tb_Workout tb_Workout { get; set; }
    }
}

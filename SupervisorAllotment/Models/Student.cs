//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupervisorAllotment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Student
    {
        [Required]
        public string CollegeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime DOB { get; set; }
        public short GateRank { get; set; }
        public short GateScore { get; set; }
        public double BtechScore { get; set; }
        public Nullable<byte> Supervisor { get; set; }
        public string Preference { get; set; }
        public Nullable<byte> ChoiceFilled { get; set; }
        public Nullable<byte> ChoiceAlloted { get; set; }
    }
}
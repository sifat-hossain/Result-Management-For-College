//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TeacherProfile
    {
        public int TeacherProfileId { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public string TeacherEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string TeacherPassword { get; set; }
        [Required]
        public int TeacherPhone { get; set; }
        [Required]
        public string TeacherAddress { get; set; }
        [Required]
        public System.DateTime JoiningDate { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Designation Designation { get; set; }
    }
}

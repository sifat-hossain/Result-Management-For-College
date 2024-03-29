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

    public partial class StudentProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentProfile()
        {
            this.Results = new HashSet<Result>();
        }
    
        public int StudentProfileId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public Nullable<int> StudentPhone { get; set; }
        [Required]
        public string StudentEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public Nullable<int> FatherPhone { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public Nullable<int> MotherPhone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Nullable<int> DepartmentId { get; set; }
        [Required]
        public System.DateTime AdmissionDate { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}

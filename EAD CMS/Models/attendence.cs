//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EAD_CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class attendence
    {
        public int Id { get; set; }
        
        [DisplayName("Status")]
        public bool status { get; set; }
        public int ass_course_id { get; set; }
        [DisplayName("Student's ID")]
        
        public string rollno { get; set; }
        [Required]
        [DisplayName("Date")]   
        public System.DateTime date { get; set; }
    
        public virtual assigned_course assigned_course { get; set; }
        public virtual student student { get; set; }
    }
}

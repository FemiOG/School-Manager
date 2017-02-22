using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagerModel.DataAnnotations
{
    [MetadataType(typeof(StudentMetaData))]
    partial class Student
    {
    }
    class StudentMetaData
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle Name is required")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Admission is required")]
        public System.DateTime AdmissionDate { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public System.DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Previous School is required")]
        public string PreviousSchool { get; set; }

        [Required(ErrorMessage = "Previous Class is required")]
        public string PreviousClassPreviousSchool { get; set; }

        [Required(ErrorMessage = "Religion is required")]
        public string Religion { get; set; }
        
        
       
        
        //public string ClassDivision { get; set; }
        
    }

}

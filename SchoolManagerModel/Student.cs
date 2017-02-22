//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int AdmissionNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public System.DateTime AdmissionDate { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string PreviousSchool { get; set; }
        public string PreviousClassPreviousSchool { get; set; }
        public int ParentParentId { get; set; }
        public string Religion { get; set; }
        public Nullable<bool> IsOnSchoolBus { get; set; }
        public string FullName { get; set; }
        public string AcademicSessionEnrolled { get; set; }
        public string ClassDivision { get; set; }
        public string AdmissionStatus { get; set; }
        public int StudentClassStudentClassId { get; set; }
        public string AdmissionNumber { get; set; }
    
        public virtual Parent Parent { get; set; }
        public virtual Account Account { get; set; }
        public virtual StudentClass StudentClass { get; set; }
    }
}

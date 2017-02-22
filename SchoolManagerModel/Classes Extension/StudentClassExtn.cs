using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagerModel;

namespace SchoolManagerModel
{
    public partial class StudentClass
    {
        private static ObservableCollection<StudentClass> _allStudentClasses;

        public static ObservableCollection<StudentClass> getClassList()
        {
            var db = new PottersEntities();
            IEnumerable<StudentClass> newList = db.StudentClasses.AsNoTracking();
            ObservableCollection<StudentClass> _newList = new ObservableCollection<StudentClass>(newList);          
            return _newList;
        }

        public static ObservableCollection<StudentClass> AllStudentClasses
        {
            get { return _allStudentClasses; }

        }
    }
}

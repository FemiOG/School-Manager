using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SchoolManagerModel;
using System.Data;



namespace SchoolManagerModel
{ 
    public partial class Parent
    {
        private static ObservableCollection<Parent> _allParents;

        public static void  Retreive()
        {
            using (PottersEntities db = new PottersEntities()) //Create a new object of the PottersEntities model(DBContext Class)
            {
                IEnumerable<Parent> parents = db.Parents.Include("Students");//retreive the list of all students using the newly created object and assigned it to a variable of type IEnumerable<>                
                ObservableCollection<Parent> parentList = new ObservableCollection<Parent>(parents);
                _allParents = parentList;
            }
        }

        public static ObservableCollection<Parent> AllParents
        {
            get { return _allParents; }
        }
       

        public static string caseFold(string word)
        {
            string lowerWord = word.ToLower();
            StringBuilder name = new StringBuilder(lowerWord);
            string initial = name[0].ToString();
            string UpperCaseReplacement = initial.ToUpper();
            name.Replace(initial, UpperCaseReplacement, 0, 1);
            string newName = name.ToString();
            name.Clear();
            
            return newName;
        }

        public Parent(string fatherFirstName,string fatherPhone,string fatherTitle,string motherFirstName,string motherPhone,string motherTitle,string surname,string email)
        {
            
            
            this.Students = new ObservableCollection<Student>();


            string newSurname =  surname; //Parent.caseFold(surname);
            string newFatherFirstName = fatherFirstName; //Parent.caseFold(fatherFirstName);
            string newMotherFirstName = motherFirstName; //Parent.caseFold(motherFirstName);
            
            this.FatherTitle = fatherTitle;
            this.FatherLastName = surname; //Parent.caseFold(surname);
            this.FatherFirstName = fatherFirstName; //Parent.caseFold(fatherFirstName);
            this.FatherPhone = fatherPhone;
            this.MotherTitle = motherTitle;
            this.MotherLastName = surname; //Parent.caseFold(surname);
            this.MotherFirstName = motherFirstName; //Parent.caseFold(motherFirstName);
            this.MotherPhone = motherPhone;
            this.Email = email;
            if (string.IsNullOrWhiteSpace(fatherFirstName) && !string.IsNullOrWhiteSpace(motherFirstName))
            {
                this.FullName = motherTitle+ " " + newMotherFirstName + " " + newSurname;
                this.FatherTitle = "";
            }
            else if (string.IsNullOrWhiteSpace(motherFirstName) && !string.IsNullOrWhiteSpace(fatherFirstName))
            {
                this.FullName = fatherTitle + " " + newFatherFirstName + " " + newSurname;
                this.MotherTitle = "";
            }
            else if (string.IsNullOrWhiteSpace(fatherFirstName) && string.IsNullOrWhiteSpace(motherFirstName))
            {
                this.FullName = newSurname;
                this.MotherTitle = "";
                this.FatherTitle = "";
            }
            else
            {
                this.FullName = fatherTitle + " & " + motherTitle + " " + newSurname;
            }
           

            var db = new PottersEntities();
            db.Parents.Add(this);            
            db.SaveChanges();                     
        }
        
        public static ObservableCollection<Parent> AllParents2 ()
        {
             PottersEntities db = new PottersEntities();
             IEnumerable<Parent> parents = db.Parents.Include("Students");//retreive the list of all students using the newly created object and assigned it to a variable of type IEnumerable<>                
             ObservableCollection<Parent> parentList = new ObservableCollection<Parent>(parents);
             return parentList;

             
        }
        

        public static void DeleteParent(Parent delParent)
        {
            PottersEntities db = new PottersEntities();
            db.Entry(delParent).State = EntityState.Deleted;
            db.Parents.Remove(delParent);
            
            db.SaveChanges();
        }
        public void updateParent(int parentId, string fatherTitle, string fatherFirstName, string fatherLastName, string fatherPhone, string motherTitle, string motherFirstName, string motherLastName, string motherPhone,string email)
        {
            PottersEntities db = new PottersEntities();
            Parent  parent = db.Parents.Find(parentId);

            string newFatherLastName = fatherLastName; //Parent.caseFold(fatherLastName);
            string newFatherFirstName = fatherFirstName; //Parent.caseFold(fatherFirstName);
            string newMotherLastName = motherLastName; //Parent.caseFold(motherLastName);
            string newMotherFirstName = motherFirstName; //Parent.caseFold(motherFirstName);

            parent.FatherTitle = fatherTitle;
            parent.FatherFirstName = newFatherFirstName;
            parent.FatherLastName = newFatherLastName;
            parent.FatherPhone = fatherPhone;
            parent.MotherTitle = motherTitle;
            parent.MotherFirstName = newMotherFirstName;
            parent.MotherLastName = newMotherLastName;
            parent.MotherPhone = motherPhone;
            parent.Email = email;

            if (string.IsNullOrWhiteSpace(fatherFirstName))
            {
                parent.FullName = motherTitle + " " + newMotherFirstName + " " + newMotherLastName;
                
            }
            else if (string.IsNullOrWhiteSpace(motherFirstName))
            {
                parent.FullName = fatherTitle + " " + newFatherFirstName + " " + newFatherLastName;
               
            }
            else
            {
                parent.FullName = fatherTitle + " & " + motherTitle + " " + newFatherLastName;
            }

            db.Entry(parent).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}

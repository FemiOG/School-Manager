using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity;
using SchoolManagerModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Collections.Specialized;


namespace SchoolManagerModel
{
    /*This is an extension of the Student class that resides in the entity model classes
     *
     */
    public partial class Student 
    {
        private static  ObservableCollection<Student> _allStudents;

        public static void Retreive()
        {
            using (PottersEntities db = new PottersEntities()) //Create a new object of the PottersEntities model(DBContext Class)
            {

                IQueryable<Student> studs = db.Students.Include("Parent").Include("Account").AsNoTracking(); //retreive the list of all students using the newly created object and assigned it to a variable of type IEnumerable<>           
                ObservableCollection<Student> studentList = new ObservableCollection<Student>(studs);
                _allStudents=  studentList;
            }
        }

        public static ObservableCollection<Student> AllStudents
        {
            get { return _allStudents; }
        }
        

        public static string caseFold(string word)
        {
            string lowerWord = word.ToLower();
            StringBuilder name = new StringBuilder(lowerWord);
            string initial = name[0].ToString();
            string UpperCaseReplacement = initial.ToUpper();
            name.Replace(initial, UpperCaseReplacement, 0, 1);
            string newName = name.ToString();

            return newName;
        }

        PottersEntities db = new PottersEntities();
        public Student()
        {

        }
        public Student(Parent parent, string firstname, string middlename, string lastname, int studentClassId, DateTime admissiondate, DateTime dateofbirth,
                        string sex, string address, string previousSchool, string previousClass, string religion, string classDivision, bool? isOnSchoolBus,string schoolBusFees)
        {
            try
            {
                    string newFirstName = firstname; //Student.caseFold(firstname);
                    string newMiddleName = middlename; //Student.caseFold(middlename);
                    string newLastName = lastname; //Student.caseFold(lastname);
                    string newClassDivision = classDivision; //Student.caseFold(classDivision);
                    
                    this.FirstName = newFirstName ;
                    this.MiddleName = newMiddleName;
                    this.LastName = newLastName;
                    this.AdmissionDate = admissiondate;
                    this.DateOfBirth = dateofbirth;
                    this.Sex = sex;
                    this.Address = address;
                    this.PreviousSchool = previousSchool;
                    this.PreviousClassPreviousSchool = previousClass;
                    this.Religion = religion;
                    this.IsOnSchoolBus = isOnSchoolBus;
                    this.Parent = parent;
                    this.FullName = newLastName + " " + newFirstName + " " + newMiddleName;
                    this.ClassDivision = newClassDivision;

                    //assign a class to the student based on selected class in the UI               
                    StudentClass studentClass = db.StudentClasses.Find(studentClassId);
                    this.StudentClass = studentClass;
                    this.Class = studentClass.Name;
                    studentClass.Count ++;
                    this.AcademicSessionEnrolled = AcademicSession.getCurrentTerm().AcademicSessionId.ToString();
                    this.AdmissionStatus = "Admitted";

                    //add this student to the DBcontext class and save changes to the databases
                    db.StudentClasses.Attach(this.StudentClass);
                    db.Parents.Attach(this.Parent);
                    db.Students.Add(this);
                    //db.SaveChanges();
                    //add this student to the Parent supplied by the constructor

                    //parent.Students.Add(this);
                    //attach the modified entity (i.e parent) back to the DBcontext class to identify the modifications made in the entity
                    db.Entry(studentClass).State = EntityState.Modified;
                    db.Entry(parent).State = EntityState.Modified;
                    db.SaveChanges();
                    Student newStudent = db.Students.AsNoTracking().Single(admNo => admNo.AdmissionNo == this.AdmissionNo);

                    //create a new account and pass this student as an argument to the account constructor
                    if (isOnSchoolBus == true)
                    {
                        int newSchoolBusFees = int.Parse(schoolBusFees);
                        Account account = new Account(newStudent, newSchoolBusFees);  
                    }
                    else
                    {
                        Account account = new Account(newStudent);  
                    }
                    

                    //AcademicSession currentTerm =  AcademicSession.getCurrentTerm();
                    //Student.Retreive();
                                    
                    Student.AllStudents.Add(newStudent);
                    

                    
            }
            //catch (NullReferenceException nre)
            //{
            //    string errorMessage = nre.Message;
            //    db.Entry(parent).State = EntityState.Deleted;
            //    db.Parents.Remove(parent);
            //    db.SaveChanges();
                
            //}
            //catch (FormatException fme)
            //{
            //    string errorMessage = fme.Message;
            //    db.Entry(parent).State = EntityState.Deleted;
            //    db.Parents.Remove(parent);
            //    db.SaveChanges();
            //}
            
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                
                db.Parents.Remove(parent);
                db.Entry(parent).State = EntityState.Deleted;
                db.SaveChanges();
            }
            
        }

        public void updateStudent(int StudentAdmissionNo, string newFirstName, string newLastName, string newMiddleName, string newSex, string newReligionComboBox, string newAddress, string newPreviousSchool, string newPreviousClass, string newClassDivision, bool? SchoolBus, double newSchoolBus)
        {

            PottersEntities db = new PottersEntities();
            Student newStudent = db.Students.Find(StudentAdmissionNo);

            string _FirstName = newFirstName; //Student.caseFold(newFirstName);
            string _MiddleName = newMiddleName; //Student.caseFold(newMiddleName);
            string _LastName = newLastName; //Student.caseFold(newLastName);
            string _ClassDivision = newClassDivision; //Student.caseFold(newClassDivision);


            newStudent.LastName = _LastName;
            newStudent.FirstName = _FirstName;
            newStudent.MiddleName = _MiddleName;
            newStudent.Sex = newSex;
            newStudent.Religion = newReligionComboBox;
            newStudent.Address = newAddress;
            newStudent.PreviousSchool = newPreviousSchool;
            newStudent.PreviousClassPreviousSchool = newPreviousClass;
            newStudent.ClassDivision = _ClassDivision;
            newStudent.IsOnSchoolBus = SchoolBus.Value;
            Account _account = db.Accounts.First( x => x.Student.AdmissionNo == StudentAdmissionNo);

            _account.SchoolBusFees = newSchoolBus;



            newStudent.FullName = _LastName + " " + _MiddleName + " " + _LastName;
            db.Entry(_account).State = EntityState.Modified;
            //db.Accounts.Attach(_account);
            db.Entry(newStudent).State = EntityState.Modified;
            db.SaveChanges();
            
            
        }
        

        public void Promote(Student _student, StudentClass _newClass)
        {
            //_student.StudentClass.Count--;

            PottersEntities db = new PottersEntities();

            int oldClassId = _newClass.StudentClassId - 1;
            StudentClass oldClass = db.StudentClasses.Include("Students").First(x => x.StudentClassId.Equals(oldClassId));
            _student.StudentClassStudentClassId = _newClass.StudentClassId;
            //oldClass.Students.Remove(_student);
            
            //_newClass.Students.Add(_student);
            db.Entry(_student).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();

            //PottersEntities dbb = new PottersEntities();
            //_student.Class = _newClass.Name;
            ////_student.StudentClassStudentClassId = _newClass.StudentClassId;
            ////_newClass.Count++;
            //dbb.Entry(_student).State = EntityState.Modified;
            //dbb.SaveChanges();
            
            //dbb.Dispose();
        }

        public string RetreiveFullName(int id)
        {
            var db = new PottersEntities();
            Student stud = db.Students.Find(id);
            return stud.LastName + "" + stud.FirstName;

        } 

        public void PayFees(Student newStudent, double newAmount, string newTellerNo, double newFees,bool newPayment)
        {                
                try
                {
                    PottersEntities db = new PottersEntities();
                    Transaction transaction = new Transaction();
                    Account account = newStudent.Account;

                    transaction.Amount = newAmount;
                    transaction.TellerNo = newTellerNo;
                    transaction.Date = DateTime.Now;
                    transaction.Account = account;
                    transaction.AcademicSession = AcademicSession.getCurrentTerm();

                    if (newPayment)
                    {
                        account.Fees = newFees;
                    }
                    
                    account.CreditBalance = newAmount + account.CreditBalance;
                    if (newAmount > newFees)
                    {
                        double debitBalance = 0;
                        account.DebitBalance = debitBalance;
                    }
                    else
                    {
                        double debitBalance = newFees - newAmount;
                        account.DebitBalance = debitBalance;
                    }
                    
                    //account.DebitBalance = debitBalance;
                    if ((newFees - newAmount) == 0 || newAmount > newFees)
                    {
                        account.FullyPaid = true;
                    }
                    else
                    {
                        account.FullyPaid = false;
                    }

                    db.AcademicSessions.Attach(transaction.AcademicSession);
                    db.Accounts.Attach(transaction.Account);                  
                    db.Transactions.Add(transaction);
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
        }
   
    }
}

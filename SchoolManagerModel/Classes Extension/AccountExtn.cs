using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagerModel;
using System.Collections.ObjectModel;
using System.Data;

namespace SchoolManagerModel
{
    public partial class Account
    {
        PottersEntities db = new PottersEntities();

        private static ObservableCollection<Account> _allAccounts;

        public static void Retreive()
        {
            using (PottersEntities db = new PottersEntities()) //Create a new object of the PottersEntities model(DBContext Class)
            {

                IEnumerable<Account> accs = db.Accounts.Include("Student"); //retreive the list of all students using the newly created object and assigned it to a variable of type IEnumerable<>           
                ObservableCollection<Account> accountList = new ObservableCollection<Account>(accs);

                _allAccounts = accountList;
            }
        }

        public static ObservableCollection<Account> AllAccounts
        {
            get { return _allAccounts; }
        }

        public Account(Student newStudent, double newSchoolBusFees = 0)
        {
            try
            {
                this.Transactions = new HashSet<Transaction>();
                this.CreditBalance = 0.0;
                this.DebitBalance = 0.0;
                this.Fees = newStudent.StudentClass.StudentClassBill.Total;
                this.BalanceLastTerm = 0.0;
                this.Rebate = 0.0;
                this.FullyPaid = false;
                this.SchoolBusFees = newSchoolBusFees;


                //assign this account to the Student passed as an argument to the account constructor
                this.Student = newStudent;
                newStudent.Account = this;
                db.Students.Attach(this.Student);
                //attach the modified entity (i.e parent) back to the DBcontext class to identify the modifications made in the entity
                //db.Entry(newStudent).State = EntityState.Modified;

                //add this account to the DBcontext class and save changes to the database

                db.Accounts.Add(this);
                db.SaveChanges();
            }

            catch (NullReferenceException nre)
            {
                string errorMessage = nre.Message;
                db.Students.Remove(Student);
                db.SaveChanges();

            }
            catch (FormatException fme)
            {
                string errorMessage = fme.Message;
                db.Students.Remove(Student);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                db.Students.Remove(Student);
                db.SaveChanges();
            }
        }
    }
}

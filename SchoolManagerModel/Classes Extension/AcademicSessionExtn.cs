using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagerModel
{
    public partial class AcademicSession
    {
        public AcademicSession(string newAcademicYear, string newTerm)
        {
            PottersEntities dba = new PottersEntities(); 

            this.Transactions = new HashSet<Transaction>();
            this.AcademicYear = newAcademicYear;
            this.Term = newTerm;
            this.FeesPayable = 0.0;
            this.FeesPaid = 0.0;
            this.FeesUnpaid = 0.0;
            this.NewStudentCount = 0;
            this.IsInSession = true;

            dba.AcademicSessions.Add(this);
            dba.SaveChanges();
        }

        public static ObservableCollection<AcademicSession> getAcademicSessions()
        {
            var db = new PottersEntities();
            IEnumerable<AcademicSession> newList = db.AcademicSessions.AsNoTracking();
            ObservableCollection<AcademicSession> _newList = new ObservableCollection<AcademicSession>(newList);
            return _newList;
        }

        public static AcademicSession getCurrentTerm()
        {
            PottersEntities dba = new PottersEntities();
            AcademicSession currentTerm = dba.AcademicSessions.AsNoTracking().Single(x => x.IsInSession == true);
            return currentTerm;
        }
        public static void resolveAccounts()
        {
            Account.Retreive();
            var allAccounts = Account.AllAccounts;
            PottersEntities dba = new PottersEntities();

            foreach (var item in allAccounts)
            {
                if (item.CreditBalance > item.Fees)
                {
                    item.Rebate = item.CreditBalance - item.Fees;
                }
                else
                {
                    item.Rebate = 0.0;
                }
            }

            foreach (var item in allAccounts)
            {
                item.BalanceLastTerm = item.DebitBalance;
                item.DebitBalance = 0.0;
                item.CreditBalance = 0.0;
                item.FullyPaid = false;

                dba.Entry(item).State = EntityState.Modified;
            }
       
            dba.SaveChanges();
        }

        public static void updateNewStudentCount()
        {
            Student.Retreive();
            
            var allStudents = Student.AllStudents;
            PottersEntities dba = new PottersEntities();
            var currentTerm = AcademicSession.getCurrentTerm();

            string academicSessionId = currentTerm.AcademicSessionId.ToString();

            int newStudentCount = 0;
            var newStudents = allStudents.Where(x => x.AcademicSessionEnrolled.Equals(academicSessionId));

            foreach (var item in newStudents)
            {
                newStudentCount++;
            }
            
            currentTerm.NewStudentCount = newStudentCount;
            dba.Entry(currentTerm).State = EntityState.Modified;
            dba.SaveChanges();
        }
        public static void updateFeesPayable()
        //updateFeesPayable
        {
            Student.Retreive();
            var allStudents = Student.AllStudents;
            PottersEntities db = new PottersEntities();
            var currentTerm = AcademicSession.getCurrentTerm();

            double TotalPayable = 0.0;
            foreach (var student in allStudents)
            {
                TotalPayable += student.Account.Fees;
            }

            currentTerm.FeesPayable = TotalPayable;
            db.Entry(currentTerm).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static void updateFeesPaid()
        //updateFeesPaid
        {
            Student.Retreive();
            var allStudents = Student.AllStudents;
            PottersEntities dba = new PottersEntities();
            var currentTerm = AcademicSession.getCurrentTerm();

            double TotalPaid = 0.0;
            foreach (var student in allStudents)
            {
                TotalPaid += student.Account.CreditBalance;
            }

            currentTerm.FeesPaid = TotalPaid;
            dba.Entry(currentTerm).State = EntityState.Modified;
            dba.SaveChanges();
        }
        public static void updateFeesUnpaid()
        //updateFeesUnpaid
        {
            Student.Retreive();
            var allStudents = Student.AllStudents;
            PottersEntities dba = new PottersEntities();
            var currentTerm = AcademicSession.getCurrentTerm();

            double TotalUnpaid = 0.0;
            foreach (var student in allStudents)
            {
                TotalUnpaid += student.Account.DebitBalance;
            }

            currentTerm.FeesUnpaid = TotalUnpaid;
            dba.Entry(currentTerm).State = EntityState.Modified;
            dba.SaveChanges();

        }
        public static void endTerm()
        {
            PottersEntities dba = new PottersEntities();
            var currentTerm = AcademicSession.getCurrentTerm();
            currentTerm.IsInSession = false;

            dba.Entry(currentTerm).State = EntityState.Modified;
            dba.SaveChanges();
        }



    }
}

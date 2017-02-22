using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolManagerModel
{
    public partial class StudentClassBill
    {
        //overloaded method for old students in lower classes (Kg 1, Kg 2, Nursery 1 & Nursery 2)
        public static double generateTotalFees(double SchoolFees, double ExamFees, double LessonFees, double FirstAid, double TextBook, double ExerciseBook)
        {
            double total = SchoolFees + ExamFees + LessonFees + FirstAid  + TextBook + ExerciseBook;
            return total;
        }
        //overloaded method for old students in upper primary (Basic 1 - 5)
        public static double generateTotalFees(double SchoolFees, double ExamFees, double LessonFees ,double FirstAid ,double Computer ,double Music ,double TextBook ,double ExerciseBook)
        {
            double total = SchoolFees + ExamFees + LessonFees + FirstAid + Computer + Music + TextBook + ExerciseBook;
            return total;
        }
        //overloaded method for new students in lower classes (Kg 1, Kg 2, Nursery 1 & Nursery 2) 
        public static double generateTotalFees(double form, double uniform, double sportWear, double developmentLevy, double Cardigan, double AnniversayTShirt, double SchoolFees, double ExamFees, double LessonFees, double FirstAid, double TextBook, double ExerciseBook)
        {
            double total = form + uniform + sportWear + developmentLevy + Cardigan + AnniversayTShirt + SchoolFees + ExamFees + LessonFees + FirstAid + TextBook + ExerciseBook;
            return total;
        }
        //overloaded method for new students in upper primary (Basic 1 - 5)
        public static double generateTotalFees(double form, double uniform, double sportWear, double developmentLevy, double Cardigan, double AnniversayTShirt, double SchoolFees, double ExamFees, double LessonFees, double FirstAid,  double Computer, double Music, double TextBook, double ExerciseBook)
        {
            double total = form + uniform + sportWear + developmentLevy + Cardigan + AnniversayTShirt + SchoolFees + ExamFees + LessonFees + FirstAid  + Computer + Music + TextBook + ExerciseBook;
            return total;
        }

        public bool updateBill(int newClassBillId,double newFormFees, double newUniformFees, double newSportwearFees, double newDevelopmentLevyFees, 
                               double newCardiganFees, double newAnniversaryTshirtFees, double newSchoolFees, double newExamFees, double newLessonFees, 
                               double newFirstAidFees,double newComputerFees, double newMusicFees, double newTextBookFees, double newExerciseBookFees,double newTotalFees)
        {
            try
            {
                PottersEntities db = new PottersEntities();
                StudentClassBill studentClassBill = db.StudentClassBills.Find(newClassBillId);
                
                studentClassBill.AnniversaryTshirtFees = newAnniversaryTshirtFees;
                studentClassBill.CardiganFees = newCardiganFees;
                studentClassBill.ComputerFees = newComputerFees;
                studentClassBill.DevelopmentLevyFees = newDevelopmentLevyFees;
                studentClassBill.ExamFees = newExamFees;
                studentClassBill.ExerciseBookFees = newExerciseBookFees;
                studentClassBill.FirstAidFees = newFirstAidFees;
                studentClassBill.FormFees = newFormFees;
                studentClassBill.LessonFees = newLessonFees;
                studentClassBill.MusicFees = newMusicFees;
                studentClassBill.SchoolFees = newSchoolFees;
                studentClassBill.SportwearFees = newSportwearFees;
                studentClassBill.TextBookFees = newTextBookFees;
                studentClassBill.Total = newTotalFees;
                studentClassBill.UniformFees = newUniformFees;

                db.Entry(studentClassBill).State = EntityState.Modified;
                db.SaveChanges();

                return true;
                
            }
            catch (Exception ex)
            {
                String errorMessage = ex.Message;
                return false;
            }
        }
        public bool updateBill(int newClassBillId, double newFormFees, double newUniformFees, double newSportwearFees, double newDevelopmentLevyFees,
                               double newCardiganFees, double newAnniversaryTshirtFees, double newSchoolFees, double newExamFees, double newLessonFees,
                               double newFirstAidFees, double newTextBookFees, double newExerciseBookFees, double newTotalFees)
        {
            try
            {
                PottersEntities db = new PottersEntities();
                StudentClassBill studentClassBill = db.StudentClassBills.Find(newClassBillId);

                studentClassBill.AnniversaryTshirtFees = newAnniversaryTshirtFees;
                studentClassBill.CardiganFees = newCardiganFees;
                studentClassBill.DevelopmentLevyFees = newDevelopmentLevyFees;
                studentClassBill.ExamFees = newExamFees;
                studentClassBill.ExerciseBookFees = newExerciseBookFees;
                studentClassBill.FirstAidFees = newFirstAidFees;
                studentClassBill.FormFees = newFormFees;
                studentClassBill.LessonFees = newLessonFees;
                studentClassBill.SchoolFees = newSchoolFees;
                studentClassBill.SportwearFees = newSportwearFees;
                studentClassBill.TextBookFees = newTextBookFees;
                studentClassBill.Total = newTotalFees;
                studentClassBill.UniformFees = newUniformFees;

                db.Entry(studentClassBill).State = EntityState.Modified;
                db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                String errorMessage = ex.Message;
                return false;
            }
        }
    }
}

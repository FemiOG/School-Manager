using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SchoolManagerModel;

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : MetroWindow
    {
        public StudentClass selectedClass;

        public Bill()
        {
            InitializeComponent();
           
        }
        public Bill(StudentClass selClass)
        {
            InitializeComponent();
            this.selectedClass = selClass;
        }
        private void Total_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedClass.StudentClassId == 1 || this.selectedClass.StudentClassId == 2 || this.selectedClass.StudentClassId == 3 || this.selectedClass.StudentClassId == 4)
            {
                try
                {
                    //Parse each textbox value to double type and pass them as arguments to the generateTotalFees method
                    double total = StudentClassBill.generateTotalFees(double.Parse(SchoolFeesTextBox.Text), double.Parse(ExamFeesTextBox.Text), double.Parse(LessonFeesTextBox.Text), double.Parse(FirstAidTextBox.Text),
                                                                      double.Parse(TextBookTextBox.Text), double.Parse(ExerciseBookTextBox.Text));
                    TotalTextBox.Text = total.ToString();
                }
                catch (FormatException fme)
                {
                    MessageBox.Show(fme.Message);
                }
            }
            else
            {
                try
                {
                    //Parse each textbox value to double type and pass them as arguments to the generateTotalFees method
                    double total = StudentClassBill.generateTotalFees(double.Parse(SchoolFeesTextBox.Text), double.Parse(ExamFeesTextBox.Text), double.Parse(LessonFeesTextBox.Text), double.Parse(FirstAidTextBox.Text),
                                                                       double.Parse(ComputerTextBox.Text), double.Parse(MusicTextBox.Text),  double.Parse(TextBookTextBox.Text), double.Parse(ExerciseBookTextBox.Text));
                    TotalTextBox.Text = total.ToString();
                }
                catch (FormatException fme)
                {
                    MessageBox.Show(fme.Message);
                }
            }
        }
        private void Total1_Click(object sender, RoutedEventArgs e)
        {
            //checks if computer and music fields are visible which is an indication to whether a student is in upper primary or not
            if (this.selectedClass.StudentClassId == 1 || this.selectedClass.StudentClassId == 2 || this.selectedClass.StudentClassId == 3 || this.selectedClass.StudentClassId == 4 )
            {
                try
                {
                    //Parse each textbox value to double type and ss them as arguments to the generateTotalFees method
                    double total = StudentClassBill.generateTotalFees(double.Parse(FormTextBox.Text), double.Parse(UniformTextBox.Text), double.Parse(SportWearTexBox.Text), double.Parse(DevLevyTextBox.Text), double.Parse(CardiganTextBox.Text), double.Parse(AnniversaryTextBox.Text),
                                                                       double.Parse(SchoolFeesTextBox1.Text), double.Parse(ExamFeesTextBox1.Text), double.Parse(LessonFeesTextBox1.Text), double.Parse(FirstAidTextBox1.Text),
                                                                       double.Parse(TextBookTextBox1.Text), double.Parse(ExerciseBookTextBox1.Text));
                    TotalTextBox1.Text = total.ToString();
                }
                catch (FormatException fme)
                {
                    MessageBox.Show("Total fees could not be calculated due to an error\n\nSuggestions\n----------------------------------------------------------------\n> Make sure only numbers and not letters are input into the form\n\n> Make sure no text box in the form is empty\n\n> Then try again", "Error");
                                                                                                        
                    MessageBox.Show(fme.Message);
                }
            }
            else
            {
                try
                {

                    //Parse each textbox value to double type and pass them as arguments to the generateTotalFees method
                    double total = StudentClassBill.generateTotalFees(double.Parse(FormTextBox.Text), double.Parse(UniformTextBox.Text), double.Parse(SportWearTexBox.Text), double.Parse(DevLevyTextBox.Text), double.Parse(CardiganTextBox.Text), double.Parse(AnniversaryTextBox.Text),
                                                                      double.Parse(SchoolFeesTextBox.Text), double.Parse(ExamFeesTextBox1.Text), double.Parse(LessonFeesTextBox1.Text), double.Parse(FirstAidTextBox1.Text),
                                                                       double.Parse(ComputerTextBox1.Text), double.Parse(MusicTextBox1.Text), double.Parse(TextBookTextBox1.Text), double.Parse(ExerciseBookTextBox1.Text));
                    
                    TotalTextBox1.Text = total.ToString();
                }
                catch (FormatException fme)
                {
                    MessageBox.Show(fme.Message);
                }
            }
        }
        private void Update1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double newFormFees = double.Parse(FormTextBox.Text);
                double newUniformFees = double.Parse(UniformTextBox.Text);
                double newSportwearFees = double.Parse(SportWearTexBox.Text);
                double newDevelopmentFees = double.Parse(DevLevyTextBox.Text);
                double newCardiganFees = double.Parse(CardiganTextBox.Text);
                double newAnniversaryFees = double.Parse(AnniversaryTextBox.Text);
                double newSchoolFees = double.Parse(SchoolFeesTextBox1.Text);
                double newExamFees = double.Parse(ExamFeesTextBox1.Text);
                double newLessonFees = double.Parse(LessonFeesTextBox1.Text);
                double newFirstAidFees = double.Parse(FirstAidTextBox1.Text);
                double newTextBookFees = double.Parse(TextBookTextBox1.Text);
                double newExerciseBookFees = double.Parse(ExerciseBookTextBox1.Text);

                PottersEntities db = new PottersEntities();
                int studentClassBillId = int.Parse(this.selectedClass.StudentClassBill.StudentClassBillId.ToString());

                if (this.selectedClass.StudentClassId == 1 || this.selectedClass.StudentClassId == 2 || this.selectedClass.StudentClassId == 3 || this.selectedClass.StudentClassId == 4)
                {
                    try
                    {
                        double total = StudentClassBill.generateTotalFees(newFormFees, newUniformFees, newSportwearFees, newDevelopmentFees, newCardiganFees, newAnniversaryFees, newSchoolFees, newExamFees, newLessonFees, newFirstAidFees, newTextBookFees, newExerciseBookFees);
                        MessageBoxResult result = MessageBox.Show("The total school fees for " + this.selectedClass.Name + " is N" + total + ", are you sure you want to update the bill? ", "Alert", MessageBoxButton.OKCancel);
                        
                        if (result == MessageBoxResult.OK)
                        {
                            StudentClassBill studentClassBill = new StudentClassBill();
                            bool updateSuccessful = studentClassBill.updateBill(studentClassBillId, newFormFees, newUniformFees, newSportwearFees, newDevelopmentFees, newCardiganFees, newAnniversaryFees, newSchoolFees,
                                                        newExamFees, newLessonFees, newFirstAidFees, newTextBookFees, newExerciseBookFees, total);

                            MessageBoxResult finalResult = MessageBox.Show(this.selectedClass.Name + " bill update was succesfull!");
                            if (finalResult == MessageBoxResult.OK)
                            {
                                this.Close();
                            }
                        }
                        else if (result == MessageBoxResult.Cancel)
                        {
                            this.Close();
                        }
                       
                        
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(this.selectedClass.Name + " bill update was not succesfull!\n\nSuggestions\n-----------------------------------------------\n> Make sure only numbers and not letters are input into the form \n\n> Make sure no text box in the form is empty\n\n> Then try again", "Error");
                        //MessageBox.Show(fme.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    double newComputerFees = double.Parse(ComputerTextBox1.Text);
                    double newMusicFees = double.Parse(MusicTextBox1.Text);

                    try
                    {
                        double total = StudentClassBill.generateTotalFees(newFormFees, newUniformFees, newSportwearFees, newDevelopmentFees, newCardiganFees, newAnniversaryFees, newSchoolFees, newExamFees, newLessonFees, newFirstAidFees, newComputerFees,newMusicFees, newTextBookFees, newExerciseBookFees);
                        MessageBoxResult result = MessageBox.Show("The total school fees for " + this.selectedClass.Name + " is N" + total + ", are you sure you want to update the bill? ", "Alert", MessageBoxButton.OKCancel);
                        
                        if (result == MessageBoxResult.OK)
                        {
                            StudentClassBill studentClassBill = new StudentClassBill();
                            bool updateSuccessful = studentClassBill.updateBill(studentClassBillId, newFormFees, newUniformFees, newSportwearFees, newDevelopmentFees, newCardiganFees, newAnniversaryFees, newSchoolFees,
                                                        newExamFees, newLessonFees, newFirstAidFees, newComputerFees, newMusicFees, newTextBookFees, newExerciseBookFees, total);

                            MessageBoxResult finalResult = MessageBox.Show(this.selectedClass.Name + " bill update was succesfull!");
                            if (finalResult == MessageBoxResult.OK)
                            {
                                this.Close();
                            }
                            
                        }
                        else if (result == MessageBoxResult.Cancel)
                        {
                            
                            this.Close();
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(this.selectedClass.Name + " bill update was not succesfull!\n\nSuggestions\n-----------------------------------------------\n> Make sure only numbers and not letters are input into the form \n\n> Make sure no text box in the form is empty\n\n> Then try again", "Error");
                        //MessageBox.Show(fme.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BillWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = selectedClass.StudentClassBill;
        }
    }
}

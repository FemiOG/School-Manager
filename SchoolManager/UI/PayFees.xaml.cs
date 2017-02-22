using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using SchoolManagerModel;
using System.Data.Entity;
using System.Data;
using System.Collections.ObjectModel;

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for PayFees.xaml
    /// </summary>
    public partial class PayFees : MetroWindow
    {
        public Student student { get; set; }
        public bool newPayment { get; set; }
        public bool fromStudentPage = false;
        public bool newStudent { get; set; }
       

        public PayFees()
        {
            InitializeComponent();
            this.fromStudentPage = false;
            
        }
        public PayFees(Student newStudent)
        {
            InitializeComponent();
            student = newStudent;
            ClassComboBox.Text = newStudent.StudentClass.Name;
            StudentComboBox.Text = newStudent.FullName;
            ClassComboBox.IsEnabled = false;
            StudentComboBox.IsEnabled = false;
            this.fromStudentPage = true;

        }
        private void PayFeesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<StudentClass> femi = StudentClass.getClassList();
            ClassComboBox.ItemsSource = femi;
            
        }

        private async void GenerateFees_Click(object sender, RoutedEventArgs e)
        {
            Student _student = new Student();
            if (fromStudentPage)
            {
                _student = student;
            }
            else
            {
                _student = (Student)StudentComboBox.SelectedItem;
            }
            if (ReasonTextBox.SelectedIndex == 1)
            {
                SubTotalTextBox.IsEnabled = true;
                SubTotalTextBox.Text = _student.Account.TextBookFees;

            }
            try
            {
                var currentTerm = AcademicSession.getCurrentTerm();

                if (!MainWindow.NewStudentFeature)
                {
                    this.newStudent = false;
                    await showConfirmation();

                }
                else if (MainWindow.NewStudentFeature)
                {
                    if (_student.AcademicSessionEnrolled == currentTerm.AcademicSessionId.ToString())
                    {
                        this.newStudent = true;
                    }
                    else
                    {
                        this.newStudent = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
            try
            {
                PottersEntities db = new PottersEntities();

                if (_student.Account.CreditBalance > 0)
                {
                    this.newPayment = false;
                }
                else
                {
                    this.newPayment = true;
                }
                        
                double updatedFees;
                if (!this.newStudent)
                {
                    double _schoolFees = _student.StudentClass.StudentClassBill.SchoolFees;
                    double _examFees = _student.StudentClass.StudentClassBill.ExamFees;
                    double _lessonFees = _student.StudentClass.StudentClassBill.LessonFees;
                    double _firstAidFees = _student.StudentClass.StudentClassBill.FirstAidFees;
                    double _textBookFees = _student.StudentClass.StudentClassBill.TextBookFees;
                    double _exerciseBookFees = _student.StudentClass.StudentClassBill.ExerciseBookFees;
                    

                    if (_student.StudentClass.StudentClassId == 1 ||_student.StudentClass.StudentClassId == 2 ||_student.StudentClass.StudentClassId == 3 ||_student.StudentClass.StudentClassId == 4 )
                    {
                        updatedFees = StudentClassBill.generateTotalFees(_schoolFees,_examFees,_lessonFees,_firstAidFees,_textBookFees,_exerciseBookFees);                        
                    }
                    else
                    {
                        double _computerFees = _student.StudentClass.StudentClassBill.ComputerFees.Value;
                        double _musicFees = _student.StudentClass.StudentClassBill.MusicFees.Value;
                        updatedFees = StudentClassBill.generateTotalFees(_schoolFees, _examFees, _lessonFees, _firstAidFees, _computerFees,_musicFees, _textBookFees, _exerciseBookFees);                        
                    }
                 
                    updatedFees = updatedFees - _student.Account.Rebate + _student.Account.BalanceLastTerm;
                }
                else
                {
                    updatedFees = _student.StudentClass.StudentClassBill.Total - _student.Account.Rebate;
                }
                
                if (_student.IsOnSchoolBus == true)
                {
                    updatedFees = updatedFees - _student.Account.SchoolBusFees;
                }
                StudentFeesTextBox.Text = _student.StudentClass.StudentClassBill.Total.ToString();

                //check id its first term (Textbook and exercise book fees would be deducted regardless)
               
                
                //check if its a newpayment via the "newPayment" boolean property
                if (this.newPayment)
                {
                    PaymentOptions window = new PaymentOptions();
                    window.ShowDialog();

                    
                    if (window.TextBook == true)
                    {
                        double studentFees = updatedFees;
                        SubTotalTextBox.Text = studentFees.ToString();
                        string message = "The total school fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees;
                        await this.ShowMessageAsync("Notification",message);
                        //MessageBox.Show();
                    }
                    else 
                    {
                        double studentFees = updatedFees - _student.StudentClass.StudentClassBill.TextBookFees;
                        SubTotalTextBox.Text = studentFees.ToString();
                        string message = "The total school fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees;
                        await this.ShowMessageAsync("Notification", message);
                    }
                    //else if (window.TextBook == true && window.ExerciseBook == true)
                    //{
                    //    double studentFees = updatedFees;
                    //    SubTotalTextBox.Text = studentFees.ToString();
                    //    string message = "The total school fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees;
                    //    await this.ShowMessageAsync("Notification", message);
                        
                    //}
                    //else if (window.TextBook == false && window.ExerciseBook == false)
                    //{
                    //    double studentFees = updatedFees - _student.StudentClass.StudentClassBill.TextBookFees - _student.StudentClass.StudentClassBill.ExerciseBookFees;
                    //    SubTotalTextBox.Text = studentFees.ToString();
                    //    string message = "The total school fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees;
                    //    await this.ShowMessageAsync("Notification", message);
                    //}
                }
                
                else if (!this.newPayment)
                {    
                    double remainingFees = _student.Account.DebitBalance;
                    if (remainingFees <= 0 )
                    {
                        string message = _student.FullName + " with Admission No " + _student.AdmissionNumber + " has completed payment of school fees! ";
                        await this.ShowMessageAsync("Notification", message);
                        //MessageBox.Show(_student.FullName + " with Admission No " + _student.AdmissionNumber + " has completed payment of school fees! ");
                        Close();
                    }
                    else 
                    {
                        double studentFees = remainingFees;
                        SubTotalTextBox.Text = studentFees.ToString();
                        string message = "The total remaining fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees;
                        await this.ShowMessageAsync("Notification", message);
                        //MessageBox.Show("The total remaining fees for " + _student.FullName + " with Admission No " + _student.AdmissionNumber + " is: " + studentFees);
                    }         
                }
                
            }
            catch (NullReferenceException nre)
            {
                //this.ShowProgressAsync("Please wait...","downloading");
                this.ShowMessageAsync("Error", "Please select a student");
                //MessageBox.Show("Please select a student!");
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error",ex.Message);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                PayFeesButton.IsEnabled = true;
            }
            
        }


        private async Task showConfirmation()
        {
            MessageDialogResult result = await this.ShowMessageAsync("Alert", "New Student Feature has been turned off, click ok to continue or cancel to turn on", MessageDialogStyle.AffirmativeAndNegative);
            if (result.Equals(MessageDialogResult.Negative))
            {
                this.Close();
                Settings window = new Settings();
                window.SettingsTabControl.SelectedIndex = 2;
                window.ShowDialog();
                //this.Close();

            }
            else if (result.Equals(MessageDialogResult.Affirmative))
            {
                //do nothing
            }
        }
        private async void PayFeesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double newAmount = double.Parse(AmountTextBox.Text);
                string newTellerNo = TellerNoTextBox.Text;
                double newFees = double.Parse(SubTotalTextBox.Text);

                double feesDifference = newFees - newAmount;

                PottersEntities db = new PottersEntities();
                int admissionNo = int.Parse(StudentComboBox.SelectedValue.ToString());
                Student newStudent = db.Students.AsNoTracking().Single(admNo => admNo.AdmissionNo == admissionNo);
                newStudent.PayFees(newStudent, newAmount, newTellerNo, newFees,newPayment);

                if (feesDifference == 0)
                {
                    string message = "School fees for " + newStudent.FullName + " has been fully paid!";
                    await this.ShowMessageAsync("Notification", message);
                    //MessageBox.Show("School fees for " + newStudent.FullName + " has been fully paid!");
                    newStudent.Account.FullyPaid = true;
                    
                }
                else
                {
                    string message = "School fees for " + newStudent.FullName + " remains " + feesDifference;
                    await this.ShowMessageAsync("Notification", message);
                    //MessageBox.Show("School fees for " + newStudent.FullName + " remains " + feesDifference);
                    newStudent.Account.FullyPaid = false;
                }
            }
            catch (FormatException ex)
            {
                this.ShowMessageAsync("Error", "Fees payment was not succesfull!, Please fill in the form correctly");
                //MessageBox.Show(" Fees payment was not succesfull!, Please fill in the form correctly");
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
            Close();
            
        }
    }
}

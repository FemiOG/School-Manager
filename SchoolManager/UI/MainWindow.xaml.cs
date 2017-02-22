using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SchoolManagerModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Data Annotations!!!/

namespace SchoolManager.UI 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static bool NewStudentFeature = true;
       

        public MainWindow()
        {
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-NG");
            //this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            //CultureInfo FrCulture = new CultureInfo("fr-FR");

            //// Sets the CurrentCulture to fr-FR.
            //Thread.CurrentThread.CurrentCulture = FrCulture;
            InitializeComponent();
            this.Loaded += MainWindow1_Loaded;  
           
                  
        }

       
        private void Finish_Click(object sender, RoutedEventArgs e)
        {  
            PottersEntities db = new PottersEntities();
            Parent newParent = new Parent();
            Student newStudent = new Student();
            try
            {
                //check if the parent of the student exists already
                if (OldParent.IsChecked == true)
                {
                    SchoolManagerModel.Parent newPar = (Parent)ParentId.SelectedItem;
                    int OldParentId = newPar.ParentId;
                    newParent = db.Parents.AsNoTracking().Single(par => par.ParentId == OldParentId);

                    //create a new student
                    newStudent = new Student(newParent, FirstName.Text, MiddleName.Text, LastName.Text, int.Parse(ClassList.SelectedValue.ToString()),
                                                          AdmissionDate.SelectedDate.Value.Date, DateOfBirth.SelectedDate.Value.Date,
                                                          Sex.Text, Address.Text, PreviousSchool.Text, PreviousClassList.Text, ReligionList.Text, ClassDivision.Text, SchoolBusCheckBox.IsChecked,SchoolBusFeesTextBox.Text);
                }
                else
                {
                    //create a new parent
                    newParent = new Parent(FatherFirstName.Text, FatherPhone.Text, FatherTitleList.Text, MotherFirstName.Text, MotherPhone.Text, MotherTitleList.Text, LastName.Text, Email.Text);


                    //create a new student 
                    newStudent = new Student(newParent, FirstName.Text, MiddleName.Text, LastName.Text, int.Parse(ClassList.SelectedValue.ToString()),
                                                         AdmissionDate.SelectedDate.Value.Date, DateOfBirth.SelectedDate.Value.Date,
                                                         Sex.Text, Address.Text, PreviousSchool.Text, PreviousClassList.Text, ReligionList.Text, ClassDivision.Text, SchoolBusCheckBox.IsChecked,SchoolBusFeesTextBox.Text);

                }
                string message = "Registration for " + LastName.Text + " " + FirstName.Text + " was Successful!";
                //this.ShowMessageAsync("Notification", message);

                //MessageBox.Show("Registration for " + LastName.Text + " " + FirstName.Text + " was Successful!");

            }
            catch (FormatException)
            {
                if (NewParent.IsChecked.Equals(true))
                {
                    SchoolManagerModel.Parent.DeleteParent(newParent);
                }
                
                //MessageBox.Show(ef.Message);
                this.ShowMessageAsync("Notification", "One of the fields was filled incorrectly, please fill the form again");
            }
            catch (NullReferenceException)
            {
                if (NewParent.IsChecked.Equals(true))
                {
                    SchoolManagerModel.Parent.DeleteParent(newParent);
                }
                this.ShowMessageAsync("Error","Please Select a class" );
            }
            catch (Exception ex)
            {
                if (NewParent.IsChecked.Equals(true))
                {
                    SchoolManagerModel.Parent.DeleteParent(newParent);
                }
                this.ShowMessageAsync("Notification", ex.Message);
                //MessageBox.Show(ex.Message);
            }
            finally
            {

                FirstName.Clear();
                LastName.Clear();
                MiddleName.Clear();
                Sex.Text = "";
                Address.Clear();
                ClassList.Text = "";
                DateOfBirth.Text = "";
                AdmissionDate.Text = "";
                SchoolBusCheckBox.IsChecked = false;
                ReligionList.Text = "";
                SchoolBusFeesTextBox.IsEnabled = false;

                ParentId.SelectedIndex = -1;
                NewParent.IsChecked = true;
                FatherTitleList.SelectedIndex = 0;
                FatherFirstName.Clear();
                FatherPhone.Clear();
                MotherTitleList.SelectedIndex = 0;
                MotherFirstName.Clear();
                MotherPhone.Clear();
                Email.Clear();

                PreviousSchool.Clear();
                PreviousClassList.Text = "";

                FirstName.Focus();

                }
            }

        private void OldParent_Checked(object sender, RoutedEventArgs e)
        {
            ParentId.IsEnabled = true;
            MotherTitleList.IsEnabled = false;
            FatherTitleList.IsEnabled = false;
            ReligionList.IsEnabled = false;
            FatherFirstName.IsEnabled = false;
            MotherFirstName.IsEnabled = false;
            FatherPhone.IsEnabled = false;
            MotherPhone.IsEnabled = false;
            Email.IsEnabled = false;

            OldParent.IsChecked = true;
            NewParent.IsChecked = false;

            ParentSurnameTextBox.Focus();

        }
        private void NewParent_Checked(object sender, RoutedEventArgs e)
        {
            ParentId.IsEnabled = false;
            MotherTitleList.IsEnabled = true;
            FatherTitleList.IsEnabled = true;
            ReligionList.IsEnabled = true;
            FatherFirstName.IsEnabled = true;
            MotherFirstName.IsEnabled = true;
            FatherPhone.IsEnabled = true;
            MotherPhone.IsEnabled = true;           
            Email.IsEnabled = true;

            OldParent.IsChecked = false;
            NewParent.IsChecked = true;

            FatherTitleList.Focus();
        }
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {


            SchoolManagerModel.Parent.Retreive();
            var _allParents = SchoolManagerModel.Parent.AllParents;
            var allParents = SchoolManagerModel.Parent.AllParents;

            ParentsTab.DataContext = _allParents ;
            ParentListBox.ItemsSource = allParents;
            CollectionViewSource.GetDefaultView(ParentListBox.ItemsSource).Filter = ParentListBoxFilter;
            ParentId.ItemsSource = SchoolManagerModel.Parent.AllParents2();
            CollectionViewSource.GetDefaultView(ParentId.ItemsSource).Filter = ParentIdFilter;
           
            SchoolManagerModel.Student.Retreive();
            var _allStudents = Student.AllStudents;
            this.DataContext = _allStudents;
            StudentsTab.DataContext = _allStudents;
            StudentListBox.ItemsSource = _allStudents;
            CollectionViewSource.GetDefaultView(StudentListBox.ItemsSource).Filter = UserFilter;

            ObservableCollection<StudentClass> classList = StudentClass.getClassList();
            ClassList.ItemsSource = classList;    
            LegderClassList.ItemsSource = classList;

            string term = AcademicSession.getCurrentTerm().Term;
            string year = AcademicSession.getCurrentTerm().AcademicYear;

            TermTextBox.Text = term;
            SessionTextBox.Text = year;
            TermTermTextBox.Text = term;
            SessionTermTextBox.Text = year;

            TermPanel.DataContext = AcademicSession.getAcademicSessions();

            TotalFeesPayableTextBox.Text = AcademicSession.getCurrentTerm().FeesPayable.ToString();
            TotalFeesPaidTextBox.Text = AcademicSession.getCurrentTerm().FeesPaid.ToString();
            TotalFeesUnpaidTextBox.Text = AcademicSession.getCurrentTerm().FeesUnpaid.ToString();
            NewStudentTextBox.Text = AcademicSession.getCurrentTerm().NewStudentCount.ToString();


            Account.Retreive();
            LedgerTab.DataContext = Account.AllAccounts;

            NewParent.IsChecked = true;

            Password.Focus();
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(StudentFilter.Text))
            {
                return true;
            }
            else
            {
                var student = (Student)item;
                return (student.LastName.StartsWith(StudentFilter.Text, StringComparison.OrdinalIgnoreCase) || student.FirstName.StartsWith(StudentFilter.Text, StringComparison.OrdinalIgnoreCase));
            }

        }
        private void StudentFilter_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(StudentListBox.ItemsSource).Refresh();
        }


        private bool ParentListBoxFilter(object item)
        {
            if (String.IsNullOrEmpty(ParentFilter.Text))
            {
                return true;
            }
            else
            {
                var parent = (Parent)item;
                return (parent.FatherLastName.StartsWith(ParentFilter.Text, StringComparison.OrdinalIgnoreCase) || parent.MotherLastName.StartsWith(ParentFilter.Text, StringComparison.OrdinalIgnoreCase));
            }
        }
        private void ParentFilter_TextChanged_1(object sender, TextChangedEventArgs e)
        {  
            CollectionViewSource.GetDefaultView(ParentListBox.ItemsSource).Refresh(); 
        }

        private bool ParentIdFilter(object item)
        {
            if (String.IsNullOrEmpty(ParentSurnameTextBox.Text))
            {
                return true;
            }
            else
            {
                var parent = (Parent)item;
                return (parent.FatherLastName.StartsWith(ParentSurnameTextBox.Text, StringComparison.OrdinalIgnoreCase) || parent.MotherLastName.StartsWith(ParentSurnameTextBox.Text, StringComparison.OrdinalIgnoreCase));
            }

        }
        private void ParentSurnameTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ParentId.ItemsSource).Refresh();
        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            var db = new PottersEntities();
            StudentClass selectedClass = db.StudentClasses.Find(LegderClassList.SelectedValue);   
            try
            {
                if (LegderClassList.SelectedValue.ToString() == "1" || LegderClassList.SelectedValue.ToString() == "2" || LegderClassList.SelectedValue.ToString() == "3" || LegderClassList.SelectedValue.ToString() == "4")
                {
                    Bill window = new Bill(selectedClass);
                    window.ShowDialog();      
                }
                else
                {
                    Bill window = new Bill(selectedClass);
                    window.ComputerTextBox.Visibility = System.Windows.Visibility.Visible;
                    window.MusicTextBox.Visibility = System.Windows.Visibility.Visible;
                    window.ComputerLabel.Visibility = System.Windows.Visibility.Visible;
                    window.MusicLabel.Visibility = System.Windows.Visibility.Visible;
                    window.ComputerTextBox1.Visibility = System.Windows.Visibility.Visible;
                    window.MusicTextBox1.Visibility = System.Windows.Visibility.Visible;
                    window.ComputerLabel1.Visibility = System.Windows.Visibility.Visible;
                    window.MusicLabel1.Visibility = System.Windows.Visibility.Visible;
                    window.ShowDialog();
                }
            }
            catch (NullReferenceException)
            {
                this.ShowMessageAsync("Error","Please Select a Class",MessageDialogStyle.AffirmativeAndNegative);
                //MessageBox.Show("Please Select a Class");
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error",ex.Message);
            }
            
        }

        private void PayFeesButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PottersEntities db = new PottersEntities();
                int admissionNo = int.Parse(StudentListBox.SelectedValue.ToString());
                Student newStudent = db.Students.AsNoTracking().Single(admNo => admNo.AdmissionNo == admissionNo);
                PayFees window = new PayFees(newStudent);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void PayFeesButtonTest_Click_1(object sender, RoutedEventArgs e)
        {
            PayFees window = new PayFees();
            window.Show();      
        }

        private void Settings_Click_1(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();
            window.ShowDialog();
           
        }

        private void Promote_Click_1(object sender, RoutedEventArgs e)
        {
            var db = new PottersEntities();
            int classId = int.Parse(LegderClassList.SelectedValue.ToString());
            StudentClass selectedClass = db.StudentClasses.Find(LegderClassList.SelectedValue);   
            Promote window = new Promote(selectedClass);
            window.ShowDialog();
        }

        private void ParentDetailsMenu_Click_1(object sender, RoutedEventArgs e)
        {
            var stud = (Student)StudentListBox.SelectedItem;
            var par = stud.Parent;         
            MainTab.SelectedIndex = 3;
            var match = ParentListBox.Items.Cast<Parent>();
            int x = 0;
            foreach (var item in match)
            {    
                if (item.ParentId.Equals(par.ParentId))
                {
                    break;
                }
                x++;
            }
            ParentListBox.SelectedIndex = x;

            
        }

        private void PaymentButton_Click_1(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentClassLedger.SelectedItem;
            Transactions window = new Transactions(student);
            window.ShowDialog();
            //var transaction = student.Account.Transactions;
        }

        private void PaymentMadeMenu_Click_1(object sender, RoutedEventArgs e)
        {
            PottersEntities db = new PottersEntities();
            int admissionNo = int.Parse(StudentListBox.SelectedValue.ToString());
            Student newStudent = db.Students.AsNoTracking().Single(admNo => admNo.AdmissionNo == admissionNo);
            Transactions window = new Transactions(newStudent);
            window.ShowDialog();
        }

        private void Refresh_Click_1(object sender, RoutedEventArgs e)
        {
            string term = TermTermTextBox.Text;
            string year = SessionTermTextBox.Text;

            string currentTerm = AcademicSession.getCurrentTerm().Term;
            string currentYear = AcademicSession.getCurrentTerm().AcademicYear;
            if (term.Equals(currentTerm) && year.Equals(currentYear))
            {
                AcademicSession.updateFeesPaid();
                AcademicSession.updateFeesPayable();
                AcademicSession.updateFeesUnpaid();
                AcademicSession.updateNewStudentCount();

                TotalFeesPayableTextBox.Text = AcademicSession.getCurrentTerm().FeesPayable.ToString();
                TotalFeesPaidTextBox.Text = AcademicSession.getCurrentTerm().FeesPaid.ToString();
                TotalFeesUnpaidTextBox.Text = AcademicSession.getCurrentTerm().FeesUnpaid.ToString();
                NewStudentTextBox.Text = AcademicSession.getCurrentTerm().NewStudentCount.ToString();
            }
            else
            {
                MessageBox.Show("You can only refresh a current term","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                SessionTermTextBox.Text = "";
            }
            
        }

        private void SearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            string term = TermTermTextBox.Text;
            string year = SessionTermTextBox.Text;
            string currentTerm = AcademicSession.getCurrentTerm().Term;
            string currentYear = AcademicSession.getCurrentTerm().AcademicYear;

            if (!(term.Equals(currentTerm) && year.Equals(currentYear)))
            {
                var allSessions = AcademicSession.getAcademicSessions();
                var sessionSearch = allSessions.FirstOrDefault(x => x.Term.Equals(term) && x.AcademicYear.Equals(year));

                if (sessionSearch != null)
                {
                    TotalFeesPayableTextBox.Text = sessionSearch.FeesPayable.ToString();
                    TotalFeesPaidTextBox.Text = sessionSearch.FeesPaid.ToString();
                    TotalFeesUnpaidTextBox.Text = sessionSearch.FeesUnpaid.ToString();
                    NewStudentTextBox.Text = sessionSearch.NewStudentCount.ToString();
                }
                else
                {
                    MessageBox.Show("Invalid term and session, this session does not exist","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
                
            }

            
        }

        private void UpdateStudent_Click_1(object sender, RoutedEventArgs e)
        {
            PottersEntities db = new PottersEntities();
            string admNo = admissionnoTextBox.Text;
            Student _student = db.Students.First(x => x.AdmissionNumber == admNo);
            int admissionNo = _student.AdmissionNo;

            string newLastName = lastNameTextBox.Text;
            string newFirstName = firstNameTextBox.Text;
            string newMiddleName = middleNameTextBox.Text;
            string newSex = sexTextBox.Text;
            string newReligionComboBox = ReligionComboBox.Text;
            string newAddress = addressTextBox.Text;
            string newPreviousSchool = PreviousSchoolName.Text;
            string newPreviousClass = PreviousSchoolClassList.Text;
            string newClassDivision = ClassDivisionTextBox.Text;
            bool? SchoolBus = SchoolBusUpdateCheckBox.IsChecked;
            double SchoolBusFees = double.Parse(SchoolBusTextBox.Text);

            if (SchoolBusFees > 0)
            {
                SchoolBus = true;
            }

            try
            {
                Student student = new Student();
                student.updateStudent(admissionNo, newFirstName, newLastName, newMiddleName, newSex, newReligionComboBox, newAddress, newPreviousSchool, newPreviousClass, newClassDivision, SchoolBus, SchoolBusFees);
                string message = newLastName + " " +newFirstName + "'s details was updated successfuly";
                this.ShowMessageAsync("Notification", message);
            }
            catch (FormatException)
            {
                this.ShowMessageAsync("Error", "The field you tried to update was filled incorrectly");
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
            
        }

        private void CancelRegistration_Click_1(object sender, RoutedEventArgs e)
        {
            FirstName.Clear();
            LastName.Clear();
            MiddleName.Clear();
            Sex.Text = "";
            Address.Clear();
            ClassList.Text = "";
            DateOfBirth.Text = "";
            AdmissionDate.Text = "";
            SchoolBusCheckBox.IsChecked = false;
            ReligionList.Text = "";
            SchoolBusFeesTextBox.IsEnabled = false;

            ParentId.SelectedIndex = -1;
            NewParent.IsChecked = true;
            FatherTitleList.SelectedIndex = 0;
            FatherFirstName.Clear();
            FatherPhone.Clear();
            MotherTitleList.SelectedIndex = 0;
            MotherFirstName.Clear();
            MotherPhone.Clear();
            Email.Clear();

            PreviousSchool.Clear();
            PreviousClassList.Text = "";

            firstNameTextBox.Focus();
        }

        private async void Password_KeyDown_1(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter )
            {
                if (!string.IsNullOrWhiteSpace(Password.Password))
                {
                    PottersEntities db = new PottersEntities();
                    string username = "user";
                    string password = Password.Password.Trim();
                    SchoolManagerModel.Login newLogin = new SchoolManagerModel.Login();
                    bool success = newLogin.login(username, password);
                    if (success)
                    {
                        //await this.ShowMessageAsync("Login", "Login Successful");
                        RegisterNewStudentTab.IsEnabled = true;
                        StudentsTab.IsEnabled = true;
                        ParentsTab.IsEnabled = true;
                        LedgerTab.IsEnabled = true;
                        PayFees.IsEnabled = true;
                        Settings.IsEnabled = true;
                        Logout.IsEnabled = true;
                        RegisterNewStudentHome.IsEnabled = true;
                        PayFeesHome.IsEnabled = true;
                        StudentsHome.IsEnabled = true;
                        LedgerHome.IsEnabled = true;
                        Password.Clear();

                        Password.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        await this.ShowMessageAsync("Login Error", "Incorrect Username & Password!!");
                        Password.Clear();
                        Password.Focus();
                    }
                }
               
            }
        }

        private bool UserLogin()
        {
            PottersEntities db = new PottersEntities();
            string username = "user";
            string password = Password.Password;
            SchoolManagerModel.Login newLogin = new SchoolManagerModel.Login();
            
            bool success = newLogin.login(username, password);
            if (success)
            {
                RegisterNewStudentTab.IsEnabled = true;
                StudentsTab.IsEnabled = true;
                ParentsTab.IsEnabled = true;
                LedgerTab.IsEnabled = true;
                PayFees.IsEnabled = true;
                Settings.IsEnabled = true;
                Logout.IsEnabled = true;
                RegisterNewStudentHome.IsEnabled = true;
                PayFeesHome.IsEnabled = true;
                StudentsHome.IsEnabled = true;
                LedgerHome.IsEnabled = true;
                Password.Clear();

                Password.Visibility = System.Windows.Visibility.Hidden;
                return true;
            }
            else
            {
                Password.Clear();
                return false;
            }
        }
        private void RegisterNewStudentHome_Click_1(object sender, RoutedEventArgs e)
        {
            MainTab.SelectedIndex = 1;
        }

        private void PayFeesHome_Click_1(object sender, RoutedEventArgs e)
        {
            PayFees window = new PayFees();
            window.Show(); 
        }

        private void StudentsHome_Click_1(object sender, RoutedEventArgs e)
        {
            MainTab.SelectedIndex = 2;
        }

        private void LedgerHome_Click_1(object sender, RoutedEventArgs e)
        {
            MainTab.SelectedIndex = 4;
        }

        private void Logout_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterNewStudentTab.IsEnabled = false;
            StudentsTab.IsEnabled = false;
            ParentsTab.IsEnabled = false;
            LedgerTab.IsEnabled = false;
            PayFees.IsEnabled = false;
            Settings.IsEnabled = false;
            Logout.IsEnabled = false;
            RegisterNewStudentHome.IsEnabled = false;
            PayFeesHome.IsEnabled = false;
            StudentsHome.IsEnabled = false;
            LedgerHome.IsEnabled = false;

            Password.Visibility = System.Windows.Visibility.Visible;
            Password.Focus();
            MainTab.SelectedIndex = 0;


            


        }

        private void UpdateParent_Click_1(object sender, RoutedEventArgs e)
        {
            

            try
            {
                PottersEntities db = new PottersEntities();
                string parNo = PIN.Text;
                Parent _parent = db.Parents.First(x => x.ParentIdentificationNo == parNo);
                int parentId = _parent.ParentId;

                string newFatherTitle = uFatherTitle.Text;
                string newFatherFirstName = uFatherFirstName.Text;
                string newFatherPhone = uFatherPhone.Text;
                string newFatherLastName = uFatherLastName.Text;
                string newMotherTitle = uMotherTitle.Text;
                string newMotherFirstName = uMotherFirstName.Text;
                string newMotherPhone = uMotherPhone.Text;
                string newMotherLastName = uMotherLastName.Text;
                string newEmail = uEmail.Text;

                Parent parent = new Parent();
                parent.updateParent(parentId,newFatherTitle,newFatherFirstName,newFatherLastName,newFatherPhone,newMotherTitle,newMotherFirstName,newMotherLastName,newMotherPhone,newEmail);
                string message = parNo + "'s details was updated successfuly";
                this.ShowMessageAsync("Notification", message);
            }
            catch (FormatException)
            {
                this.ShowMessageAsync("Error", "The field you tried to update was filled incorrectly");
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
        }

        private void MainWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.LeftCtrl))
            {
                MainWindow1_Loaded(sender, e);
            }
            else if (e.Key.Equals(Key.RightCtrl) && e.Key.Equals(Key.R))
            {
                MainWindow1_Loaded(sender, e);
            }
        }

        private void SchoolBusCheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            //SchoolBusFeesTextBox.IsEnabled = true;
        }

        private void SchoolBusCheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            if (SchoolBusFeesTextBox.IsEnabled)
            {
                SchoolBusFeesTextBox.IsEnabled = false;
            }
            else
            {
                SchoolBusFeesTextBox.IsEnabled = true ;
            }
        }

        

        

        
    }
}

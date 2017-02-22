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
using MahApps.Metro.Controls.Dialogs;
using SchoolManagerModel;
using System.Data;

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public string Session { get; set; }
        public string Term { get; set; }

       

        public Settings()
        {
            InitializeComponent();
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            var currentTerm = AcademicSession.getCurrentTerm();
            SessionTextBox.Text = currentTerm.AcademicYear;
            TermComboBox.Text = currentTerm.Term;

            if (!MainWindow.NewStudentFeature)
            {
                NFSswitch.IsChecked = false;
            }
            //if (TermComboBox.SelectedIndex == 0)
            //{
            //    SessionTextBox.Text = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1);
            //}
            //SessionTextBox.Text = currentTerm.AcademicYear; 
        }

        private async void BeginTerm_Click_1(object sender, RoutedEventArgs e)
        {
            
            var currentTerm = AcademicSession.getCurrentTerm();
            string _currentTerm = currentTerm.Term;

            if (_currentTerm == TermComboBox.Text)
            {
                string message = "Please pick a new term in the " + SessionTextBox.Text + " session";
                await this.ShowMessageAsync("Warning",message);
                //MessageBox.Show("Please pick a new term in the " + SessionTextBox.Text + " session", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
            else if (_currentTerm == "3rd" && TermComboBox.SelectedIndex == 1)
            {
                string message = "You cannot skip a term, please pick the 1st term in the " + SessionTextBox.Text + " session";
                //MessageBox.Show("You cannot skip a term, please pick the 1st term in the " + SessionTextBox.Text + " session", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
            else if ( _currentTerm == "1st" && TermComboBox.SelectedIndex == 2)
            {
                string message = "You cannot skip a term, please pick the 2nd term in the " + SessionTextBox.Text + " session";
                await this.ShowMessageAsync("Warning", message);
                //MessageBox.Show("You cannot skip a term, please pick the 2nd term in the " + SessionTextBox.Text + " session", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
            else if (_currentTerm == "2nd" && TermComboBox.SelectedIndex == 0)
            {
                string message = "You cannot skip a term, please pick the 3rd term in the " + SessionTextBox.Text + " session";
                await this.ShowMessageAsync("Warning", message);
                //MessageBox.Show("You cannot skip a term, please pick the 3rd term in the " + SessionTextBox.Text + " session", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
            else if (currentTerm.AcademicYear == SessionTextBox.Text && TermComboBox.SelectedIndex == 0)
            {
                string message = "Please type in a new session, this term already exists!";
                await this.ShowMessageAsync("Warning", message);
                //MessageBox.Show("Please type in a new session, this term already exists!", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
            else
            {
                await Confirmation(currentTerm);
                //MessageDialogResult result = await this.ShowMessageAsync("Are you sure you want to end the " + currentTerm.Term + " term of " + currentTerm.AcademicYear + " session ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                //try
                //{
                //    if (result == MessageBoxResult.OK)
                //    {
                //        //end previous term
                //        PottersEntities db = new PottersEntities();
                //        AcademicSession term = db.AcademicSessions.AsNoTracking().Single(x => x.AcademicSessionId == currentTerm.AcademicSessionId);
                //        AcademicSession.updateFeesPaid();
                //        AcademicSession.updateFeesPayable();
                //        AcademicSession.updateFeesUnpaid();
                //        AcademicSession.resolveAccounts();

                //        AcademicSession.endTerm();

                //        //create new term
                //        this.Session = SessionTextBox.Text;
                //        this.Term = TermComboBox.Text;
                //        DialogResult = true;
                //        AcademicSession newAcademicSession = new AcademicSession(this.Session, this.Term);
                //        MessageBox.Show("Welcome to the " + this.Term + " term of the " + this.Session + " session");

                //        //enforce update of bill
                //        if (TermComboBox.SelectedIndex == 0)
                //        {
                //            MessageBox.Show("Please update all bills for the new term!", "Notification", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //        }

                //        this.Close();
                //    }
                //    else
                //    {
                //        this.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }              
        }
        private async Task Confirmation(AcademicSession currentTerm)
        {
            MessageDialogResult result = await this.ShowMessageAsync("Warning","Are you sure you want to end the " + currentTerm.Term + " term of " + currentTerm.AcademicYear + " session ?");
            try
            {
                if (result == MessageDialogResult.Affirmative)
                {
                    //end previous term
                    PottersEntities db = new PottersEntities();
                    AcademicSession term = db.AcademicSessions.AsNoTracking().Single(x => x.AcademicSessionId == currentTerm.AcademicSessionId);
                    AcademicSession.updateFeesPaid();
                    AcademicSession.updateFeesPayable();
                    AcademicSession.updateFeesUnpaid();
                    AcademicSession.resolveAccounts();

                    AcademicSession.endTerm();

                    //create new term
                    this.Session = SessionTextBox.Text;
                    this.Term = TermComboBox.Text;
                    DialogResult = true;
                    AcademicSession newAcademicSession = new AcademicSession(this.Session, this.Term);
                    await this.ShowMessageAsync("Notification","Welcome to the " + this.Term + " term of the " + this.Session + " session");
                    //MessageBox.Show("Welcome to the " + this.Term + " term of the " + this.Session + " session");

                    //enforce update of bill
                    if (TermComboBox.SelectedIndex == 0)
                    {
                       await this.ShowMessageAsync( "Notification","Please update all bills for the new term!");
                    }

                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Error", ex.Message);
            }
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            if (NFSswitch.IsChecked == false)
            {
                MainWindow.NewStudentFeature = false;
            }
            else if (NFSswitch.IsChecked == true)
            {
                MainWindow.NewStudentFeature = true;
            }
           
        }

        private void SaveNewPassword_Click_1(object sender, RoutedEventArgs e)
        {
            SchoolManagerModel.Login login = new SchoolManagerModel.Login();
            string username = Username.Text.Trim();
            string oldpassword = OldPassword.Password.Trim();
            string newpassword = NewPassword.Password.Trim();
            string confirmpassword = ConfirmPassword.Password.Trim();
            if (newpassword.Equals(confirmpassword))
            {
                bool success = login.changePassword(username,newpassword,oldpassword);
                if (success)
                {
                    this.ShowMessageAsync("Notification","Password was successfully changed");
                }
                else
                {
                   this.ShowMessageAsync("Error","Incorrect Password and Username!");
                }
            }
            else
            {
                this.ShowMessageAsync("Error","Password not confirmed!");
            }
            
        }

        private void CancelPasswordChange_Click_1(object sender, RoutedEventArgs e)
        {
            Username.Clear();
            OldPassword.Clear();
            NewPassword.Clear();
            ConfirmPassword.Clear();
        }

    }
}

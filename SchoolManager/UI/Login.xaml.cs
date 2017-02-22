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
using MahApps.Metro.Controls;

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();

        }
        MainWindow window = new MainWindow();
        

        private void LoginButton_Click_1(object sender, RoutedEventArgs e)
        {
            PottersEntities db = new PottersEntities();
            string username = Username.Text.Trim();
            string password = Password.Password.Trim();
            SchoolManagerModel.Login newLogin = new SchoolManagerModel.Login();
            bool success = newLogin.login(username, password);

            if (success)
            {
                window.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Incorrect Username & Password");
            }
        }
    }
}

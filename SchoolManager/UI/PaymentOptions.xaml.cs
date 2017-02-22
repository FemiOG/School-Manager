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

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for PaymentOptions.xaml
    /// </summary>
    public partial class PaymentOptions : MetroWindow
    {
        public bool ExerciseBook = false;
        public bool TextBook = false;

        public PaymentOptions()
        {
            InitializeComponent();
        }

        private void Ok_Click_1(object sender, RoutedEventArgs e)
        {   
            if ((bool)TextBookChecked.IsChecked)
            {
                TextBook = true;
            }
            DialogResult = true;
            Close();
        }
    }
}

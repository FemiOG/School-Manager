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
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : MetroWindow
    {
        public Student student { get; set; }
        public Transactions()
        {
            InitializeComponent();
        }
        public Transactions(Student newStudent)
        {
            InitializeComponent();
            this.student = newStudent;
        }

        private void TransactionsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int thisTermId = AcademicSession.getCurrentTerm().AcademicSessionId;
            var transaction = student.Account.Transactions.Where(x => x.AcademicSession.AcademicSessionId.Equals(thisTermId));
            TransactionsList.ItemsSource = transaction;
        }

    }
}

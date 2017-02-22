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
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

namespace SchoolManager.UI
{
    /// <summary>
    /// Interaction logic for Promote.xaml
    /// </summary>
    public partial class Promote : MetroWindow
    {
        public StudentClass thisClass { get; set; }
        public Promote()
        {
            InitializeComponent();
        }
        
        public Promote(StudentClass selectedClass)
        {
            InitializeComponent();
            this.thisClass = selectedClass; 
        }

        private void PromoteWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            ClassLabel.Content = thisClass.Name;
            StudentPromotionList.ItemsSource = thisClass.Students;
            //ObservableCollection<StudentClass> femi = StudentClass.getClassList();
            //ClassPromoteCombo.ItemsSource = femi;
        }

        private void PromoteStudents_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PottersEntities db = new PottersEntities();
                //int x = ClassPromoteCombo.SelectedIndex;
                
                //StudentClass currentClass = (StudentClass)ClassPromoteCombo.SelectedItem;
                int id = thisClass.StudentClassId;
                id++;

                StudentClass newClass = db.StudentClasses.AsNoTracking().Single(studentClass => studentClass.StudentClassId.Equals(id));

                foreach (var item in StudentPromotionList.SelectedItems)
                {
                    Student promotedStudent = (Student)item;
                    promotedStudent.Promote(promotedStudent, newClass);

                    MessageBox.Show("Success");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void PromoteCheck_Checked(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

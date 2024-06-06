using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models.Complaints;
using Models.Complaints.Requests;

namespace wpf_ui
{
    /// <summary>
    /// Interaction logic for ComplaintDetailsWindow.xaml
    /// </summary>
    public partial class ComplaintDetailsWindow : Window
    {
        public ComplaintDetailsWindow(Complaint complaint)
        {
            InitializeComponent();

            mainGrid.DataContext = complaint;
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Exit_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
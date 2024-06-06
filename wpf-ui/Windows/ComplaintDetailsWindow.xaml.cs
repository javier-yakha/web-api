using System.Net.Http;
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
using wpf_ui.Windows;

namespace wpf_ui
{
    /// <summary>
    /// Interaction logic for ComplaintDetailsWindow.xaml
    /// </summary>
    public partial class ComplaintDetailsWindow : Window
    {
        private HttpClient Client;
        private Complaint complaint;
        public ComplaintDetailsWindow(HttpClient client, Complaint complaint)
        {
            Client = client;
            InitializeComponent();

            this.complaint = complaint;
            mainGrid.DataContext = complaint;
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {
            if (complaint != null)
            {
                this.Close();
                (new UpdateComplaintWindow(Client, complaint)).Show();
            }
        }
    }
}
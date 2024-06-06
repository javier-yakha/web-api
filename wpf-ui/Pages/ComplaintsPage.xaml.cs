using Models;
using Models.Complaints;
using Models.Complaints.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_ui.Pages
{
    /// <summary>
    /// Interaction logic for SeeAllComplaintsPage.xaml
    /// </summary>
    public partial class ComplaintsPage : Page
    {
        private readonly HttpClient Client;
        ComplaintDetailsWindow detailsWindow;
        public List<Complaint> complaints = [];
        public ComplaintsPage(HttpClient client)
        {
            Client = client;

            LoadComplaintsAsync();

            InitializeComponent();
        }
            

        private async Task LoadComplaintsAsync()
        {
            try
            {
                ResponseStatus<ComplaintList>? response = await Client.GetFromJsonAsync<ResponseStatus<ComplaintList>>("complaint/read/all/bydate");

                if (response is not null && response.Data is not null && response.Success && response.Data.Complaints is not null)
                {
                    complaints = response.Data.Complaints;
                    ComplaintsList.ItemsSource = response.Data.Complaints;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            Complaint complaint = (Complaint)btn.CommandParameter;

            if (detailsWindow is not null)
            {
                detailsWindow.Close();
            }
            detailsWindow = new ComplaintDetailsWindow(complaint);
            detailsWindow.Show();
        }
    }
}

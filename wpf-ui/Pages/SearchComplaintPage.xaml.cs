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
using Models;
using Responses = Models.Complaints.Responses;
using Models.Complaints;

namespace wpf_ui.Pages
{
    /// <summary>
    /// Interaction logic for SearchComplaintPage.xaml
    /// </summary>
    public partial class SearchComplaintPage : Page
    {
        private readonly HttpClient Client;
        ComplaintDetailsWindow detailsWindow;
        public List<Complaint> complaints = [];
        public SearchComplaintPage(HttpClient client)
        {
            Client = client;
            InitializeComponent();
        }


        private async Task SearchComplaintsAsync(string name)
        {
            try
            {
                var response = await Client.PostAsJsonAsync("complaint/read/search/", name);
                ResponseStatus<Responses.ComplaintList>? content = await response.Content.ReadFromJsonAsync<ResponseStatus<Responses.ComplaintList>>();

                if (content is not null && content.Data is not null && content.Success && content.Data.Complaints is not null)
                {
                    complaints = content.Data.Complaints;
                    ComplaintsList.ItemsSource = content.Data.Complaints;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DetailsBtnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            Complaint complaint = (Complaint)btn.CommandParameter;

            if (detailsWindow is not null)
            {
                detailsWindow.Close();
            }
            detailsWindow = new ComplaintDetailsWindow(Client, complaint);
            detailsWindow.Show();
        }
    }
}

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

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBar.Text;
            if (!string.IsNullOrEmpty(query))
            {
                await SearchComplaintsAsync(query);
            }
        }

        private async Task SearchComplaintsAsync(string name)
        {
            try
            {
                var response = await Client.GetFromJsonAsync<ResponseStatus<Responses.ComplaintList>>($"complaint/read/search?personName={name}");

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
    }
}

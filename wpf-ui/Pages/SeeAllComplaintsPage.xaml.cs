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
    public partial class SeeAllComplaintsPage : Page
    {
        private readonly HttpClient Client;
        public SeeAllComplaintsPage(HttpClient client)
        {
            Client = client;
            InitializeComponent();
        }

        private async Task<bool> GetAllComplaintsAsync()
        {
            try
            {
                ResponseStatus<ComplaintList>? response = await Client.GetFromJsonAsync<ResponseStatus<ComplaintList>>("complaint/read/all/bydate");

                if (response is not null && response.Success)
                {
                    MessageBox.Show(response.Data?.Total.ToString());

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}

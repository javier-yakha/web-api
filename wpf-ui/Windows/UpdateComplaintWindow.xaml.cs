using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using Models;
using Models.Complaints;
using Requests = Models.Complaints.Requests;
using Responses = Models.Complaints.Responses;

namespace wpf_ui.Windows
{
    /// <summary>
    /// Interaction logic for UpdateComplaintWindow.xaml
    /// </summary>
    public partial class UpdateComplaintWindow : Window
    {
        private HttpClient Client;
        private readonly string Id;
        public UpdateComplaintWindow(HttpClient client, Complaint complaint)
        {
            Client = client;
            Id = complaint.Id;

            InitializeComponent();

            LocationComboBox.ItemsSource = Enum.GetValues<Locations>();
            CategoryComboBox.ItemsSource = Enum.GetValues<Categories>();

            NameTextBox.Text = complaint.PersonName;
            ApartmentTextBox.Text = complaint.PersonApartmentCode;
            LocationComboBox.SelectedItem = complaint.Location;
            CategoryComboBox.SelectedItem = complaint.Category;
            DescriptionTextBox.Text = complaint.Description;
        }

        private async void UpdateBtn(object sender, RoutedEventArgs e)
        {
            ClearErrorLabels();

            if (!ValidateForm())
            {
                MessageBox.Show("Form Invalid, please fill it properly.");

                return;
            }

            if (await UpdateComplaintAsync())
            {
                ClearForm();
                MessageBox.Show("Complain once, shame on them. Complain twice, shame on you.");
                this.Close();
                return;
            }

            MessageBox.Show("Error updating the database, please try again.");
        }

        private void ClearErrorLabels()
        {
            NameErrorLabel.Content = "";
            ApartmentErrorLabel.Content = "";
            LocationErrorLabel.Content = "";
            CategoryErrorLabel.Content = "";
            DescriptionErrorLabel.Content = "";
        }

        private bool ValidateForm()
        {
            bool sendStatus = true;

            string name = NameTextBox.Text;
            string apartment = ApartmentTextBox.Text;
            int location = LocationComboBox.SelectedIndex;
            int category = CategoryComboBox.SelectedIndex;
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(name))
            {
                sendStatus = false;
                NameErrorLabel.Content = "Please enter a name.";
            }
            else if (name.Length > 50)
            {
                sendStatus = false;
                NameErrorLabel.Content = "Name is too long, max 50 characters.";
            }

            if (string.IsNullOrEmpty(apartment))
            {
                sendStatus = false;
                ApartmentErrorLabel.Content = "Please enter an apartment.";
            }
            else if (apartment.Length > 50)
            {
                sendStatus = false;
                ApartmentErrorLabel.Content = "Apartment is too long, max 50 characters.";
            }

            if (location == -1)
            {
                sendStatus = false;
                LocationErrorLabel.Content = "Please select a location.";
            }

            if (category == -1)
            {
                sendStatus = false;
                CategoryErrorLabel.Content = "Please select a category.";
            }

            if (string.IsNullOrEmpty(description))
            {
                sendStatus = false;
                DescriptionErrorLabel.Content = "Please enter a description.";
            }
            else if (description.Length > 2000)
            {
                sendStatus = false;
                DescriptionErrorLabel.Content = "Description is too long, max 2000 characters.";
            }

            return sendStatus;
        }

        private Requests.UpdateComplaint GetFormData()
        {
            return new Requests.UpdateComplaint()
            {
                Id = this.Id,
                PersonName = NameTextBox.Text,
                PersonApartmentCode = ApartmentTextBox.Text,
                Location = (Locations)LocationComboBox.SelectedIndex,
                Category = (Categories)CategoryComboBox.SelectedIndex,
                Description = DescriptionTextBox.Text
            };
        }

        private async Task<bool> UpdateComplaintAsync()
        {
            Requests.UpdateComplaint complaint = GetFormData();
            try
            {
                var response = await Client.PostAsJsonAsync("complaint/update/data", complaint);

                ResponseStatus<Responses.UpdatedStatus>? content = await response.Content.ReadFromJsonAsync<ResponseStatus<Responses.UpdatedStatus>>();

                return content is not null && content.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        private void ClearForm()
        {
            NameTextBox.Text = "";
            ApartmentTextBox.Text = "";
            LocationComboBox.SelectedItem = null;
            CategoryComboBox.SelectedItem = null;
            DescriptionTextBox.Text = "";
        }


    }
}

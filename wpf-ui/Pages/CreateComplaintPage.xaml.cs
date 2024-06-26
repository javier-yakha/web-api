﻿using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Net.Http.Json;
using Models;
using Models.Complaints;
using Requests = Models.Complaints.Requests;
using Responses = Models.Complaints.Responses;

namespace wpf_ui.Pages
{
    /// <summary>
    /// Interaction logic for CreateComplaintPage.xaml
    /// </summary>
    public partial class CreateComplaintPage : Page
    {
        private readonly HttpClient Client;
        public CreateComplaintPage(HttpClient client)
        {
            Client = client;
            InitializeComponent();
            LocationComboBox.ItemsSource = Enum.GetValues<Locations>();
            CategoryComboBox.ItemsSource = Enum.GetValues<Categories>();
            
            ComplainBtn.Click += ComplainBtn_Click;
        }

        private async void ComplainBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorLabels();

            if (!ValidateForm())
            {
                MessageBox.Show("Form Invalid, please fill it properly.");

                return;
            }

            if (await CreateComplaintAsync())
            {
                ClearForm();
                MessageBox.Show("You have become complainer, the destroyer of fun.");

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

        private Requests.CreateComplaint GetFormData()
        {
            return new Requests.CreateComplaint()
            {
                PersonName = NameTextBox.Text,
                PersonApartmentCode = ApartmentTextBox.Text,
                Location = (Locations)LocationComboBox.SelectedIndex,
                Category = (Categories)CategoryComboBox.SelectedIndex,
                Description = DescriptionTextBox.Text
            };
        }

        private async Task<bool> CreateComplaintAsync()
        {
            Requests.CreateComplaint complaint = GetFormData();
            try
            {
                var response = await Client.PostAsJsonAsync("complaint/create", complaint);

                ResponseStatus<Responses.CreateComplaint>? content = await response.Content.ReadFromJsonAsync<ResponseStatus<Responses.CreateComplaint>>();

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

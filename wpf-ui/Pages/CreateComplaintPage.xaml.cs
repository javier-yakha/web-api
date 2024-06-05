using Models.Complaints.Requests;
using Models.Complaints;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_ui.Pages
{
    /// <summary>
    /// Interaction logic for CreateComplaintPage.xaml
    /// </summary>
    public partial class CreateComplaintPage : Page
    {
        public CreateComplaintPage()
        {
            InitializeComponent();
            foreach (Locations location in Enum.GetValues<Locations>())
            {
                LocationComboBox.Items.Add(location);
            }
            foreach (Categories category in Enum.GetValues<Categories>())
            {
                CategoryComboBox.Items.Add(category);
            }
            ComplainBtn.Click += ComplainBtn_Click;
        }

        private async void ComplainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                MessageBox.Show("You have become complainer, the destroyer of fun.");
                CreateComplaint? complaint = new()
                {
                    PersonName = NameTextBox.Text,
                    PersonApartmentCode = ApartmentTextBox.Text,
                    Location = (Locations)LocationComboBox.SelectedIndex,
                    Category = (Categories)CategoryComboBox.SelectedIndex,
                    Description = DescriptionTextBox.Text
                };

                await ApiCall(complaint);

                NameTextBox.Text = "";
                ApartmentTextBox.Text = "";
                LocationComboBox.SelectedItem = null;
                CategoryComboBox.SelectedItem = null;
                DescriptionTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Please correctly fill the form.");
            }

        }
        private bool ValidateForm()
        {
            bool sendStatus = true;

            NameErrorLabel.Content = "";
            ApartmentErrorLabel.Content = "";
            LocationErrorLabel.Content = "";
            CategoryErrorLabel.Content = "";
            DescriptionErrorLabel.Content = "";

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

        private async Task<bool> ApiCall(CreateComplaint complaint)
        {
            await Task.Delay(100);
             
            return true;
        }

    }

}

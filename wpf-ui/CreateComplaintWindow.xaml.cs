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
    /// Interaction logic for CreateComplaintWindow.xaml
    /// </summary>
    public partial class CreateComplaintWindow : Window
    {
        public CreateComplaintWindow()
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
            bool sendStatus = true;
            NameErrorLabel.Content = "";
            ApartmentErrorLabel.Content = "";
            DescriptionErrorLabel.Content = "";
            
            string name = NameTextBox.Text;
            string apartment = ApartmentTextBox.Text;
            Locations location = (Locations)LocationComboBox.SelectedIndex;
            Categories category = (Categories)CategoryComboBox.SelectedIndex;
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(name))
            {
                sendStatus = false;
                NameErrorLabel.Content = $"Please enter a name.";
            }
            else if (name.Length > 50)
            {
                sendStatus = false;
                NameErrorLabel.Content = $"Name {name} is too long, max 50 characters.";
            }

            if (string.IsNullOrEmpty(apartment))
            {
                sendStatus = false;
                ApartmentErrorLabel.Content = $"Please enter an apartment.";
            }
            else if (apartment.Length > 50)
            {
                sendStatus = false;
                ApartmentErrorLabel.Content = $"Apartment {apartment} is too long, max 50 characters.";
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

            if (sendStatus)
            {
                MessageBox.Show("You have become complainer, the destroyer of fun.");
                CreateComplaint? complaint = new()
                {
                    PersonName = name,
                    PersonApartmentCode = apartment,
                    Location = location,
                    Category = category,
                    Description = description
                };
                await ApiCall(complaint);
            }
            else
            {
                MessageBox.Show("Please correctly fill the form.");
            }

        }
        private bool CorrectForm()
        {
            return true;
        }

        private async Task<bool> ApiCall(CreateComplaint complaint)
        {
            await Task.Delay(1000);
            return true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
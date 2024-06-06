using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpf_ui.Pages;

namespace wpf_ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient Client = new();
        CreateComplaintPage? CreateComplaintView;
        public MainWindow()
        {
            Client.BaseAddress = new Uri("https://localhost:44381");
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bye!");
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RadioButton_Tab_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;

            switch (radioButton.Name)
            {
                case "HomeTab":
                    ContentFrame.Navigate(null);
                    break;
                case "ComplaintsTab":
                    ContentFrame.Navigate(new ComplaintsPage(Client));
                    break;
                case "NewComplaintTab":
                    CreateComplaintView = CreateComplaintView ?? new CreateComplaintPage(Client);
                    ContentFrame.Navigate(CreateComplaintView);
                    break;
                case "SearchTab":
                    ContentFrame.Navigate(null);
                    break;
                default:
                    ContentFrame.Navigate(null);
                    break;
            }

        }
    }
}

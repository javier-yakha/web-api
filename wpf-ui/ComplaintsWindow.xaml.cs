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
using System.Windows.Shapes;
using Models.Complaints;
 
namespace wpf_ui
{
    /// <summary>
    /// Interaction logic for ComplaintsWindow.xaml
    /// </summary>
    public partial class ComplaintsWindow : Window
    {
        List<Complaint> complaints = new(20);
        public ComplaintsWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                Complaint complaint = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    PersonName = $"S{i * i}r{i + i}es{i}{i}",
                    PersonApartmentCode = $"{i}",
                    Location = (Locations)(i % Enum.GetValues<Locations>().Length),
                    Category = (Categories)(i % Enum.GetValues<Categories>().Length),
                    Description = $"random text {i * i * i}, {i}, {i + i}",
                    CurrentStatus = (ActiveStatus)(i % Enum.GetValues<ActiveStatus>().Length),
                    DateActivated = DateTime.UtcNow.AddDays(-i),
                    LastUpdated = DateTime.UtcNow
                };
                complaints.Add(complaint);
            }
        }
    }
}

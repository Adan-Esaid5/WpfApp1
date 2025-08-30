using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    public partial class VehicleCompositionCheckDialog : Window
    {
        public string ProjectNumber { get; set; }
        public ObservableCollection<VehicleComposition> VehicleCompositions { get; set; }
        public bool DataSaved { get; private set; }

        public VehicleCompositionCheckDialog(string projectNumber)
        {
            InitializeComponent();

            ProjectNumber = projectNumber;
            DataSaved = false;

            // Initialize with sample data - in a real application, this would come from your database
            VehicleCompositions = new ObservableCollection<VehicleComposition>
            {
                new VehicleComposition { Road = "כביש 1", Direction = "מזרח", Type = "רכב פרטי" },
                new VehicleComposition { Road = "כביש 1", Direction = "מערב", Type = "רכב פרטי" },
                new VehicleComposition { Road = "כביש 1", Direction = "מזרח", Type = "משאית" },
                new VehicleComposition { Road = "כביש 1", Direction = "מערב", Type = "משאית" },
                new VehicleComposition { Road = "כביש 4", Direction = "צפון", Type = "רכב פרטי" },
                new VehicleComposition { Road = "כביש 4", Direction = "דרום", Type = "רכב פרטי" },
                new VehicleComposition { Road = "כביש 4", Direction = "צפון", Type = "אוטובוס" },
                new VehicleComposition { Road = "כביש 4", Direction = "דרום", Type = "אוטובוס" }
            };

            VehicleCompositionDataGrid.ItemsSource = VehicleCompositions;
            this.DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a real application, you would save the data to your database here

                // Simulate saving
                System.Threading.Thread.Sleep(500);

                DataSaved = true;
                MessageBox.Show("הנתונים נשמרו בהצלחה!", "שמירת נתונים", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשמירת הנתונים: {ex.Message}", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if data has been modified but not saved
            if (!DataSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "האם ברצונך לסגור ללא שמירת השינויים?",
                    "אישור סגירה",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            Close();
        }
    }

    // Model class for vehicle composition
    public class VehicleComposition
    {
        public string Road { get; set; }
        public string Direction { get; set; }
        public string Type { get; set; }
    }
}
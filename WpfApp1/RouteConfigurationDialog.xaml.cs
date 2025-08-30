using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class RouteConfigurationDialog : Window
    {
        public ObservableCollection<RouteConfiguration> RouteConfigurations { get; set; }
        public List<string> DirectionOptions { get; set; }
        public List<int> LanesCountOptions { get; set; }

        public RouteConfigurationDialog()
        {
            InitializeComponent();

            // Initialize direction options
            DirectionOptions = new List<string>
            {
                "צפון",
                "דרום",
                "מזרח",
                "מערב",
                "צפון-מזרח",
                "צפון-מערב",
                "דרום-מזרח",
                "דרום-מערב"
            };

            // Initialize lanes count options (1-7 as per the warning message)
            LanesCountOptions = Enumerable.Range(1, 7).ToList();

            // Initialize with sample data - in a real application, this would come from your data source
            RouteConfigurations = new ObservableCollection<RouteConfiguration>
            {
                new RouteConfiguration { Road = "כביש 1", Direction = "מזרח", LanesCount = 3 },
                new RouteConfiguration { Road = "כביש 1", Direction = "מערב", LanesCount = 3 },
                new RouteConfiguration { Road = "כביש 4", Direction = "צפון", LanesCount = 2 },
                new RouteConfiguration { Road = "כביש 4", Direction = "דרום", LanesCount = 2 },
                new RouteConfiguration { Road = "כביש 6", Direction = "צפון", LanesCount = 3 },
                new RouteConfiguration { Road = "כביש 6", Direction = "דרום", LanesCount = 3 }
            };

            RoutesDataGrid.ItemsSource = RouteConfigurations;
            this.DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate data
            if (!ValidateData())
            {
                return;
            }

            // In a real application, you would save the data to your data source here
            MessageBox.Show("הנתונים נשמרו בהצלחה!", "שמירת נתונים", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private bool ValidateData()
        {
            // Check for empty or invalid entries
            foreach (var config in RouteConfigurations)
            {
                if (string.IsNullOrWhiteSpace(config.Road))
                {
                    MessageBox.Show("יש להזין שם דרך לכל השורות.", "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(config.Direction))
                {
                    MessageBox.Show("יש לבחור כיוון לכל השורות.", "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (config.LanesCount < 1 || config.LanesCount > 7)
                {
                    MessageBox.Show("מספר הנתיבים חייב להיות בין 1 ל-10", "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Check for duplicate road/direction combinations
            var duplicates = RouteConfigurations
                .GroupBy(c => new { c.Road, c.Direction })
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .FirstOrDefault();

            if (duplicates != null)
            {
                MessageBox.Show($"קיימת כפילות עבור הדרך '{duplicates.Road}' בכיוון '{duplicates.Direction}'.",
                                "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation if there are unsaved changes
            MessageBoxResult result = MessageBox.Show("האם לסגור את החלון ללא שמירת השינויים?",
                                                     "אישור סגירה",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
            }
        }
    }

    // Model class for route configuration
    public class RouteConfiguration : INotifyPropertyChanged
    {
        private string _road;
        private string _direction;
        private int _lanesCount;

        public string Road
        {
            get { return _road; }
            set
            {
                if (_road != value)
                {
                    _road = value;
                    OnPropertyChanged(nameof(Road));
                }
            }
        }

        public string Direction
        {
            get { return _direction; }
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged(nameof(Direction));
                }
            }
        }

        public int LanesCount
        {
            get { return _lanesCount; }
            set
            {
                if (_lanesCount != value)
                {
                    _lanesCount = value;
                    OnPropertyChanged(nameof(LanesCount));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class ReplaceJunctionStripsDialog : Window
    {
        public string JunctionName { get; set; }
        public bool ReplacementCompleted { get; private set; }

        private List<string> roads;
        private Dictionary<string, List<string>> roadDirections;

        public ReplaceJunctionStripsDialog(string junctionName)
        {
            InitializeComponent();

            JunctionName = junctionName;
            ReplacementCompleted = false;

            // Initialize with sample data - in a real application, this would come from your database
            roads = new List<string>
            {
                "כביש 1",
                "כביש 4",
                "כביש 6",
                "כביש 20",
                "דרך בגין",
                "דרך נמיר"
            };

            roadDirections = new Dictionary<string, List<string>>
            {
                { "כביש 1", new List<string> { "מזרח", "מערב" } },
                { "כביש 4", new List<string> { "צפון", "דרום" } },
                { "כביש 6", new List<string> { "צפון", "דרום" } },
                { "כביש 20", new List<string> { "צפון", "דרום" } },
                { "דרך בגין", new List<string> { "צפון", "דרום" } },
                { "דרך נמיר", new List<string> { "צפון", "דרום" } }
            };

            // Populate the road combo boxes
            ReplaceRoadComboBox.ItemsSource = roads;
            WithRoadComboBox.ItemsSource = roads;

            // Select the first road by default
            if (roads.Count > 0)
            {
                ReplaceRoadComboBox.SelectedIndex = 0;
                WithRoadComboBox.SelectedIndex = roads.Count > 1 ? 1 : 0;
            }

            this.Title = $"החלפת רצועות לצומת - {JunctionName}";
        }

        private void RoadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null || comboBox.SelectedItem == null) return;

            string selectedRoad = comboBox.SelectedItem.ToString();

            if (comboBox == ReplaceRoadComboBox)
            {
                // Update the direction combo box for the selected road
                ReplaceDirectionComboBox.ItemsSource = roadDirections[selectedRoad];
                ReplaceDirectionComboBox.SelectedIndex = 0;
            }
            else if (comboBox == WithRoadComboBox)
            {
                // Update the direction combo box for the selected road
                WithDirectionComboBox.ItemsSource = roadDirections[selectedRoad];
                WithDirectionComboBox.SelectedIndex = 0;
            }
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate selections
            if (ReplaceRoadComboBox.SelectedItem == null ||
                ReplaceDirectionComboBox.SelectedItem == null ||
                WithRoadComboBox.SelectedItem == null ||
                WithDirectionComboBox.SelectedItem == null)
            {
                MessageBox.Show("אנא בחר דרך וכיוון בשני השדות.", "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if the same road and direction are selected in both combo boxes
            if (ReplaceRoadComboBox.SelectedItem.ToString() == WithRoadComboBox.SelectedItem.ToString() &&
                ReplaceDirectionComboBox.SelectedItem.ToString() == WithDirectionComboBox.SelectedItem.ToString())
            {
                MessageBox.Show("לא ניתן להחליף דרך וכיוון בעצמם.", "שגיאת אימות", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected values
            string replaceRoad = ReplaceRoadComboBox.SelectedItem.ToString();
            string replaceDirection = ReplaceDirectionComboBox.SelectedItem.ToString();
            string withRoad = WithRoadComboBox.SelectedItem.ToString();
            string withDirection = WithDirectionComboBox.SelectedItem.ToString();

            // Confirm the replacement
            MessageBoxResult result = MessageBox.Show(
                $"האם אתה בטוח שברצונך להחליף את הרצועות של {replaceRoad} ({replaceDirection}) עם {withRoad} ({withDirection})?",
                "אישור החלפה",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // In a real application, you would perform the replacement in your database here

                    // Simulate the replacement
                    System.Threading.Thread.Sleep(500);

                    ReplacementCompleted = true;
                    MessageBox.Show("החלפת הרצועות בוצעה בהצלחה!", "החלפת רצועות", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Close the dialog
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"שגיאה בביצוע החלפת הרצועות: {ex.Message}", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
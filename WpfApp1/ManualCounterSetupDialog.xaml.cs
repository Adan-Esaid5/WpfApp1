using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class ManualCounterSetupDialog : Window
    {
        // Dictionary to store file paths for each direction
        private Dictionary<string, string> directionFilePaths;

        public ManualCounterSetupDialog()
        {
            InitializeComponent();

            // Initialize the dictionary with all 8 directions
            directionFilePaths = new Dictionary<string, string>
            {
                { "זרוע צפון", string.Empty },
                { "זרוע דרום", string.Empty },
                { "זרוע מזרח", string.Empty },
                { "זרוע מערב", string.Empty },
                { "זרוע צפון-מערב", string.Empty },
                { "זרוע צפון-מזרח", string.Empty },
                { "זרוע דרום-מערב", string.Empty },
                { "זרוע דרום-מזרח", string.Empty }
            };
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button button = sender as Button;
            if (button == null) return;

            // Get the parent grid
            Grid parentGrid = button.Parent as Grid;
            if (parentGrid == null) return;

            // Get the direction text block and text box
            TextBlock directionTextBlock = parentGrid.Children[0] as TextBlock;
            TextBox filePathTextBox = parentGrid.Children[2] as TextBox;
            CheckBox dataExistsCheckBox = parentGrid.Children[1] as CheckBox;

            if (directionTextBlock == null || filePathTextBox == null || dataExistsCheckBox == null) return;

            string direction = directionTextBlock.Text;

            // Show file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|CSV Files|*.csv|All Files|*.*",
                Title = $"בחר קובץ נתונים עבור {direction}"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePathTextBox.Text = openFileDialog.FileName;
                directionFilePaths[direction] = openFileDialog.FileName;
                dataExistsCheckBox.IsChecked = true;
            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            Grid parentGrid = button.Parent as Grid;
            if (parentGrid == null) return;

            TextBlock directionTextBlock = parentGrid.Children[0] as TextBlock;
            CheckBox dataExistsCheckBox = parentGrid.Children[1] as CheckBox;

            if (directionTextBlock == null || dataExistsCheckBox == null) return;

            string direction = directionTextBlock.Text;

            if (dataExistsCheckBox.IsChecked == true)
            {
                MessageBox.Show($"מוסיף נתונים עבור {direction}", "הוספת נתונים", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("אנא סמן 'קיים מידע' או בחר קובץ תחילה.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // בדיקה פשוטה אם יש לפחות CheckBox מסומן
            bool hasData = false;
            foreach (var checkBox in FindVisualChildren<CheckBox>(this))
            {
                if (checkBox.IsChecked == true)
                {
                    hasData = true;
                    break;
                }
            }

            if (hasData)
            {
                MessageBoxResult result = MessageBox.Show(
                    "האם אתה בטוח שברצונך לסגור? כל הנתונים שלא נשמרו יאבדו.",
                    "אישור סגירה",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            this.Close();
        }

        // Helper method to find visual children of a specific type
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T t)
                    {
                        yield return t;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

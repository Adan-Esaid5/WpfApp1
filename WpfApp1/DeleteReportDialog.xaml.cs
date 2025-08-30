using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    public partial class DeleteReportDialog : Window
    {
        public ObservableCollection<Report> Reports { get; set; }

        public DeleteReportDialog()
        {
            InitializeComponent();

            // Initialize with sample data - in a real application, this would come from your data source
            Reports = new ObservableCollection<Report>
            {
                new Report { Id = 1, Name = "דוח חודשי - ינואר 2025" },
                new Report { Id = 2, Name = "דוח חודשי - פברואר 2025" },
                new Report { Id = 3, Name = "דוח רבעוני - Q1 2025" },
                new Report { Id = 4, Name = "דוח שנתי - 2024" }
            };

            ReportComboBox.ItemsSource = Reports;
            ReportComboBox.SelectedIndex = 0;

            this.DataContext = this;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Report selectedReport = ReportComboBox.SelectedItem as Report;

            if (selectedReport != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"האם אתה בטוח שברצונך למחוק את הדוח '{selectedReport.Name}'?",
                    "אישור מחיקה",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Delete the report - in a real application, this would call your data service
                    Reports.Remove(selectedReport);

                    if (Reports.Count > 0)
                    {
                        ReportComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        // No more reports to delete
                        MessageBox.Show("אין דוחות נוספים למחיקה.", "מידע", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    // Model class for reports
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
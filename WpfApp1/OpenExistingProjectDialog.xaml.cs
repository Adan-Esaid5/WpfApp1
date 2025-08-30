using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class OpenExistingProjectDialog : Window
    {
        public ObservableCollection<ProjectInfo> Projects { get; set; }
        public ProjectInfo SelectedProject { get; private set; }

        public OpenExistingProjectDialog()
        {
            InitializeComponent();

            // Initialize with sample data - in a real application, this would come from your database
            Projects = new ObservableCollection<ProjectInfo>
            {
                new ProjectInfo
                {
                    ProjectCode = "P001",
                    JunctionNumber = "J123",
                    JunctionName = "צומת אלנבי-רוטשילד",
                    Client = "עיריית תל אביב",
                    CountDate = new DateTime(2025, 5, 15)
                },
                new ProjectInfo
                {
                    ProjectCode = "P002",
                    JunctionNumber = "J456",
                    JunctionName = "צומת הרצל-יפו",
                    Client = "משרד התחבורה",
                    CountDate = new DateTime(2025, 4, 20)
                },
                new ProjectInfo
                {
                    ProjectCode = "P003",
                    JunctionNumber = "J789",
                    JunctionName = "צומת בן גוריון-ויצמן",
                    Client = "עיריית רמת גן",
                    CountDate = new DateTime(2025, 3, 10)
                },
                new ProjectInfo
                {
                    ProjectCode = "P004",
                    JunctionNumber = "J234",
                    JunctionName = "צומת אבן גבירול-שאול המלך",
                    Client = "עיריית תל אביב",
                    CountDate = new DateTime(2025, 2, 5)
                },
                new ProjectInfo
                {
                    ProjectCode = "P005",
                    JunctionNumber = "J567",
                    JunctionName = "צומת דיזנגוף-פרישמן",
                    Client = "משרד התחבורה",
                    CountDate = new DateTime(2025, 1, 25)
                }
            };

            ProjectsDataGrid.ItemsSource = Projects;
        }

        private void OpenProjectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedProject();
        }

        private void ProjectsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                OpenSelectedProject();
            }
        }

        private void OpenSelectedProject()
        {
            ProjectInfo selectedProject = ProjectsDataGrid.SelectedItem as ProjectInfo;

            if (selectedProject != null)
            {
                SelectedProject = selectedProject;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("אנא בחר פרויקט מהרשימה.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    // Model class for project information
    public class ProjectInfo
    {
        public string ProjectCode { get; set; }
        public string JunctionNumber { get; set; }
        public string JunctionName { get; set; }
        public string Client { get; set; }
        public DateTime CountDate { get; set; }
    }
}
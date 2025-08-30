using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VehicleTable : Window
    {
        public VehicleTable() 
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Initialize controls and load data
            LoadVehicleGroups();
        }

        private void LoadVehicleGroups()
        {
            // In a real application, this would load data from a database or service
            // For now, we'll add some sample data
            var comboBox = this.FindName("VehicleGroupComboBox") as ComboBox;
            if (comboBox != null)
            {
                comboBox.Items.Add("קבוצה 1");
                comboBox.Items.Add("קבוצה 2");
                comboBox.Items.Add("קבוצה 3");
            }
        }
    }
}

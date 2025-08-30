using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Page9 : Window
    {
        public Page9()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Set current date values
            var currentDate = DateTime.Now;

            // You can add more initialization code here
            // For example, populating comboboxes with data
        }
    }
}

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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MyWindow.xaml
    /// </summary>
    public partial class AboutBox : Window
    {
        Window lastWidndow;
        public AboutBox(Window window)
        {
            this.lastWidndow = window;
            //InitializeComponent();
        }
        // Event handler for the OK button click
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

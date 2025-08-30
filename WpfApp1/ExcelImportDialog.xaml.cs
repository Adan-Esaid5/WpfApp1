//using System;
//using System.ComponentModel;
//using System.IO;
//using System.Windows;
//using Microsoft.Win32;
//using System.Data;

//namespace WpfApp1
//{
//    public partial class ExcelImportDialog : Window, INotifyPropertyChanged
//    {
//        private bool _hasSelectedFile;


//        public bool HasSelectedFile
//        {
//            get { return _hasSelectedFile; }
//            set
//            {
//                if (_hasSelectedFile != value)
//                {
//                    _hasSelectedFile = value;
//                    OnPropertyChanged(nameof(HasSelectedFile));
//                }
//            }
//        }

//        public string SelectedFilePath { get; private set; }
//        public DataTable ImportedData { get; private set; }

//        public event PropertyChangedEventHandler PropertyChanged;

//        public ExcelPackage exP;

//        public ExcelImportDialog()
//        {
//            InitializeComponent();
//            HasSelectedFile = false;
//            this.DataContext = this;

//            // Set license context for EPPlus if using version 5+
//            //ExcelPackage.LicenseC = LicenseContext.NonCommercial;
//            exP = new ExcelPackage(new FileInfo(this.SelectedFilePath));
//        }

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        private void BrowseButton_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog
//            {
//                Filter = "Excel Files|*.xlsx;*.xls",
//                Title = "בחר קובץ Excel"
//            };

//            if (openFileDialog.ShowDialog() == true)
//            {
//                SelectedFilePath = openFileDialog.FileName;
//                FilePathTextBox.Text = SelectedFilePath;
//                HasSelectedFile = true;
//            }
//        }

//        private void ImportButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(SelectedFilePath) || !File.Exists(SelectedFilePath))
//            {
//                MessageBox.Show("אנא בחר קובץ Excel תקין.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
//                return;
//            }

//            try
//            {
//                ImportedData = ReadExcelFile(SelectedFilePath);

//                if (ImportedData != null && ImportedData.Rows.Count > 0)
//                {
//                    MessageBox.Show($"נקראו {ImportedData.Rows.Count} שורות מהקובץ בהצלחה.",
//                                    "קריאת נתונים", MessageBoxButton.OK, MessageBoxImage.Information);

//                    DialogResult = true;
//                    Close();
//                }
//                else
//                {
//                    MessageBox.Show("לא נמצאו נתונים בקובץ או שמבנה הקובץ אינו תקין.",
//                                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"אירעה שגיאה בקריאת הקובץ: {ex.Message}",
//                                "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void CloseButton_Click(object sender, RoutedEventArgs e)
//        {
//            DialogResult = false;
//            Close();
//        }

//        private DataTable ReadExcelFile(string filePath)
//        {
//            DataTable dataTable = new DataTable();

//            using (var package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                // Assume first worksheet contains the data
//                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

//                // Determine the dimensions of the worksheet
//                int rows = worksheet.Dimension.Rows;
//                int columns = worksheet.Dimension.Columns;

//                // Add columns to DataTable (use first row as column names)
//                for (int col = 1; col <= columns; col++)
//                {
//                    string columnName = worksheet.Cells[1, col].Text;
//                    if (string.IsNullOrEmpty(columnName))
//                        columnName = $"Column{col}";

//                    dataTable.Columns.Add(columnName);
//                }

//                // Add data rows to DataTable (skip header row)
//                for (int row = 2; row <= rows; row++)
//                {
//                    DataRow dataRow = dataTable.NewRow();

//                    for (int col = 1; col <= columns; col++)
//                    {
//                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
//                    }

//                    dataTable.Rows.Add(dataRow);
//                }
//            }

//            return dataTable;
//        }
//    }
//}



using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using OfficeOpenXml;

namespace WpfApp1
{
    public partial class ExcelImportDialog : Window, INotifyPropertyChanged
    {
        // ===================== תכונות (Properties) =====================
        private bool _hasSelectedFile;
        public bool HasSelectedFile
        {
            get => _hasSelectedFile;
            set
            {
                if (_hasSelectedFile == value) return;
                _hasSelectedFile = value;
                OnPropertyChanged(nameof(HasSelectedFile));
            }
        }

        private string _selectedFilePath;
        public string SelectedFilePath
        {
            get => _selectedFilePath;
            private set
            {
                if (_selectedFilePath == value) return;
                _selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
            }
        }

        public DataTable ImportedData { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // ===================== בנאי (Constructors) =====================
        public ExcelImportDialog()
        {
            InitializeComponent();
            DataContext = this;
            HasSelectedFile = false;

            // רישוי EPPlus v5+ (שימוש לא מסחרי)
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        }

        // ===================== פעולות (Methods) =====================
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                // EPPlus עובד עם XLSX/XLSM/XLTX/XLTM (לא עם XLS ישן)
                Filter = "Excel Files (*.xlsx;*.xlsm;*.xltx;*.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm",
                Title = "בחר קובץ Excel"
            };

            if (ofd.ShowDialog() == true)
            {
                SelectedFilePath = ofd.FileName;
                FilePathTextBox.Text = SelectedFilePath; // אם יש Binding, אפשר לוותר על השורה הזו
                HasSelectedFile = true;
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedFilePath) || !File.Exists(SelectedFilePath))
            {
                MessageBox.Show("אנא בחר קובץ Excel תקין.", "שגיאה",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                ImportedData = ReadExcelFile(SelectedFilePath);

                if (ImportedData.Rows.Count > 0)
                {
                    MessageBox.Show($"נקראו {ImportedData.Rows.Count} שורות מהקובץ בהצלחה.",
                        "קריאת נתונים", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("לא נמצאו נתונים בקובץ או שמבנה הקובץ אינו תקין.",
                        "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"אירעה שגיאה בקריאת הקובץ: {ex.Message}",
                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private static DataTable ReadExcelFile(string filePath)
        {
            var table = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var wb = package.Workbook;
                if (wb == null || wb.Worksheets.Count == 0)
                    return table;

                // ב-EPPlus האינדקס של גיליונות הוא 1-based
                var worksheet = wb.Worksheets[1];
                if (worksheet.Dimension == null)
                    return table;

                int rows = worksheet.Dimension.End.Row;
                int cols = worksheet.Dimension.End.Column;

                // כותרות מהשורה הראשונה
                for (int col = 1; col <= cols; col++)
                {
                    var header = worksheet.Cells[1, col].Text?.Trim();
                    if (string.IsNullOrEmpty(header)) header = $"Column{col}";
                    table.Columns.Add(header);
                }

                // נתונים מהשורה השנייה והלאה
                for (int row = 2; row <= rows; row++)
                {
                    var dr = table.NewRow();
                    for (int col = 1; col <= cols; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    table.Rows.Add(dr);
                }
            }

            return table;
        }
    }
}

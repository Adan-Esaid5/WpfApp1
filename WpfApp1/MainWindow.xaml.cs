using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddShortcuts();
        }

        // הוספת קיצורי מקלדת לפעולות
        private void AddShortcuts()
        {
            // Ctrl+N - חדש
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, New_Click));
            InputBindings.Add(new KeyBinding(ApplicationCommands.New, Key.N, ModifierKeys.Control));

            // Ctrl+O - פתח
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Click));
            InputBindings.Add(new KeyBinding(ApplicationCommands.Open, Key.O, ModifierKeys.Control));

            // Ctrl+S - שמור
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Click));
            InputBindings.Add(new KeyBinding(ApplicationCommands.Save, Key.S, ModifierKeys.Control));

            // Alt+F4 - סגירה (ברירת מחדל, אין צורך בקוד)
        }

        // פעולה: חדש
        private void New_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("נבחר 'חדש' - ניתן לאפס את כל השדות כאן.");
        }

        // פעולה: פתח
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("נבחר 'פתח' - כאן תוכל לפתוח קובץ נתונים.");
        }

        // פעולה: שמור
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("נבחר 'שמור' - כאן תוכל לשמור את המידע.");
        }

        // פעולה: יציאה
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

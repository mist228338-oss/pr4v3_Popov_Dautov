using System.ComponentModel;
using System.Windows;
using PracticalWork4_Variant3.Pages;

namespace PracticalWork4_Variant3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1());
        }

        private void BtnPage1_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page1());
        private void BtnPage2_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page2());
        private void BtnPage3_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page3());

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var res = MessageBox.Show(
                "Вы действительно хотите выйти из приложения?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (res != MessageBoxResult.Yes)
                e.Cancel = true;
        }
    }
}

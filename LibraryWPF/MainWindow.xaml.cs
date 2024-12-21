using LibraryWPF.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigated += MainFrame_Navigated;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Проверяем, является ли текущая страница главной
            if (e.Content is MainWindow)
            {
                // Показываем заголовок "Добро пожаловать в главное меню!"
                welcomeTextBlock.Visibility = Visibility.Visible;

                // Показываем кнопки
                btnAuthors.Visibility = Visibility.Visible;
                btnBooks.Visibility = Visibility.Visible;
                btnReaders.Visibility = Visibility.Visible;
                btnLoans.Visibility = Visibility.Visible;
            }
            else
            {
                // Скрываем заголовок на других страницах
                welcomeTextBlock.Visibility = Visibility.Collapsed;

                // Скрываем кнопки на других страницах
                btnAuthors.Visibility = Visibility.Collapsed;
                btnBooks.Visibility = Visibility.Collapsed;
                btnReaders.Visibility = Visibility.Collapsed;
                btnLoans.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAuthors_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
            MainFrame.Navigate(new AuthorsPage());
        }

        // Обработчик кнопки "Книги"
        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
            MainFrame.Navigate(new BooksPage());
        }

        // Обработчик кнопки "Читатели"
        private void btnReaders_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
            MainFrame.Navigate(new ReadersPage());
        }

        // Обработчик кнопки "Выдачи"
        private void btnLoans_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
            MainFrame.Navigate(new LoansPage());
        }

    }
}

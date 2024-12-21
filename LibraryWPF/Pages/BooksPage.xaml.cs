using LibraryCore;
using LibraryCore.DataBase;
using LibraryWPF.EditAndAdd;
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

namespace LibraryWPF.Pages
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        private Upravlenie_bibliotekoyEntities _context;
        public BooksPage()
        {
            InitializeComponent();
            _context = data.GetContext(); // Инициализация контекста базы данных
            LoadData();

        }

        private void LoadData()
        {
            var books = _context.Books.AsQueryable();
            dataGrid.ItemsSource = books.ToList();
        }

        // Обработчик для кнопки "Добавить"
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления
            var addWindow = new AddBookWindow(_context);

            // Если окно закрылось с результатом "true", обновляем DataGrid
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        // Обработчик для кнопки "Редактировать"
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный объект из DataGrid
            var selectedBook = dataGrid.SelectedItem as Books;
            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу для редактирования.");
                return;
            }

            // Открываем окно для редактирования выбранного объекта
            OpenEditWindow(selectedBook);
        }

        // Обработчик для кнопки "Удалить"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный объект из DataGrid
            var selectedBook = dataGrid.SelectedItem as Books;
            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show("Вы действительно хотите удалить выбранную книгу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Удаляем объект из базы данных
                _context.Books.Remove(selectedBook);
                _context.SaveChanges();

                // Обновляем DataGrid
                LoadData();
            }
        }

        // Метод для открытия окна редактирования
        private void OpenEditWindow(Books book)
        {
            // Создаем окно редактирования (например, EditBookWindow)
            var editWindow = new EditBookWindow(book);

            // Показываем окно и обновляем DataGrid после закрытия
            if (editWindow.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadData();
            }
        }

        // Обработчик для выбора сортировки в ComboBox
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cbSort.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;

            var sortType = selectedItem.Tag.ToString();

            switch (sortType)
            {
                case "None":
                    LoadData(); // Без сортировки
                    break;
                case "Title":
                    dataGrid.ItemsSource = _context.Books.OrderBy(b => b.Title).ToList(); // По названию
                    break;
                case "Genre":
                    dataGrid.ItemsSource = _context.Books.OrderBy(b => b.Genre).ToList(); // По жанру
                    break;
            }
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр главного окна
            MainWindow mainWindow = new MainWindow();

            // Закрываем текущую страницу (AuthorsPage)
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();

            // Открываем главное окно
            mainWindow.Show();
        }

        // Обработчик для кнопки "Статистика"
        private void btnShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        // Метод для отображения статистики
        private void ShowStatistics()
        {
            int totalBooks = _context.Books.Count();

            MessageBox.Show($"Количество книг в библиотеке: {totalBooks}");
        }



    }
}

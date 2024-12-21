using LibraryCore;
using LibraryCore.DataBase;
using LibraryWPF.EditAndAdd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for AuthorsPage.xaml
    /// </summary>
    public partial class AuthorsPage : Page
    {
        private Upravlenie_bibliotekoyEntities _context;

        public AuthorsPage()
        {
            InitializeComponent();
            _context = data.GetContext(); // Инициализация контекста базы данных
            LoadData();
        }

        // Метод для загрузки данных в DataGrid
        private void LoadData()
        {
            var authors = _context.Authors.AsQueryable();
            dataGrid.ItemsSource = authors.ToList();
        }

        // Обработчик для кнопки "Добавить"
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления
            var addWindow = new AddAuthorsWindow(_context);

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
            var selectedAuthor = dataGrid.SelectedItem as Authors;
            if (selectedAuthor == null)
            {
                MessageBox.Show("Выберите автора для редактирования.");
                return;
            }

            // Открываем окно для редактирования выбранного объекта
            OpenEditWindow(selectedAuthor);
        }

        // Обработчик для кнопки "Удалить"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный объект из DataGrid
            var selectedAuthor = dataGrid.SelectedItem as Authors;
            if (selectedAuthor == null)
            {
                MessageBox.Show("Выберите автора для удаления.");
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show("Вы действительно хотите удалить выбранного автора?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Удаляем объект из базы данных
                _context.Authors.Remove(selectedAuthor);
                _context.SaveChanges();

                // Обновляем DataGrid
                LoadData();
            }
        }

        // Метод для открытия окна редактирования
        private void OpenEditWindow(Authors author)
        {
            // Создаем окно редактирования (например, EditAuthorWindow)
            var editWindow = new EditAuthorWindow(author);

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
                case "ZA":
                    dataGrid.ItemsSource = _context.Authors.OrderBy(a => a.LastName).ToList(); // От А до Я
                    break;
                case "AZ":
                    dataGrid.ItemsSource = _context.Authors.OrderByDescending(a => a.LastName).ToList(); // От Я до А
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
            int totalAuthors = _context.Authors.Count();

            MessageBox.Show($"Количество авторов в библиотеке: {totalAuthors}");
        }
    }
}

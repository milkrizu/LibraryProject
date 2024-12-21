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
    /// Interaction logic for ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        private Upravlenie_bibliotekoyEntities _context;
        public ReadersPage()
        {
            InitializeComponent();
            _context = data.GetContext(); // Инициализация контекста базы данных
            LoadData();
        }

        private void LoadData()
        {
            var readers = _context.Readers.AsQueryable();
            dataGrid.ItemsSource = readers.ToList();
        }

        // Обработчик для кнопки "Добавить"
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления
            var addWindow = new AddReaderWindow(_context);

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
            var selectedReader = dataGrid.SelectedItem as Readers;
            if (selectedReader == null)
            {
                MessageBox.Show("Выберите читателя для редактирования.");
                return;
            }

            // Открываем окно для редактирования выбранного объекта
            OpenEditWindow(selectedReader);
        }

        // Обработчик для кнопки "Удалить"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный объект из DataGrid
            var selectedReader = dataGrid.SelectedItem as Readers;
            if (selectedReader == null)
            {
                MessageBox.Show("Выберите читателя для удаления.");
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show("Вы действительно хотите удалить выбранного читателя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Удаляем объект из базы данных
                _context.Readers.Remove(selectedReader);
                _context.SaveChanges();

                // Обновляем DataGrid
                LoadData();
            }
        }

        // Метод для открытия окна редактирования
        private void OpenEditWindow(Readers reader)
        {
            // Создаем окно редактирования (например, EditReaderWindow)
            var editWindow = new EditReaderWindow(reader);

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
                case "FirstName":
                    dataGrid.ItemsSource = _context.Readers.OrderBy(r => r.FirstName).ToList(); // По имени
                    break;
                case "LastName":
                    dataGrid.ItemsSource = _context.Readers.OrderBy(r => r.LastName).ToList(); // По фамилии
                    break;
            }
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр главного окна
            MainWindow mainWindow = new MainWindow();

            // Закрываем текущую страницу (ReadersPage)
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
            int totalReaders = _context.Readers.Count();

            MessageBox.Show($"Количество читателей в библиотеке: {totalReaders}");
        }
    }
}


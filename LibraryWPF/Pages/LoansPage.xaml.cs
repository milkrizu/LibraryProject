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
    /// Interaction logic for LoansPage.xaml
    /// </summary>
    public partial class LoansPage : Page
    {
        private Upravlenie_bibliotekoyEntities _context;
        public LoansPage()
        {
            InitializeComponent();
            _context = data.GetContext(); // Инициализация контекста базы данных
            LoadData();
        }

        private void LoadData()
        {
            var loans = _context.Loans.AsQueryable();
            dataGrid.ItemsSource = loans.ToList();
        }

        // Обработчик для кнопки "Добавить"
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления
            var addWindow = new AddLoanWindow(_context);

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
            var selectedLoan = dataGrid.SelectedItem as Loans;
            if (selectedLoan == null)
            {
                MessageBox.Show("Выберите запись о выдаче для редактирования.");
                return;
            }

            // Открываем окно для редактирования выбранного объекта
            OpenEditWindow(selectedLoan);
        }

        // Обработчик для кнопки "Удалить"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный объект из DataGrid
            var selectedLoan = dataGrid.SelectedItem as Loans;
            if (selectedLoan == null)
            {
                MessageBox.Show("Выберите запись о выдаче для удаления.");
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show("Вы действительно хотите удалить выбранную запись о выдаче?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Удаляем объект из базы данных
                _context.Loans.Remove(selectedLoan);
                _context.SaveChanges();

                // Обновляем DataGrid
                LoadData();
            }
        }

        // Метод для открытия окна редактирования
        private void OpenEditWindow(Loans loan)
        {
            // Создаем окно редактирования (например, EditAuthorWindow)
            var editWindow = new EditLoanWindow(loan);

            // Показываем окно и обновляем DataGrid после закрытия
            if (editWindow.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadData();
            }
        }



        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр главного окна
            MainWindow mainWindow = new MainWindow();

            // Закрываем текущую страницу (LoansPage)
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();

            // Открываем главное окно
            mainWindow.Show();
        }


    }
}

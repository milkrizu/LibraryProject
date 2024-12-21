using LibraryCore;
using LibraryCore.DataBase;
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

namespace LibraryWPF.EditAndAdd
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private Books _book;
        private Upravlenie_bibliotekoyEntities _context;
        public EditBookWindow(Books book)
        {
            InitializeComponent();
            _book = book;
            _context = data.GetContext(); // Инициализация контекста базы данных

            // Заполняем поля данными из выбранной книги
            txtTitle.Text = _book.Title;
            txtYear.Text = _book.Year.ToString();
            txtGenre.Text = _book.Genre;
            txtQuantity.Text = _book.Quantity.ToString();

            //// Заполняем ComboBox с авторами
            //cbAuthors.ItemsSource = _context.Authors.ToList();
            //cbAuthors.SelectedItem = _book.Authors;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения данных
            var newBook = new Books
            {
                Title = txtTitle.Text,
                AuthorID = int.Parse(txtAuthor.Text), // Предполагаем, что вводится ID автора
                Year = int.Parse(txtYear.Text),
                Genre = txtGenre.Text,
                Quantity = int.Parse(txtQuantity.Text)
            };

            // Сохранение данных
            _context.Books.Add(newBook);
            _context.SaveChanges();

            // Закрываем окно с результатом "true"
            this.DialogResult = true;
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Просто закрываем текущее окно
            this.Close();
        }
    }
}

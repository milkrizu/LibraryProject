using LibraryCore;
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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private Upravlenie_bibliotekoyEntities _context;
        public AddBookWindow(Upravlenie_bibliotekoyEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения новой книги
            var newBook = new Books
            {
                Title = txtTitle.Text,
                AuthorID = int.Parse(txtAuthorID.Text),
                Year = int.Parse(txtYear.Text),
                Genre = txtGenre.Text,
                Quantity = int.Parse(txtQuantity.Text)
            };

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

using LibraryCore;
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
using System.Windows.Shapes;

namespace LibraryWPF.EditAndAdd
{
    /// <summary>
    /// Interaction logic for AddAuthorsWindow.xaml
    /// </summary>
    public partial class AddAuthorsWindow : Window
    {
        private Upravlenie_bibliotekoyEntities _context;
        public AddAuthorsWindow(Upravlenie_bibliotekoyEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения нового автора
            var newAuthor = new Authors
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                BirthDate = dpBirthDate.SelectedDate,
                Country = txtCountry.Text
            };

            _context.Authors.Add(newAuthor);
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

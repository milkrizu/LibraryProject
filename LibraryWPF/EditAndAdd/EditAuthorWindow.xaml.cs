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
    /// Interaction logic for EditAuthorWindow.xaml
    /// </summary>
    public partial class EditAuthorWindow : Window
    {
        private Authors _author;
        public EditAuthorWindow(Authors author)
        {
            InitializeComponent();
            _author = author;

            // Заполняем поля данными из выбранного автора
            txtFirstName.Text = author.FirstName;
            txtLastName.Text = author.LastName;
            dpBirthDate.SelectedDate = author.BirthDate;
            txtCountry.Text = author.Country;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения изменений
            _author.FirstName = txtFirstName.Text;
            _author.LastName = txtLastName.Text;
            _author.BirthDate = dpBirthDate.SelectedDate;
            _author.Country = txtCountry.Text;

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

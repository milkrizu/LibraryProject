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
    /// Interaction logic for EditReaderWindow.xaml
    /// </summary>
    public partial class EditReaderWindow : Window
    {
        private Readers _reader;
        public EditReaderWindow(Readers reader)
        {
            InitializeComponent();
            _reader = reader;

            txtFirstName.Text = reader.FirstName;
            txtLastName.Text = reader.LastName;
            txtLibraryCardNumber.Text = reader.LibraryCardNumber;
            txtPhone.Text = reader.Phone;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения изменений
            _reader.FirstName = txtFirstName.Text;
            _reader.LastName = txtLastName.Text;
            _reader.LibraryCardNumber = txtLibraryCardNumber.Text;
            _reader.Phone = txtPhone.Text;

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

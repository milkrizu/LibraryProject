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
    /// Interaction logic for EditLoanWindow.xaml
    /// </summary>
    public partial class EditLoanWindow : Window
    {
        private Loans _loan;
        
        public EditLoanWindow(Loans loan)
        {
            InitializeComponent();
            _loan = loan;
           

            // Заполняем поля данными из переданного объекта выдачи
            txtBookID.Text = _loan.BookID.ToString();
            txtReaderID.Text = _loan.ReaderID.ToString();
            dpLoanDate.SelectedDate = _loan.LoanDate;
            dpReturnDate.SelectedDate = _loan.ReturnDate;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения изменений
            _loan.BookID = int.Parse(txtBookID.Text);
            _loan.ReaderID = int.Parse(txtReaderID.Text);
            _loan.LoanDate = dpLoanDate.SelectedDate.Value;
            _loan.ReturnDate = dpReturnDate.SelectedDate.Value;

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

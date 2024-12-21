﻿using LibraryCore;
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
    /// Interaction logic for AddLoanWindow.xaml
    /// </summary>
    public partial class AddLoanWindow : Window
    {
        private Upravlenie_bibliotekoyEntities _context;
        public AddLoanWindow(Upravlenie_bibliotekoyEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения новой записи о выдаче
            var newLoan = new Loans
            {
                BookID = int.Parse(txtBookID.Text),
                ReaderID = int.Parse(txtReaderID.Text),
                LoanDate = dpLoanDate.SelectedDate.Value,
                ReturnDate = dpReturnDate.SelectedDate.Value
            };

            _context.Loans.Add(newLoan);
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

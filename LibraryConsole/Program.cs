using LibraryCore.DataBase;
using LibraryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            var dbContext = new Upravlenie_bibliotekoyEntities();

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотреть список авторов");
                Console.WriteLine("2. Просмотреть список книг");
                Console.WriteLine("3. Просмотреть список читателей");
                Console.WriteLine("4. Просмотреть список выдачи книг");
                Console.WriteLine("5. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageAuthors(dbContext);
                        break;
                    case "2":
                        ManageBooks(dbContext);
                        break;
                    case "3":
                        ManageReaders(dbContext);
                        break;
                    case "4":
                        ManageLoans(dbContext);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ManageAuthors(Upravlenie_bibliotekoyEntities dbContext)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие для авторов:");
                Console.WriteLine("1. Просмотреть список авторов");
                Console.WriteLine("2. Добавить автора");
                Console.WriteLine("3. Удалить автора");
                Console.WriteLine("4. Редактировать автора");
                Console.WriteLine("5. Вернуться в главное меню");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAuthors(dbContext);
                        break;
                    case "2":
                        AddAuthor(dbContext);
                        break;
                    case "3":
                        DeleteAuthor(dbContext);
                        break;
                    case "4":
                        UpdateAuthor(dbContext);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowAuthors(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nСписок авторов:");
            var authors = dbContext.Authors.ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"ID: {author.AuthorID}, Фамилия: {author.LastName}, Имя: {author.FirstName}");
            }
        }

        static void AddAuthor(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nДобавление нового автора:");
            Console.Write("Введите фамилию автора: ");
            string lastName = Console.ReadLine();
            Console.Write("Введите имя автора: ");
            string firstName = Console.ReadLine();

            var newAuthor = new Authors
            {
                LastName = lastName,
                FirstName = firstName
            };

            dbContext.Authors.Add(newAuthor);
            dbContext.SaveChanges();

            Console.WriteLine("Автор успешно добавлен!");
        }

        static void DeleteAuthor(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nУдаление автора:");
            Console.Write("Введите ID автора для удаления: ");
            int authorIdToDelete = int.Parse(Console.ReadLine());

            var authorToDelete = dbContext.Authors.FirstOrDefault(a => a.AuthorID == authorIdToDelete);
            if (authorToDelete != null)
            {
                dbContext.Authors.Remove(authorToDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Автор успешно удален!");
            }
            else
            {
                Console.WriteLine("Автор не найден.");
            }
        }

        static void UpdateAuthor(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nОбновление данных автора:");
            Console.Write("Введите ID автора для обновления: ");
            int authorIdToUpdate = int.Parse(Console.ReadLine());

            var authorToUpdate = dbContext.Authors.FirstOrDefault(a => a.AuthorID == authorIdToUpdate);
            if (authorToUpdate != null)
            {
                Console.Write("Введите новую фамилию автора: ");
                string newLastName = Console.ReadLine();
                Console.Write("Введите новое имя автора: ");
                string newFirstName = Console.ReadLine();

                authorToUpdate.LastName = newLastName;
                authorToUpdate.FirstName = newFirstName;

                dbContext.SaveChanges();
                Console.WriteLine("Данные автора успешно обновлены!");
            }
            else
            {
                Console.WriteLine("Автор не найден.");
            }
        }

        static void ManageBooks(Upravlenie_bibliotekoyEntities dbContext)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие для книг:");
                Console.WriteLine("1. Просмотреть список книг");
                Console.WriteLine("2. Добавить книгу");
                Console.WriteLine("3. Удалить книгу");
                Console.WriteLine("4. Редактировать книгу");
                Console.WriteLine("5. Вернуться в главное меню");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowBooks(dbContext);
                        break;
                    case "2":
                        AddBook(dbContext);
                        break;
                    case "3":
                        DeleteBook(dbContext);
                        break;
                    case "4":
                        UpdateBook(dbContext);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowBooks(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nСписок книг:");
            var books = dbContext.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BookID}, Название: {book.Title}, Автор: {book.Authors.LastName}, Жанр: {book.Genre}");
            }
        }

        static void AddBook(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nДобавление новой книги:");
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            Console.Write("Введите ID автора: ");
            int authorId = int.Parse(Console.ReadLine());
            Console.Write("Введите жанр книги: ");
            string genre = Console.ReadLine();

            var newBook = new Books
            {
                Title = title,
                AuthorID = authorId,
                Genre = genre
            };

            dbContext.Books.Add(newBook);
            dbContext.SaveChanges();

            Console.WriteLine("Книга успешно добавлена!");
        }

        static void DeleteBook(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nУдаление книги:");
            Console.Write("Введите ID книги для удаления: ");
            int bookIdToDelete = int.Parse(Console.ReadLine());

            var bookToDelete = dbContext.Books.FirstOrDefault(b => b.BookID == bookIdToDelete);
            if (bookToDelete != null)
            {
                dbContext.Books.Remove(bookToDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Книга успешно удалена!");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        static void UpdateBook(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nОбновление данных книги:");
            Console.Write("Введите ID книги для обновления: ");
            int bookIdToUpdate = int.Parse(Console.ReadLine());

            var bookToUpdate = dbContext.Books.FirstOrDefault(b => b.BookID == bookIdToUpdate);
            if (bookToUpdate != null)
            {
                Console.Write("Введите новое название книги: ");
                string newTitle = Console.ReadLine();
                Console.Write("Введите новый ID автора: ");
                int newAuthorId = int.Parse(Console.ReadLine());
                Console.Write("Введите новый жанр книги: ");
                string newGenre = Console.ReadLine();

                bookToUpdate.Title = newTitle;
                bookToUpdate.AuthorID = newAuthorId;
                bookToUpdate.Genre = newGenre;

                dbContext.SaveChanges();
                Console.WriteLine("Данные книги успешно обновлены!");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        static void ManageReaders(Upravlenie_bibliotekoyEntities dbContext)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие для читателей:");
                Console.WriteLine("1. Просмотреть список читателей");
                Console.WriteLine("2. Добавить читателя");
                Console.WriteLine("3. Удалить читателя");
                Console.WriteLine("4. Редактировать читателя");
                Console.WriteLine("5. Вернуться в главное меню");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowReaders(dbContext);
                        break;
                    case "2":
                        AddReader(dbContext);
                        break;
                    case "3":
                        DeleteReader(dbContext);
                        break;
                    case "4":
                        UpdateReader(dbContext);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowReaders(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nСписок читателей:");
            var readers = dbContext.Readers.ToList();
            foreach (var reader in readers)
            {
                Console.WriteLine($"ID: {reader.ReaderID}, Фамилия: {reader.LastName}, Имя: {reader.FirstName}");
            }
        }

        static void AddReader(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nДобавление нового читателя:");
            Console.Write("Введите фамилию читателя: ");
            string lastName = Console.ReadLine();
            Console.Write("Введите имя читателя: ");
            string firstName = Console.ReadLine();

            var newReader = new Readers
            {
                LastName = lastName,
                FirstName = firstName
            };

            dbContext.Readers.Add(newReader);
            dbContext.SaveChanges();

            Console.WriteLine("Читатель успешно добавлен!");
        }

        static void DeleteReader(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nУдаление читателя:");
            Console.Write("Введите ID читателя для удаления: ");
            int readerIdToDelete = int.Parse(Console.ReadLine());

            var readerToDelete = dbContext.Readers.FirstOrDefault(r => r.ReaderID == readerIdToDelete);
            if (readerToDelete != null)
            {
                dbContext.Readers.Remove(readerToDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Читатель успешно удален!");
            }
            else
            {
                Console.WriteLine("Читатель не найден.");
            }
        }

        static void UpdateReader(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nОбновление данных читателя:");
            Console.Write("Введите ID читателя для обновления: ");
            int readerIdToUpdate = int.Parse(Console.ReadLine());

            var readerToUpdate = dbContext.Readers.FirstOrDefault(r => r.ReaderID == readerIdToUpdate);
            if (readerToUpdate != null)
            {
                Console.Write("Введите новую фамилию читателя: ");
                string newLastName = Console.ReadLine();
                Console.Write("Введите новое имя читателя: ");
                string newFirstName = Console.ReadLine();

                readerToUpdate.LastName = newLastName;
                readerToUpdate.FirstName = newFirstName;

                dbContext.SaveChanges();
                Console.WriteLine("Данные читателя успешно обновлены!");
            }
            else
            {
                Console.WriteLine("Читатель не найден.");
            }
        }

        static void ManageLoans(Upravlenie_bibliotekoyEntities dbContext)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие для выдачи книг:");
                Console.WriteLine("1. Просмотреть список выдачи книг");
                Console.WriteLine("2. Добавить выдачу книги");
                Console.WriteLine("3. Удалить выдачу книги");
                Console.WriteLine("4. Редактировать выдачу книги");
                Console.WriteLine("5. Вернуться в главное меню");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowLoans(dbContext);
                        break;
                    case "2":
                        AddLoan(dbContext);
                        break;
                    case "3":
                        DeleteLoan(dbContext);
                        break;
                    case "4":
                        UpdateLoan(dbContext);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowLoans(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nСписок выдачи книг:");
            var loans = dbContext.Loans.ToList();
            foreach (var loan in loans)
            {
                Console.WriteLine($"ID: {loan.LoanID}, Книга: {loan.Books.Title}, Читатель: {loan.Readers.LastName}, Дата выдачи: {loan.LoanDate}, Дата возврата: {loan.ReturnDate}");
            }
        }

        static void AddLoan(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nДобавление новой выдачи книги:");
            Console.Write("Введите ID книги: ");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID читателя: ");
            int readerId = int.Parse(Console.ReadLine());
            Console.Write("Введите дату выдачи (гггг-мм-дд): ");
            DateTime loanDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите дату возврата (гггг-мм-дд): ");
            DateTime returnDate = DateTime.Parse(Console.ReadLine());

            var newLoan = new Loans
            {
                BookID = bookId,
                ReaderID = readerId,
                LoanDate = loanDate,
                ReturnDate = returnDate
            };

            dbContext.Loans.Add(newLoan);
            dbContext.SaveChanges();

            Console.WriteLine("Выдача книги успешно добавлена!");
        }

        static void DeleteLoan(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nУдаление выдачи книги:");
            Console.Write("Введите ID выдачи для удаления: ");
            int loanIdToDelete = int.Parse(Console.ReadLine());

            var loanToDelete = dbContext.Loans.FirstOrDefault(l => l.LoanID == loanIdToDelete);
            if (loanToDelete != null)
            {
                dbContext.Loans.Remove(loanToDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Выдача книги успешно удалена!");
            }
            else
            {
                Console.WriteLine("Выдача книги не найдена.");
            }
        }

        static void UpdateLoan(Upravlenie_bibliotekoyEntities dbContext)
        {
            Console.WriteLine("\nОбновление данных выдачи книги:");
            Console.Write("Введите ID выдачи для обновления: ");
            int loanIdToUpdate = int.Parse(Console.ReadLine());

            var loanToUpdate = dbContext.Loans.FirstOrDefault(l => l.LoanID == loanIdToUpdate);
            if (loanToUpdate != null)
            {
                Console.Write("Введите новый ID книги: ");
                int newBookId = int.Parse(Console.ReadLine());
                Console.Write("Введите новый ID читателя: ");
                int newReaderId = int.Parse(Console.ReadLine());
                Console.Write("Введите новую дату выдачи (гггг-мм-дд): ");
                DateTime newLoanDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите новую дату возврата (гггг-мм-дд): ");
                DateTime newReturnDate = DateTime.Parse(Console.ReadLine());

                loanToUpdate.BookID = newBookId;
                loanToUpdate.ReaderID = newReaderId;
                loanToUpdate.LoanDate = newLoanDate;
                loanToUpdate.ReturnDate = newReturnDate;

                dbContext.SaveChanges();
                Console.WriteLine("Данные выдачи книги успешно обновлены!");
            }
            else
            {
                Console.WriteLine("Выдача книги не найдена.");
            }
        }
    }
}



using Book_Data1;
using Book_Domin;
using System;
using System.Text.Json;

namespace Book_mainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char contine = 'y';
            int countOfBook= 0;
            int numOfOp;
            bool login = false;
            string name;
            string pass;
            using (BookDBcontext context = new BookDBcontext())
            {

                context.Database.EnsureCreated();
            }
            jsontoDb();
            using (BookDBcontext context = new BookDBcontext())
            {
                Console.WriteLine(".....welcome to our application.....\n");
                Console.Write("pleas enter user name: ");
                name = Console.ReadLine();
                Console.Write("pleas enter password: ");
                pass = Console.ReadLine();
                foreach (var user in context.users)
                    if (name == user.userName && pass == user.password)
                    {
                        login = true;
                    }
            }
            if (login== true)
            {
                Console.WriteLine("............");
                Console.WriteLine("logged in successfully");
                Console.WriteLine("Hello! " + name);
                Console.WriteLine("............");
                using (BookDBcontext context = new BookDBcontext())
                {
                    var books = context.books.ToList();
                    countOfBook = books.Count;
                    Console.WriteLine("The number of books in repository: "+ countOfBook);
                    Console.WriteLine("............");
                }
                while (contine == 'y')
                {
                    Console.WriteLine("You can choose one of the following operation: ");
                    Console.WriteLine("To get informaitos of books enter  1 \n" +
                        "To get informaitos for book by id enter 2 \n" +
                        "To update information of book enter 3 \n" +
                        "To delet book enter 4");
                    numOfOp = Convert.ToInt32(Console.ReadLine());
                    switch (numOfOp)
                    {
                        case 1: getInfoBook(); break;
                        case 2: getInfoById(); break;
                        case 3: updateById(); break;
                        case 4: deleteById(); break;
                        default: Console.WriteLine("You entered an incorrect number"); break;
                    }
                    Console.WriteLine("............");
                    Console.Write("To continue enter 'y' otherwise enter any thing: ");
                    contine = Convert.ToChar(Console.ReadLine());

                }
            }
            else 
            {
                Console.WriteLine(".................");
                Console.WriteLine("wrong username or passwod "); 
                
            }
            
        }
        private static void getInfoBook()
        {
            using (BookDBcontext context = new BookDBcontext())
            {
                var books = context.books.ToList();
                int sum = 0;
                foreach (var book in books)
                {
                    Console.WriteLine("Id: " + book.Id + ", " + "The titel: " + book.Name + ", " + book.numberofcoby); ;
                    sum += book.numberofcoby;
                }
                Console.WriteLine("The totel number of copies repository: "+ sum);
            }
        }
        private static void getInfoById()
        {
            using (BookDBcontext context = new BookDBcontext())
            {
               int id;
               Console.WriteLine("Enter the id of book to get informations: ");
               id=Convert.ToInt32(Console.ReadLine());
                foreach (var book in context.books)
                {
                    if (id == book.Id)
                    {
                        Console.WriteLine("Book information:" +book.Name + ", " + book.price.ToString("c") + ", "+ book.numberofcoby);                   
                    }
                }


            }
        }

        private static void deleteById()
        {
            using (BookDBcontext context = new BookDBcontext())
            {
                int id;
                Console.WriteLine("Enter the id of book you went to delete it: ");
                id = Convert.ToInt32(Console.ReadLine());
                foreach (var book in context.books)
                {

                    if (id == book.Id)
                    {
                        context.Remove(book);
                        Console.WriteLine("deleted successfully");
                    }
                }
                context.SaveChanges();

            }

        }
        private static void updateById()
        {
            using (BookDBcontext context = new BookDBcontext())
            {
                int id;
                Console.WriteLine("Enter the id of book you went to update it: ");
                id = Convert.ToInt32(Console.ReadLine());
                foreach (var book in context.books)
                {

                    if (id == book.Id)
                    {
                        Console.WriteLine("Enter the new name of the book: ");
                        book.Name = Console.ReadLine();
                        Console.WriteLine("Enter the new price of the bbook");
                        book.price = Convert.ToInt32(Console.ReadLine());
                    }
                }
                context.SaveChanges();

            }
        }
        private static void  jsontoDb()
        {
            string file = File.ReadAllText("C:\\Users\\HIBA-HY\\source\\repos\\Book_Domin\\Book_mainApp\\users.json");
            var defultUsers = JsonSerializer.Deserialize<List<users>>(file);
            bool isthere = false;
            
            using (BookDBcontext context = new BookDBcontext())
            {

                foreach (var item in defultUsers)
                {
                    foreach (var user in context.users)
                        if (item.userName == user.userName)
                        {
                            isthere = true;
                        }

                }
                if (isthere == false)
                {
                    context.users.AddRange(defultUsers);
                    context.SaveChanges();
                }
                
            }
            
        }
       
    }
}
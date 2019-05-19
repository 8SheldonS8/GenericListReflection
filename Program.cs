using Common;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace GenericListReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = CreatePerson();
            person.Purchases = CreatePurchases();
            person.Reviews = CreateReviews(person.Purchases);
            List<string> requestedFields = new List<string>
            {
                "FirstName",
                "LastName",
                "Purchases",
                "Reviews"
            };
            dynamic returnObject = new ExpandoObject();
            DataMapper dataMapper = new DataMapper();

            returnObject = dataMapper.GetFieldValue(person, requestedFields);

            string returnValue = JsonConvert.SerializeObject(returnObject);
            Console.WriteLine(returnValue);

            Console.ReadKey();
        }

        private static Person CreatePerson()
        {
            Person person = new Person();
            person.Id = 1;
            person.FirstName = "Jane";
            person.LastName = "Doe";
            return person;
        }

        private static List<Purchase> CreatePurchases()
        {
            List<Purchase> purchases = new List<Purchase>();
            purchases.Add(new Purchase{
                Id = 1,
                ProductName = "Widget",
                PurchaseDate = Convert.ToDateTime("1/1/2019 09:30:00")
            });
            purchases.Add(new Purchase{
                Id = 2,
                ProductName = "Socks",
                PurchaseDate = Convert.ToDateTime("2/10/2019 13:00:00")
            });
            purchases.Add(new Purchase{
                Id = 3,
                ProductName = "Scarf",
                PurchaseDate = Convert.ToDateTime("2/11/2019 09:15:35")
            });
            purchases.Add(new Purchase{
                Id = 4,
                ProductName = "Scarf",
                PurchaseDate = Convert.ToDateTime("3/5/2019 10:25:15")
            });
            purchases.Add(new Purchase{
                Id = 5,
                ProductName = "Jacket",
                PurchaseDate = Convert.ToDateTime("3/6/2019 09:45:00")
            });
            return purchases;
        }

        private static List<Review> CreateReviews(List<Purchase> purchases)
        {
            List<Review> reviews = new List<Review>();
            reviews.Add(new Review{
                Id = 1,
                PurchaseId = 1,
                ReviewDate = purchases[0].PurchaseDate.AddDays(1),
                Rating = 10,
                Comment = "This is the best widget EVAH!"
            });
            reviews.Add(new Review{
                Id = 2,
                PurchaseId = 2,
                ReviewDate = purchases[1].PurchaseDate.AddDays(1),
                Rating = 8,
                Comment = "This is a really good scarf, but the sizes are a bit on the small side.  That being said I will purhcase one of the correct size and use this one as a gift."
            });
            reviews.Add(new Review{
                Id = 3,
                PurchaseId = 5,
                ReviewDate = purchases[4].PurchaseDate.AddDays(1),
                Rating = 5,
                Comment = "How is this a jacket?  It is so think it wouldn't keep my dog warm."
            });
            return reviews;
        }

    }
}

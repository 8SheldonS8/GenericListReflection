using System;
using System.Collections;
using System.Collections.Generic;

namespace Models
{
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Review> Reviews { get; set; }
    }
}

using System;

namespace Models
{
    class Purchase
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ProdcutNotes { get; set; }
    }
}
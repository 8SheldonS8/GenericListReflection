using System;

namespace Models
{
    class Review
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
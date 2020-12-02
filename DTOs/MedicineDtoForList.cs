using System;

namespace PharmaBackend.DTOs
{
    public class MedicineDtoForList
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Brand { get; set; }

        public decimal Price{ get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int ExpiringInDays { get; set; } = 0;

        public string Notes { get; set; }
    }
}
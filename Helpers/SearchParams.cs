using System;

namespace PharmaBackend.Helpers
{
    public class SearchParams
    {
        private const int MaxPageSize = 50;
        
        public int PageNumber { get; set; } = 1;
        
        private int _pageSize = 10;
        
        public int PageSize
        {
            get { return _pageSize;}
            set { _pageSize = (value) > MaxPageSize ? MaxPageSize : value;}
        }
        
        public int MedicineId { get; set; }
        
        public string FullName { get; set; }
        
        public string Brand { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
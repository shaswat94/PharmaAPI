using System;
using System.ComponentModel.DataAnnotations;

namespace PharmaBackend.Models
{
    ///<summary>
    /// Medicine Entity class.
    ///</summary>
    public class Medicine
    {
        private decimal _price;
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Brand { get; set; }
        
        [Required]
        public decimal Price 
        { 
            get 
            {
                return _price;
            }
            set
            {
                _price = Math.Round(value, 2);
            }
        }
        
        [Required]
        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }
        
        public string Notes { get; set; }
    }
}
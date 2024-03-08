using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class BookingReservation
    {
        public BookingReservation()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }
        [Required]
        public int BookingReservationId { get; set; }
        [Required]
        public DateTime? BookingDate { get; set; }
        
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte? BookingStatus { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}

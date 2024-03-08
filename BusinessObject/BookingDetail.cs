using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class BookingDetail
    {
        [Required]
        public int BookingReservationId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal? ActualPrice { get; set; }
        
        public virtual BookingReservation BookingReservation { get; set; } = null!;
        public virtual RoomInformation Room { get; set; } = null!;
    }
}

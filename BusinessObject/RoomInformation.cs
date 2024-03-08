using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class RoomInformation
    {
        public RoomInformation()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public string RoomNumber { get; set; } = null!;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        public virtual RoomType RoomType { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}

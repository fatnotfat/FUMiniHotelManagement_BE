using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class RoomType
    {
        public RoomType()
        {
            RoomInformations = new HashSet<RoomInformation>();
        }
        [Required]

        public int RoomTypeId { get; set; }
        [Required]
        public string RoomTypeName { get; set; } = null!;
        public string? TypeDescription { get; set; }
        public string? TypeNote { get; set; }

        public virtual ICollection<RoomInformation> RoomInformations { get; set; }
    }
}

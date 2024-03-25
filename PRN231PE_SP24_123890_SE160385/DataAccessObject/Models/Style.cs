﻿using System;
using System.Collections.Generic;

namespace DataAccessObject.Models
{
    public partial class Style
    {
        public Style()
        {
            WatercolorsPaintings = new HashSet<WatercolorsPainting>();
        }

        public string StyleId { get; set; } = null!;
        public string StyleName { get; set; } = null!;
        public string StyleDescription { get; set; } = null!;
        public string? OriginalCountry { get; set; }

        public virtual ICollection<WatercolorsPainting> WatercolorsPaintings { get; set; }
    }
}

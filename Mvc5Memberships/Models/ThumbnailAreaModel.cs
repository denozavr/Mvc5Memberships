using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5Memberships.Models
{
    public class ThumbnailAreaModel
    {
        public string Title { get; set; }
        public ThumbnailModel Thumbnails { get; set; }//was IEnumerable
    }
}
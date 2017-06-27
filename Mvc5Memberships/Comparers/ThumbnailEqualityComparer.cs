using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Comparers
{
    public class ThumbnailEqualityComparer : IEqualityComparer<ThumbnailModel>
    {
        //compare Thumbnails objects using ProdID not their reference
        public bool Equals(ThumbnailModel thumb1, ThumbnailModel thumb2)
        {
            return thumb1.ProductId.Equals(thumb2.ProductId);
        }

        public int GetHashCode(ThumbnailModel thumb)
        {
            return thumb.ProductId;
        }
    }
}
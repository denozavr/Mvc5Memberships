﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mvc5Memberships.Areas.Admin.Models
{
    public class EditButtonModel
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int SubscriptionId { get; set; }
        public string Link
        {
            get
            {
                var s = new StringBuilder("?");
                if (ItemId > 0) s.Append($"itemId={ItemId}&");
                if (ProductId > 0) s.Append($"productId={ProductId}&");
                if (SubscriptionId > 0) s.Append($"subscriptionId={SubscriptionId}&");
                return s.ToString().Substring(0, s.Length - 1);
            }
        }
    }
}
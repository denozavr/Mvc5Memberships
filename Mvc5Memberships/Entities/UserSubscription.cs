using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mvc5Memberships.Entities
{
    [Table("UserSubscription")]
    public class UserSubscription
    {
        //Composite	key: Subscription and AspNetUser tables
        [Required]
        [Key, Column(Order = 1)]
        public int SubscriptionId { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        [MaxLength(128)]
        public string UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
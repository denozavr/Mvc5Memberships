using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc5Memberships.Entities;

namespace Mvc5Memberships.Extenstion
{
    public static class SubscriptionExtensions
    {
        public static async Task<int> GetSubscriptionIdByRegistrationCode(
            this IDbSet<Subscription> subscription, string code)
        {
            try
            {
                if (subscription == null || code == null || code.Length <= 0)
                    return Int32.MinValue;

                var subscriptionId = await subscription.FirstOrDefaultAsync(s => s.RegistrationCode == code);
                var id = subscriptionId.Id;

                return id;
            }
            catch { return Int32.MinValue; }
        }


        public static async Task Register(this IDbSet<UserSubscription> userSubscription,
            int subscriptionId, string userId)
        {
            try
            {
                if (userSubscription == null || subscriptionId == Int32.MinValue ||
                    userId == string.Empty)
                    return;

                var exist = await Task.Run(() => userSubscription.CountAsync(
                                s => s.SubscriptionId==subscriptionId &&
                                     s.UserId==userId)) > 0;

                if (!exist)
                    await Task.Run(() => userSubscription.Add(
                        new UserSubscription
                        {
                            UserId = userId,
                            SubscriptionId = subscriptionId,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.MaxValue
                        }));
            }
            catch
            {
                Debug.WriteLine("Input parameters no valid");
            }
        }


    }



}

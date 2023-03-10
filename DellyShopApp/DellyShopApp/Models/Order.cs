using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DellyShopApp.Models
{
    public class Order
    {
        public int orgId { get; set; }
        public Guid orderGuid { get; set; }
        public int UserId { get; set; }
        public int Qty { get; set; }

        internal object Whare(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        internal static void Add(Order order)
        {
          throw new NotImplementedException();
        }

         public static IEnumerable<Order> ItemsSource { get; internal set; }
    }
}
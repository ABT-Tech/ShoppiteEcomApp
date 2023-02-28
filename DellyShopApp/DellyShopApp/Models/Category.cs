using System;

namespace DellyShopApp.Models
{
    public class Category
    {
        public string Banner { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        internal static object Which(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}

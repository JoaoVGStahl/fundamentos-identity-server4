using System.Collections.Generic;

namespace SkyCommerce.ViewObjects
{
    public class ListOf<T> where T : class
    {
        public ListOf(IEnumerable<T> collection, int total)
        {
            this.Collection = collection;
            this.Total = total;
        }

        public IEnumerable<T> Collection { get; set; }

        public int Total { get; set; }

        public bool PossuiItens => Total > 0;
    }
}

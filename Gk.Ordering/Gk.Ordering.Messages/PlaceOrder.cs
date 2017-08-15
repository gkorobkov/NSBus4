using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gk.Ordering.Messages
{
    public interface IProductMessage
    {
        Guid Id { get; set; }
        string Product { get; set; }
    }

    public class PlaceOrder : IProductMessage
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
    }
}

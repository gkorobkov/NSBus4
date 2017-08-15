using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gk.Ordering.Messages;
using NServiceBus;

namespace Gk.Ordering.Server
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }

        public void Handle(PlaceOrder message)
        {
            Console.WriteLine(@"PlaceOrder: {0} placed with id: {1}", message.Product, message.Id);
        }
    }

    public class ProductMessageHandler : IHandleMessages<IProductMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(IProductMessage message)
        {
            Console.WriteLine(@"IProductMessage: {0} placed with id: {1}", message.Product, message.Id);
        }
    }

}

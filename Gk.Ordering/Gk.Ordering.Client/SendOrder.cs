using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gk.Ordering.Messages;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;

namespace Gk.Ordering.Client
{
    [EndpointName("Gk.Ordering.Client_InputQueue")]
    public class SendOrder : IWantToRunWhenBusStartsAndStops, IWantCustomInitialization, IConfigureThisEndpoint, AsA_Client
    {

        public IBus Bus { get; set; }

        public void Init()
        {
            Configure.With();
            var configure = Configure.Instance
                .DefaultBuilder();
            Configure.Serialization.Xml();
            configure.UseInMemoryTimeoutPersister()
                .UseTransport<Msmq>()
                .UnicastBus()
                .LoadMessageHandlers()
                .DefiningMessagesAs(t =>
                {
                    if (typeof(IProductMessage) == t ||
                        t.IsAssignableFrom(typeof(IProductMessage)) ||
                        t.GetInterfaces().Any(itype =>
                            typeof(IProductMessage) == itype ||
                            typeof(IProductMessage).FullName.Equals(itype.FullName)
                        ))
                    {
                        Console.WriteLine("[DefiningMessagesAs] {0}", t.FullName);
                        return true;
                    }
                    return false;
                }
                );

        }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");
            Console.Title = "Gk.Ordering.Client";
            while (Console.ReadLine() != null)
            {
                var msg = (IProductMessage)Bus.CreateInstance(typeof(IProductMessage));

                msg.Product = "New shoes";
                var id = Guid.NewGuid();
                msg.Id = id;
                Bus.Send("Gk.Ordering.Server_InputQueue", msg);

                Console.WriteLine("==========================================================================");
                Console.WriteLine("Send a new PlaceOrder message with id: {0}", id.ToString("N"));
            }
        }
        public void Stop()
        {
        }
    }

}

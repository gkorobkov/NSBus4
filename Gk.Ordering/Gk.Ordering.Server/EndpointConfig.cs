
using System;
using System.Linq;
using Gk.Ordering.Messages;

namespace Gk.Ordering.Server
{
    using NServiceBus;

    [EndpointName("Gk.Ordering.Server_InputQueue")]
    public class EndpointConfig : IWantCustomInitialization, IConfigureThisEndpoint, AsA_Server
    {

        public void Init()
        {
            Console.Title = "Gk.Ordering.Server";
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
    }
}

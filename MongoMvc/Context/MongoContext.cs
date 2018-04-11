using log4net;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoMvc.POCOs;
using MongoMvc.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoMvc.Context
{
    public class MongoContext: IDisposable
    {
        public IMongoDatabase Database;

        public MongoContext()
        {

            //var client = new MongoClient(Settings.Default.MongoConn);         
            //Database = client.GetDatabase(Settings.Default.Database);

            //Create a settings object and add event subscribtions
            var conString = Settings.Default.MongoConn;
            var settings = MongoClientSettings.FromUrl(new MongoUrl(conString));
            var client = new MongoClient(settings);

            //setup clustor configurator to listen to events
            //settings.ClusterConfigurator = builder => builder.Subscribe<CommandStartedEvent>(started=>{

            //});

            settings.ClusterConfigurator = builder => builder.Subscribe<Log4netEvents>(started => {

            });


            Database = client.GetDatabase(Settings.Default.Database);

        }

        public IMongoCollection<Rental> Rentals => Database.GetCollection<Rental>("rentals");

        public void Dispose()
        {
           
        }
    }


    //Wiring up custom Event Handler
    internal class Log4netEvents : IEventSubscriber
    {
        public static ILog CommandStartedLog = LogManager.GetLogger("CommandStarted");
        private ReflectionEventSubscriber _Subscriber;
        public Log4netEvents()
        {
            _Subscriber = new ReflectionEventSubscriber(this);
        }

        //event tracing using log4net
        public bool TryGetEventHandler<TEvent>(out Action<TEvent> handler)
        {
            return _Subscriber.TryGetEventHandler(out handler);
        }

        public void Handle(CommandStartedEvent started)
        {
            CommandStartedLog.Info(new
            {
                started.Command,
                started.CommandName,
                started.ConnectionId,
                started.DatabaseNamespace,
                started.OperationId,
                started.RequestId
                
            
            });
        }

        public void Handle(ConnectionClosedEvent closed)
        {
            CommandStartedLog.Info(new
            {
                closed.ClusterId,
                closed.ConnectionId,
                closed.Duration,
                closed.ServerId

            });
        }

        public void Handle(CommandSucceededEvent succeed)
        {

        }


        //optional event tracing
        //public bool TryGetEventHandler<TEvent>(out Action<TEvent> handler)
        //{
        //    if(typeof (TEvent) != typeof(CommandStartedEvent))
        //    {
        //        handler = null;
        //        return false;
        //    }
        //    handler = e =>
        //     {
        //         //you can return back an event handler of your choice
        //     };
        //    return true;
        //}


    }
}
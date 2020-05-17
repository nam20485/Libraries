using System;
using System.Runtime.Remoting;

namespace Libraries.UtilityLib.Remoting
{
    public class RemotingClient<TRemoteObj> : RemotingPeer
        where TRemoteObj : RemotingObject, new()
    {
        public readonly string RemoteUri;

        protected TRemoteObj remoteObject;
        public TRemoteObj RemoteObject
        { 
            get
            { 
//                if (remoteObject == null)
//                {
//                    remoteObject = CreateRemoteObject();
//                }
                return remoteObject;
            }
        }

        public RemotingClient(string remoteUri)
        {
            RemoteUri = remoteUri;
        }

        public RemotingClient(string protocol, string host, int port, string applicationName, string endpoint)
            : this(BuildUri(protocol, host, port, applicationName, endpoint))
        {
        }

        public RemotingClient(string remoteUri, string uriEndpoint)
            : this(BuildUri(remoteUri, uriEndpoint))
        {
        }

        #region implemented abstract members of Libraries.UtilityLib.Remoting.RemotingPeer
        public override void Register()
        {
            RemotingConfiguration.RegisterWellKnownClientType(typeof(TRemoteObj), RemoteUri);
            remoteObject = CreateRemoteObject();
        }
        #endregion

        protected virtual TRemoteObj CreateRemoteObject()
        {
            //return new TRemoteObj();
            return CreateRemoteObject(null);
        }

        protected virtual TRemoteObj CreateRemoteObject(params object[] args)
        {
            return Activator.CreateInstance(typeof(TRemoteObj), args, new object[] { RemoteUri }) as TRemoteObj;
        }
    }
}


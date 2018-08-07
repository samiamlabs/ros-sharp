/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public abstract class ServiceProvider<Tin, Tout> : MonoBehaviour where Tin : Message where Tout : Message
    {
        public string serviceName;

        protected virtual void Start()
        {
            GetComponent<RosConnector>().RosSocket.AdvertiseService<Tin, Tout>(serviceName, ServiceResponseHandler);
        }

        protected abstract bool ServiceResponseHandler(Tin request, out Tout response);

    }
}

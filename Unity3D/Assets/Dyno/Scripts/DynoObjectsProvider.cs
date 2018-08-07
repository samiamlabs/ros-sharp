/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using RosSharp.RosBridgeClient.Services.Dyno;
using UnityEngine;
using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class DynoObjectsProvider : ServiceProvider<Services.Dyno.GetObjectsRequest, Services.Dyno.GetObjectsResponse>
    {
        private Messages.Dyno.Object[] objects;

        protected override void Start()
        {
            base.Start();
            objects = new Messages.Dyno.Object[2];
            objects[0] = new Messages.Dyno.Object();
            objects[1] = new Messages.Dyno.Object();

            objects[0].name = "blue box";
            objects[1].name = "red box";

        }

        protected override bool ServiceResponseHandler(GetObjectsRequest request, out GetObjectsResponse response)
        { 
            response = new Services.Dyno.GetObjectsResponse(objects);
            
            return true;
        }
    }

}

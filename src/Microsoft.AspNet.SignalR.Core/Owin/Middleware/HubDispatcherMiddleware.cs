﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;

namespace Microsoft.AspNet.SignalR.Owin.Middleware
{
    public class HubDispatcherMiddleware : PathMatchingMiddleware
    {
        private readonly HubConfiguration _configuration;

        public HubDispatcherMiddleware(OwinMiddleware next, string path, HubConfiguration configuration)
            : base(next, path)
        {
            _configuration = configuration;
        }

        protected override Task ProcessRequest(OwinRequest request, OwinResponse response)
        {
            var dispatcher = new HubDispatcher(_configuration);

            dispatcher.Initialize(_configuration.Resolver);

            return dispatcher.ProcessRequest(request.Environment);
        }
    }
}

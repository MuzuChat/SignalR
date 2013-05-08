﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.Owin;

namespace Microsoft.AspNet.SignalR.Owin.Middleware
{
    public class PersistentConnectionMiddleware : PathMatchingMiddleware
    {
        private readonly Type _connectionType;
        private readonly ConnectionConfiguration _configuration;

        public PersistentConnectionMiddleware(OwinMiddleware next,
                                              string path,
                                              Type connectionType,
                                              ConnectionConfiguration configuration)
            : base(next, path)
        {
            _connectionType = connectionType;
            _configuration = configuration;
        }

        protected override Task ProcessRequest(OwinRequest request, OwinResponse response)
        {
            var connectionFactory = new PersistentConnectionFactory(_configuration.Resolver);
            PersistentConnection connection = connectionFactory.CreateInstance(_connectionType);

            connection.Initialize(_configuration.Resolver);

            return connection.ProcessRequest(request.Environment);
        }
    }
}

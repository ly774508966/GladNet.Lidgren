﻿using GladNet.Lidgren.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Engine.Common;

namespace GladNet.Lidgren.Server.Unity
{
	public class PeerSendServiceSelectionStrategy : ISendServiceSelectionStrategy
	{
		private IAUIDService<ClientSessionServiceContext> contextService { get; }

		public PeerSendServiceSelectionStrategy(IAUIDService<ClientSessionServiceContext> contextAuidService)
		{
			if (contextAuidService == null)
				throw new ArgumentNullException(nameof(contextAuidService), $"Provided {nameof(IAUIDService<ClientSessionServiceContext>)} must not be null.");

			contextService = contextAuidService;
		}

		public INetworkMessageRouterService GetRouterService(int connectionId)
		{
			contextService.syncObj.EnterReadLock();
			try
			{
				return contextService[connectionId]?.SendService;
			}
			finally
			{
				contextService.syncObj.ExitReadLock();
			}
		}
	}
}

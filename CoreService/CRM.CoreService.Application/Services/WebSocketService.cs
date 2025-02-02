using CRM.CoreService.Application.Extensions;
using CRM.CoreService.Application.Interfaces.Services;
using CRM.CoreService.Domain.Entities.Enums;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace CRM.CoreService.Application.Services
{
	public class WebSocketService : IWebSocketService
	{
		private ConcurrentDictionary<AppNoificationEventsEnum, List<WebSocket>> subscribers = new();

		public WebSocketService()
		{

		}

		public void AddSubscriber(AppNoificationEventsEnum e, WebSocket webSocket)
		{
			if (subscribers.TryGetValue(e, out List<WebSocket>? subs))
				subs.Add(webSocket);
			else
				subscribers.TryAdd(e, new() { webSocket });
		}
		public void RemoveSubscriber(AppNoificationEventsEnum e, WebSocket webSocket)
		{
			if (!subscribers.TryGetValue(e, out List<WebSocket>? subs))
				return;

			if (subs.Count > 0)
				subs.Remove(webSocket);
			else
				subscribers.TryRemove(e, out _);
		}

		public async Task NotifySubscribers<T>(AppNoificationEventsEnum e, T args)
		{
			foreach (var subSocket in subscribers[e])
			{
				if (subSocket.State == WebSocketState.Closed)
					RemoveSubscriber(e, subSocket);
				else
					await subSocket.SendJsonAsync<T>(args);
			}
		}
	}
}

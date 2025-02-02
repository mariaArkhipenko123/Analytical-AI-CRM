using CRM.CoreService.Domain.Entities.Enums;
using System.Net.WebSockets;

namespace CRM.CoreService.Application.Interfaces.Services
{
	public interface IWebSocketService
	{
		void AddSubscriber(AppNoificationEventsEnum e, WebSocket webSocket);
		void RemoveSubscriber(AppNoificationEventsEnum e, WebSocket webSocket);
		Task NotifySubscribers<T>(AppNoificationEventsEnum e, T args);
	}
}
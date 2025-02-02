namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
	public interface IStreamManager
	{
		void AddListener(string stream);
		void RemoveListener(string stream);
		Task SendAsync<T>(string Stream, string target, string task, T Message);
		void StartListener(string stream);
		void StopListener(string stream);
		void Subscribe<T>(string stream, string task, Action<T> action);
		void Unsubscribe<T>(string stream, string task, Action<T> action);
	}
}

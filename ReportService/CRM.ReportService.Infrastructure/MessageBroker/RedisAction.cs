namespace CRM.ReportService.Infrastructure.MessageBroker
{
	internal class RedisAction
	{
		public Delegate Action { get; }
		public Type ExpectedType { get; }

		public RedisAction(Delegate action, Type expectedType)
		{
			Action = action;
			ExpectedType = expectedType;
		}

		public override bool Equals(object? obj)
		{
			if (obj is not null && obj is RedisAction other)
			{
				return Action.Method == other.Action.Method && Action.Target == other.Action.Target;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Action.Method, Action.Target, ExpectedType);
		}
	}
}

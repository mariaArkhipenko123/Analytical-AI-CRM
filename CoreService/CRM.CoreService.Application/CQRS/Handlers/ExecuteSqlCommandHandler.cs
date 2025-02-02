using CRM.CoreService.Application.CQRS.Commands;
using CRM.CoreService.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class ExecuteSqlCommandHandler : IRequestHandler<ExecuteSqlCommand, int>
    {
        private readonly IRawSqlRepository repository;
        public ExecuteSqlCommandHandler(IRawSqlRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(ExecuteSqlCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            return await repository.ExecuteCommandAsync(command.Sql, command.Parameters);
        }
    }

}

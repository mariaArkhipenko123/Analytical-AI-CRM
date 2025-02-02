using CRM.CoreService.Application.CQRS.Queries;
using CRM.CoreService.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class ExecuteSqlQueryHandler<T> : IRequestHandler<ExecuteSqlQuery<T>, List<T>> where T : class
    {
        private readonly IRawSqlRepository repository;

        public ExecuteSqlQueryHandler(IRawSqlRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<T>> Handle(ExecuteSqlQuery<T> query, CancellationToken cancellationToken)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            return await repository.ExecuteQueryAsync<T>(query.Sql, query.Parameters);
        }
    }

}

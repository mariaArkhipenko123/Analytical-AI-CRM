using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Queries
{
    public class ExecuteSqlQuery<T> : IRequest<List<T>> where T : class
    {
        public string Sql { get; }
        public object[] Parameters { get; }

        public ExecuteSqlQuery(string sql, object[] parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));

            Sql = sql;
            Parameters = parameters ?? Array.Empty<object>();
        }
    }
}

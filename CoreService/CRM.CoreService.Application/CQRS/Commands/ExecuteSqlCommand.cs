using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Commands
{
    public class ExecuteSqlCommand : IRequest<int>
    {
        public string Sql { get; }
        public object[] Parameters { get; }

        public ExecuteSqlCommand(string sql, object[] parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));

            Sql = sql;
            Parameters = parameters ?? Array.Empty<object>();
        }
    }

}

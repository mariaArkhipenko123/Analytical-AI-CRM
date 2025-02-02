using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.Interfaces.Repositories
{
    public interface IRawSqlRepository
    {
        Task<List<T>> ExecuteQueryAsync<T>(string sql, object[] parameters = null) where T : class;
        Task<int> ExecuteCommandAsync(string sql, object[] parameters = null);
    }
}

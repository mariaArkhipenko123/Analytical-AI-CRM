using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.Interfaces.Services
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        Task HandleAsync(TEvent @event);
    }
}

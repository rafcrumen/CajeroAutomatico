using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITransaccionRepository Transacciones { get; }
        
        void Complete(); 
        Task CompleteAsync(); 
    }
}

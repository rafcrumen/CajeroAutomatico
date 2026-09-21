using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITransaccionRepository Transacciones { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Transacciones = new TransaccionRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

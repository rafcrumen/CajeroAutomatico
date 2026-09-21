using System;
using System.Collections.Generic;
using System.Text;
using Repository.Dto;
using Repository.Models;

namespace Repository
{
    public interface ITransaccionRepository
    {
        public void RegistrarTransaccion(Retiro retiro);
        public List<Dinero> GetDinero();
    }
}

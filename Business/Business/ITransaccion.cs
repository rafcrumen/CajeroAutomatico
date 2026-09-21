using Repository.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface ITransaccion
    {
        public Retiro RegistrarTransaccion(decimal montoRetiro);
    }
}

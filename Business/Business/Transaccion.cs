using Repository;
using Repository.Dto;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using static Azure.Core.HttpHeader;

namespace Business
{
    public class Transaccion : ITransaccion
    {   
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public Transaccion(IUnitOfWork unitOfWork, ITransaccionRepository transaccionRepository)
        {
           _unitOfWork = unitOfWork;
           _transaccionRepository = transaccionRepository;
        }
        public Retiro RegistrarTransaccion(decimal montoRetiro)
        {
            var dinero = this._transaccionRepository.GetDinero();
            var totalDinero = CalcularTotalDinero(dinero);

            if (!HaySuficienteDinero(totalDinero, montoRetiro))
            {
                throw new InsufficientCashException(montoRetiro, totalDinero);
            }

            var retiroExitoso = AplicarRetiro(dinero, montoRetiro);
            retiroExitoso.SaldoInicial = totalDinero;
            retiroExitoso.SaldoFinal = totalDinero - montoRetiro;

            _unitOfWork.Transacciones.RegistrarTransaccion(retiroExitoso);

            _unitOfWork.Complete();

            return retiroExitoso;
        }

        public bool HaySuficienteDinero(decimal totalDinero, decimal montoRetiro)
        {
            return totalDinero >= montoRetiro;
        }
        private decimal CalcularTotalDinero(List<Dinero> dinero)
        {
            decimal total = 0;
            foreach (var d in dinero)
            {
                total += d.Denominacion * d.Existencia;
            }
            return total;
        }
        private Retiro AplicarRetiro(List<Dinero> dinero, decimal montoRetiro)
        {
            Retiro retiro = new Retiro();
            retiro.Id = new Guid();
            retiro.Dinero = new List<Dinero>();
            var dineroExistente = dinero.FindAll(d => d.Existencia > 0).OrderByDescending(d => d.Denominacion);
            foreach (var d in dineroExistente)
            {
                Dinero denominacionAplicada = new Dinero { Id = d.Id, Denominacion = d.Denominacion, Existencia = 0 };
                while (montoRetiro > 0 && montoRetiro >= (d.Denominacion * d.Existencia))
                {
                    montoRetiro -= d.Denominacion;
                    d.Existencia--;
                    denominacionAplicada.Existencia++;
                }
                if (denominacionAplicada.Existencia > 0)    
                    retiro.Dinero.Add(denominacionAplicada);
            }

            return retiro;
        }
    }
}

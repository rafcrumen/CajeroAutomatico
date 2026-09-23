using System;
using System.Collections.Generic;
using System.Text;
using Repository.Dto;
using Repository.Models;

namespace Repository
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransaccionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Dinero> GetDinero()
        {
            return  _context.Dineros.ToList();
        }

        public void RegistrarTransaccion(Retiro retiro)
        {
            Transaccione transaccion = new Transaccione
            {
                Transaccion = "Retiro",
                Fecha = DateTime.Now,
                Estatus = "Aplicado",
                Monto = retiro.SaldoInicial - retiro.SaldoFinal
            };
            _context.Transacciones.Add(transaccion);
            _context.SaveChanges();
            foreach (Dinero din in retiro.Dinero)
            {
                TransaccionDetalle detalle = new TransaccionDetalle
                {
                    TransaccionId = transaccion.Id,
                    DineroId = din.Id,
                    Cantidad = din.Existencia,
                    Monto = din.Denominacion * din.Existencia
                };
                var dineroExistente = _context.Dineros.FirstOrDefault(x => x.Id == din.Id);
                if (dineroExistente != null)
                {
                    dineroExistente.Existencia -= din.Existencia;
                }
                _context.TransaccionDetalles.Add(detalle);
            }
            //foreach (var d in retiro.Dinero)
            //{
            //    var dineroExistente = _context.Dineros.FirstOrDefault(x => x.Denominacion == d.Denominacion);
            //    if (dineroExistente != null)
            //    {
            //        dineroExistente.Existencia -= d.Existencia;
            //    }
            //}
        }

        List<Dinero> ITransaccionRepository.GetDinero()
        {
            return this._context.Dineros.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class InsufficientCashException : Exception
    {
        public decimal RequestedAmount { get; }
        public decimal AvailableAmount { get; }

        public InsufficientCashException(decimal requested, decimal available)
            : base($"No hay suficiente dinero en el cajero. Solicitado: {requested:C}, Disponible: {available:C}.")
        {
            RequestedAmount = requested;
            AvailableAmount = available;
        }
    }
}

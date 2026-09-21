using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Transaccione
{
    public int Id { get; set; }

    public string Transaccion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public decimal Monto { get; set; }

    public string Estatus { get; set; } = null!;
}

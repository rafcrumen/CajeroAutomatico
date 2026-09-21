using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class TransaccionDetalle
{
    public int Id { get; set; }

    public int TransaccionId { get; set; }

    public int DineroId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Monto { get; set; }
}

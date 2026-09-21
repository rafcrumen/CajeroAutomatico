using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Dinero
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal Denominacion { get; set; }

    public int Existencia { get; set; }
}

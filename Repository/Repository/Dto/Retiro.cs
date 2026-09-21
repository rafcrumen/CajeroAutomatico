
using Repository.Models;

namespace Repository.Dto
{
    public class Retiro
    {   
        public Guid Id { get; set; }
        public List<Dinero> Dinero { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }



    }
}

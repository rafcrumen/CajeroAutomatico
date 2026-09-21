using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Dto;

namespace CajeroAutomatico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetiroController : ControllerBase

    {
        private ITransaccion _transaccion;
        public RetiroController(ITransaccion transaccion)
        {
            _transaccion = transaccion;
        }
        [HttpGet]
        public Retiro RetirarDinero(decimal montoRetiro)
        {
            var retiro = this._transaccion.RegistrarTransaccion(montoRetiro);
                        return retiro;
        }
    }
}

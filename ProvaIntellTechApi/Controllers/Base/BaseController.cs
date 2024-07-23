using Microsoft.AspNetCore.Mvc;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Results.Enums;
using ProvaIntellTechApi.Service.Results;

namespace ProvaIntellTechApi.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected IActionResult GetIActionResult(Result result)
        {
            if (result == null)
            {
                return StatusCode((int)StatusCodeResultEnum.InternalServerError);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        protected IActionResult GetIActionResult<T>(Result<T> result)
        {
            if (result == null)
            {
                return StatusCode((int)StatusCodeResultEnum.InternalServerError);
            }
            if (result.StatusCode == StatusCodeResultEnum.NoContent)
            {
                return StatusCode((int)result.StatusCode);
            }
            if (!OperacaoValida())
            {
                return StatusCode((int)StatusCodeResultEnum.BadRequest, result);
            }

            return StatusCode((int)result.StatusCode, result);
        }
    }
}

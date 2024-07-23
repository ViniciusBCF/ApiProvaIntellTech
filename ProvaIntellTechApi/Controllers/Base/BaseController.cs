using Microsoft.AspNetCore.Mvc;
using ProvaIntellTechApi.Service.Notifications.Interfaces;

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
    }
}

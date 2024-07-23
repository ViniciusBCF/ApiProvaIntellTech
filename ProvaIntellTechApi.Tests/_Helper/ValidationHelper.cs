using FluentValidation;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Service.Base;

namespace ProvaIntellTechApi.Tests._Helper
{
    public class ValidationHelper : BaseService
    {
        public ValidationHelper(INotificador notificador) : base(notificador)
        {
        }

        new public bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}

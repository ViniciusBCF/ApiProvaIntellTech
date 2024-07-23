using FluentValidation;
using ProvaIntellTechApi.Domain._Helper;
using ProvaIntellTechApi.Service.ViewModel;

namespace ProvaIntellTechApi.Service.Validation
{
    public class AtividadeValidation : AbstractValidator<AtividadeViewModel>
    {
        public AtividadeValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage(Constantes.CampoVazioErrorMsg(nameof(AtividadeViewModel.Nome)))
                .MaximumLength(Constantes.Numero128)
                .WithMessage(Constantes.CampoMaiorErrorMsg(nameof(AtividadeViewModel.Nome), Constantes.Numero128));

            RuleFor(a => a.Descricao)
                .NotEmpty()
                .WithMessage(Constantes.CampoVazioErrorMsg(nameof(AtividadeViewModel.Descricao)))
                .MaximumLength(Constantes.Numero512)
                .WithMessage(Constantes.CampoMaiorErrorMsg(nameof(AtividadeViewModel.Descricao), Constantes.Numero512));

            RuleFor(a => a.TipoAtividade)
               .NotNull()
               .WithMessage(Constantes.CampoVazioErrorMsg(nameof(AtividadeViewModel.TipoAtividade)));
        }
    }
}

using AutoMapper;
using ProvaIntellTechApi.Data.Repositories.Interfaces;
using ProvaIntellTechApi.Domain._Helper;
using ProvaIntellTechApi.Domain.Entities;
using ProvaIntellTechApi.Domain.Entities.Enums;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Results;
using ProvaIntellTechApi.Service.Results.Enums;
using ProvaIntellTechApi.Service.Service.Base;
using ProvaIntellTechApi.Service.Service.Interfaces;
using ProvaIntellTechApi.Service.Validation;
using ProvaIntellTechApi.Service.ViewModel;

namespace ProvaIntellTechApi.Service.Service
{
    public class AtividadeService : BaseService, IAtividadeService
    {
        private readonly IAtividadeRepository _atividadeRepository;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;

        public AtividadeService(IAtividadeRepository atividadeRepository,
                                INotificador notificador,
                                IMapper mapper) : base(notificador)
        {
            _atividadeRepository = atividadeRepository;
            _mapper = mapper;
            _notificador = notificador;
        }

        public async Task<Result<IEnumerable<AtividadeViewModel>>> ObterTodosAsync()
        {
            var result = new Result<IEnumerable<AtividadeViewModel>>();
            try
            {
                var lista = _mapper.Map<List<AtividadeViewModel>>(await _atividadeRepository.ObterTodosAsync());
                if (lista.Any())
                {
                    result.Data = lista;
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = [Constantes.RetornandoAtividadesMsg(lista.Count)];
                }
                else
                {
                    result.StatusCode = StatusCodeResultEnum.NoContent;
                }
            }
            catch (Exception ex)
            {

                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = [ex.Message];
            }
            return result;
        }

        public async Task<Result<AtividadeViewModel>> ObterPorIdAsync(Guid id)
        {
            var result = new Result<AtividadeViewModel>();
            try
            {
                var obj = await _atividadeRepository.ObterPorIdAsync(id);
                if (obj is null)
                {
                    result.Message = [Constantes.AtividadeNaoEncontradaErrorMsg];
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;
                }
                else
                {
                    result.Data = _mapper.Map<AtividadeViewModel>(obj);
                    result.StatusCode = StatusCodeResultEnum.Ok;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = [ex.Message];
            }
            return result;
        }

        public async Task<Result<AtividadeViewModel>> AdicionarAsync(AtividadeViewModel atividade)
        {
            if (!ExecutarValidacao(new AtividadeValidation(), atividade))
            {
                return new Result<AtividadeViewModel>()
                {
                    Data = atividade,
                    IsValid = false,
                    StatusCode = StatusCodeResultEnum.BadRequest,
                    Message = _notificador.ObterMensagensNotificacoes()
                };
            }

            var atividadeAdicionada = await _atividadeRepository.AdicionarAsync(_mapper.Map<Atividade>(atividade));
            return new Result<AtividadeViewModel>()
            {
                Data = _mapper.Map<AtividadeViewModel>(atividadeAdicionada),
                IsValid = true,
                StatusCode = StatusCodeResultEnum.Ok
            };
        }

        public async Task<Result<AtividadeViewModel>> AtualizarAsync(Guid id, AtividadeViewModel atividade)
        {
            if (!ExecutarValidacao(new AtividadeValidation(), atividade))
            {
                return new Result<AtividadeViewModel>()
                {
                    Data = atividade,
                    IsValid = false,
                    StatusCode = StatusCodeResultEnum.BadRequest,
                    Message = _notificador.ObterMensagensNotificacoes()
                };
            }
            try
            {
                var objAlteracao = await _atividadeRepository.ObterPorIdAsync(id);
                if (objAlteracao is null)
                {
                    return new Result<AtividadeViewModel>()
                    {
                        Message = [Constantes.AtividadeNaoEncontradaErrorMsg],
                        IsValid = false,
                        StatusCode = StatusCodeResultEnum.NotFound
                    };
                }
                objAlteracao.AlterarNome(atividade.Nome);
                objAlteracao.AlterarDescricao(atividade.Descricao);
                objAlteracao.AlterarTipoAtividade(atividade.TipoAtividade);

                await _atividadeRepository.AtualizarAsync(objAlteracao);
                return new Result<AtividadeViewModel>()
                {
                    Data = _mapper.Map<AtividadeViewModel>(objAlteracao),
                    StatusCode = StatusCodeResultEnum.Ok,
                };
            }
            catch (Exception ex)
            {
                return new Result<AtividadeViewModel>()
                {
                    StatusCode = StatusCodeResultEnum.InternalServerError,
                    IsValid = false,
                    Message = [ex.Message]
                };
            }
        }

        public async Task<Result<AtividadeViewModel>> RemoverAsync(Guid id)
        {
            try
            {
                var objDelecao = await _atividadeRepository.ObterPorIdAsync(id);
                if (objDelecao is null)
                {
                    return new Result<AtividadeViewModel>()
                    {
                        Message = [Constantes.AtividadeNaoEncontradaErrorMsg],
                        IsValid = false,
                        StatusCode = StatusCodeResultEnum.NotFound
                    };
                }
                await _atividadeRepository.RemoverAsync(id);
                return new Result<AtividadeViewModel>()
                {
                    Data = _mapper.Map<AtividadeViewModel>(objDelecao),
                    StatusCode = StatusCodeResultEnum.Ok,
                    Message = [Constantes.AtividadeRemovidaMsg]
                };
            }
            catch (Exception ex)
            {
                return new Result<AtividadeViewModel>()
                {
                    StatusCode = StatusCodeResultEnum.InternalServerError,
                    IsValid = false,
                    Message = [ex.Message]
                };
            }
        }

        public async Task<Result<IEnumerable<object>>> ObterTiposAtividade()
        {
            var values = Enum.GetValues(typeof(TipoDeAtividade))
                             .Cast<TipoDeAtividade>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString()
                             })
                             .Cast<object>();

            return new Result<IEnumerable<object>>
            {
                Data = values,
                StatusCode = StatusCodeResultEnum.Ok,
            };
        }

        public void Dispose()
        {
            _atividadeRepository?.Dispose();
        }
    }
}

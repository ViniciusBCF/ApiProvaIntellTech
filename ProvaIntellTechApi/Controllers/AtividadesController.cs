using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProvaIntellTechApi.Controllers.Base;
using ProvaIntellTechApi.Domain.Entities.Enums;
using ProvaIntellTechApi.Service.DTOs;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Results;
using ProvaIntellTechApi.Service.Results.Enums;
using ProvaIntellTechApi.Service.Service.Interfaces;
using ProvaIntellTechApi.Service.ViewModel;

namespace ProvaIntellTechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadesController : BaseController
    {

        private readonly IAtividadeService _atividadeService;
        private readonly IMapper _mapper;

        public AtividadesController(IAtividadeService atividadeService,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _atividadeService = atividadeService;
            _mapper = mapper;
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            return GetIActionResult(await _atividadeService.ObterTodosAsync());
        }

        [HttpGet("retornarPorId/{id:guid}")]
        public async Task<IActionResult> RetornaPorId([FromRoute] Guid id)
        {
            return GetIActionResult(await _atividadeService.ObterPorIdAsync(id));
        }

        [HttpGet("tiposAtividade")]
        public async Task<IActionResult> ObterTiposAtividade()
        {

            return GetIActionResult(await _atividadeService.ObterTiposAtividade());
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AtividadeDto atividade)
        {
            return GetIActionResult(await _atividadeService.AdicionarAsync(_mapper.Map<AtividadeViewModel>(atividade)));
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtividadeDto atividade)
        {
            return GetIActionResult(await _atividadeService.AtualizarAsync(id, _mapper.Map<AtividadeViewModel>(atividade)));
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {

            return GetIActionResult(await _atividadeService.RemoverAsync(id));
        }

        private IActionResult GetIActionResult(Result result)
        {
            if (result == null)
            {
                return StatusCode((int)StatusCodeResultEnum.InternalServerError);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        private IActionResult GetIActionResult<T>(Result<T> result)
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

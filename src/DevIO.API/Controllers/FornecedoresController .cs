using AutoMapper;
using DevIO.API.ViewModels;
using DevIO.Business.Entidades;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IMapper mapper,
                                      IFornecedorRepositorio fornecedorRepositorio,
                                      IFornecedorService fornecedorService,
                                      INotificador notificador) : base(notificador)

        {
            _mapper = mapper;
            _fornecedorRepositorio = fornecedorRepositorio;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepositorio.ObterTodos());

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id); 

            if (fornecedor == null) return NotFound();

            return fornecedor;

        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(HttpStatusCode.Created, fornecedorViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(HttpStatusCode.NoContent);

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            await _fornecedorService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);

        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterFornecedorProdutosEndereco(id));
        }

    }

}

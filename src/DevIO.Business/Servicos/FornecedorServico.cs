using DevIO.Business.Entidades;
using DevIO.Business.Entidades.Validacoes;
using DevIO.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Servicos
{
    public class FornecedorServico : BaseServico, IFornecedorService
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;


        public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio,
            INotificador notificador) : base (notificador)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
        }


        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidacao(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidacao(), fornecedor.Endereco)) return;
            
            if(_fornecedorRepositorio.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepositorio.Adicionar(fornecedor);
           
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidacao(), fornecedor)) return;

            if(_fornecedorRepositorio.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento.");
            }

            await _fornecedorRepositorio.Atualizar(fornecedor);
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepositorio.ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null)
            {
                Notificar("Fornecedor não existe.");
                return;
            }

            if (fornecedor.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados");
                return;
            }

            var endereco = await _fornecedorRepositorio.ObterEnderecoPorFornecedor(id);
            
            if (endereco != null)
            {
                await _fornecedorRepositorio.RemoverEnderecoPorFornecedor(endereco);
            }

            await _fornecedorRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepositorio?.Dispose();
        }
    }
}

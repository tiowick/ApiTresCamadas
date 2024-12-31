using DevIO.Business.Entidades;
using DevIO.Business.Entidades.Validacoes;
using DevIO.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Servicos
{
    public class ProdutoServico : BaseServico, IProdutoService
    {
        private readonly IProdutoRepositorio _produtoRepositorio;


        public ProdutoServico(IProdutoRepositorio produtoRepositorio,
            INotificador notificador) : base (notificador)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return;

            await _produtoRepositorio.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return;

            await _produtoRepositorio.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepositorio?.Dispose();
        }
    }
}


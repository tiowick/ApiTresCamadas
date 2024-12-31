using DevIO.Business.Entidades;
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


        public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
        }


        public async Task Adicionar(Fornecedor fornecedor)
        {
            // validar se a entidade é consistente
            // validar se não ja existe outro fornecedor com o mesmo documento

           await _fornecedorRepositorio.Adicionar(fornecedor);
           
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            await _fornecedorRepositorio.Atualizar(fornecedor);
        }

        public async Task Remover(Guid id)
        {
            await _fornecedorRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepositorio?.Dispose();
        }
    }
}

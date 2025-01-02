using DevIO.Business.Entidades;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repositorio
{
    public class FornecedorRepositorio : Repositorio<Fornecedor>, IFornecedorRepositorio
    {
        public FornecedorRepositorio(AppDbContext context) : base(context) 
        {
            
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Endereco)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Endereco?> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }

        public async Task RemoverEnderecoPorFornecedor(Endereco endereco)
        {
            Db.Enderecos.Remove(endereco);
            await SaveChanges();
        }
    }
}

using DevIO.Business.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)"); // sevindo para cnpj também

            // Relação 1 pra 1
            // 1 : 1 Fornecedor : Endereco

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Fornecedor);

            // Relação 1 pra N
            // 1 : N Fornecedor: Produtos

            builder.HasMany(c => c.Produtos)
                .WithOne(c => c.Fornecedor)
                .HasForeignKey(c => c.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}

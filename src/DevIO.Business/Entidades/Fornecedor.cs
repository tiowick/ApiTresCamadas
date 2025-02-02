﻿using DevIO.Business.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Entidades
{
    public class Fornecedor : Entity
    {
        public string? Nome { get; set; }

        public string? Documento { get; set; }

        public TipoFornecedor TipoFornecedor { get; set; }

        public bool Ativo { get; set; }

        public Endereco? Endereco { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}

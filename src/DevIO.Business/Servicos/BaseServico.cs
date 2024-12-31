using DevIO.Business.Entidades;
using DevIO.Business.Entidades.Validacoes.Documentos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Servicos
{
    public abstract class BaseServico
    {
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) 
            where TV : AbstractValidator<TE>
            where TE : Entity
        {

            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            // lançamento de noty

            return false;
        }





    }
}

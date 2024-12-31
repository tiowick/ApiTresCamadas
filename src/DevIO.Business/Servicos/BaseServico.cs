﻿using DevIO.Business.Entidades;
using DevIO.Business.Entidades.Validacoes.Documentos;
using DevIO.Business.Interfaces;
using DevIO.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Servicos
{
    public abstract class BaseServico
    {
        private readonly INotificador _notificador;

        protected BaseServico(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) 
            where TV : AbstractValidator<TE>
            where TE : Entity
        {

            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }





    }
}

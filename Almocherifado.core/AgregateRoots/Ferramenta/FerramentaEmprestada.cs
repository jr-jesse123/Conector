﻿using System;
using Almocherifado.core.AgregateRoots.EmprestimoNm;

namespace Almocherifado.core.AgregateRoots.FerramentaNm
{
    public class FerramentaEmprestada
    {
        private FerramentaEmprestada()
        {

        }
        public FerramentaEmprestada(Ferramenta ferramenta)
        {
            Ferramenta = ferramenta;
        }

        public void AcusarRecebimento()
        {
            if(!DataDevolucao.HasValue)
            DataDevolucao = DateTime.Now;
        }


        public bool Recebido
        {
            get => DataDevolucao is null ? false : true;
        }

        public int Id { get; }
        public virtual Emprestimo Emprestimo { get; }
        public DateTime? DataDevolucao { get; private set; }
        public virtual Ferramenta Ferramenta { get; }

    }
}

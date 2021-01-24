﻿using System;

namespace Almocherifado.core
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
            DataDevolucao = DateTime.Now;
        }

        public int Id { get; }
        public virtual Emprestimo Emprestimo { get; set; }
        public DateTime? DataDevolucao { get; private set; }
        public virtual Ferramenta Ferramenta { get; }
    }
}

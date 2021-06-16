using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Almocherifado.UI.Components.Helpers;
using Syncfusion.Blazor.Inputs;

namespace Almocherifado.UI.Components.Models
{



    public record CadastroFerramentaModel 
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public int Patrimonio { get; set; }
        public IEnumerable<UploadFiles> Fotos { get; set; }
        public string Descricao { get; set; }


    }

}

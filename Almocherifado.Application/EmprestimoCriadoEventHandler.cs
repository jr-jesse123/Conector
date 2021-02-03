using Almocherifado.core.Commom;
using Almocherifado.InfraEstrutura;
using System.Threading.Tasks;

namespace Almocherifado.Application
{
    [DomainEventHandler]
    public class EmprestimoCriadoEventHandler : IHandler<EmprestimoCriadoEvent>
    {
        public void HandleAsync(EmprestimoCriadoEvent DomainEvent)
        {
            var nome = DomainEvent.Emprestimo.Funcionario.Nome;
            var text = $"Sr {nome}, você recebeu umas ferramenta aí hein";
             new MailSender().sendAsync(text).Wait();
        }
    }
}

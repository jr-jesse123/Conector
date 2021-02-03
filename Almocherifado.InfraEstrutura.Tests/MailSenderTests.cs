using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class MailSenderTests
    {
        [Fact]
        public async Task testAsync()
        {
            var sender = new MailSender();
            await sender.sendAsync("teste");

        }
    }
}

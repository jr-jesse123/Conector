using FluentAssertions;
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Almocherifado.InfraEstrutura
{
    public class MailSender
    {
        public async Task sendAsync(string text)
        {
            var services = new ServiceCollection();

            services
             .AddFluentEmail("defaultsender@test.test")
             .AddSmtpSender("localhost", 25);
            
            var email = services.BuildServiceProvider().GetRequiredService<IFluentEmail>();

            var response = await email
                .SetFrom("john@email.com", "jhon")
                .To("bob@email.com", "bob")
                .Subject("hows it going bob")
                .Body(text)
                .SendAsync();

            response.Successful.Should().BeTrue();
        }
    }
}

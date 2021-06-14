using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Almocherifado.UI.Components.Profiles;
using AutoFixture;
using Almocherifado.UI.Components.Models;
using Entities;
using Moq;
using Syncfusion.Blazor.Inputs;
using Xunit.Abstractions;

namespace Almocherifado.UI.Tests.Properties
{
    public class ProfileTests
    {
        private readonly ITestOutputHelper outputHelper;

        public ProfileTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }


        [Fact]
        public void FerramentaModelEhConvertidaParaDominioCorretamente()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LeanProfile());
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var fotosStub = new Mock<UploadFiles>();

            var ferramentaModel = new Fixture()
                    .Build<CadastroFerramentaModel>()
                    .With(cf => cf.Fotos, new UploadFiles[] { fotosStub.Object })
                    .Create();

            var domain = mapper.Map<Ferramenta>(ferramentaModel);

            outputHelper.WriteLine(domain.ToString());
            

        }
    }
}

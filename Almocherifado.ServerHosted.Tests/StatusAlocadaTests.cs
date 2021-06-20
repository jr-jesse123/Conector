using Bunit;
using FsCheck;
using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Almocherifado.UI.Tests
{
    public static class AlocacaoGenerator
    {

    }
    public class StatusAlocadaTests : TestContext
    {
        private readonly ITestOutputHelper outputHelper;

        public StatusAlocadaTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        //[Property()]
        public Property Render()
        {
            throw new NotImplementedException();
        }
    }
}

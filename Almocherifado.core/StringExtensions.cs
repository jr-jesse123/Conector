using CSharpFunctionalExtensions;
using System.Linq;

namespace Almocherifado.core
{
    public static class StringExtensions
    {
        public static string SomenteNumeros(this string cpfStr)
        {
            var caracteresArray = cpfStr.Where(c => char.IsDigit(c)).ToArray();
            return new string(caracteresArray);
        }
    }

    


}

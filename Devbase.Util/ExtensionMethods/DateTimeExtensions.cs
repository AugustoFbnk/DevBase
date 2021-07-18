using System;

namespace DevBase.Util.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static int CalcularIdade(this DateTime dataNascimento)
        {
            int YearsOld = (DateTime.Today.Year - dataNascimento.Year);
            if (DateTime.Today.Month < dataNascimento.Month || (DateTime.Today.Month == dataNascimento.Month && DateTime.Today.Day < dataNascimento.Day))
                YearsOld--;

            return (YearsOld < 0) ? 0 : YearsOld;
        }
    }
}

namespace DevBase.Util.ExtensionMethods
{
    public static class IntExtensions
    {
        public static int TryToInt(this object valor)
        {
            int.TryParse(valor.ToString(), out int valorConvertido);

            return valorConvertido;
        }

    }
}

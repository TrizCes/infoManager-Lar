namespace infoManagerAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensagem)
            : base(mensagem) { }
    }
}

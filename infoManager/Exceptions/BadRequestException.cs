namespace infoManagerAPI.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string mensagem)
            : base(400, mensagem) { }

    }
}

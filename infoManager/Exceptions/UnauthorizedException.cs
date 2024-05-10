namespace infoManagerAPI.Exceptions
{
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException(string mensagem)
            : base(401, mensagem) { }
    }
}

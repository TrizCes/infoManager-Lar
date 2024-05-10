namespace infoManagerAPI.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string mensagem)
            : base(404, mensagem) { }
    }
}

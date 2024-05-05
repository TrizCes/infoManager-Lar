namespace infoManagerAPI.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string mensagem)
            : base(mensagem) { }
    }
}

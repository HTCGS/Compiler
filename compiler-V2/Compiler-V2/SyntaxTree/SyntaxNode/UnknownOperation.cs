namespace CompilerV2
{
    public class UnknownOperation : Operation
    {
        public UnknownOperation(string errorMessage) : base(null, null)
        {
            this.Name = errorMessage;
        }
    }
}
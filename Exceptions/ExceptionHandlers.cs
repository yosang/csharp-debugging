namespace ExceptionHandlers
{
    // Custom Exception class
    public class NumTooLow : Exception
    {
        // Constructoir inherits message from Exception base class with base.
        public NumTooLow(string message) : base(message) { }
    }
}
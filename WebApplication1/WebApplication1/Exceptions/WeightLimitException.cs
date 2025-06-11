namespace WebApplication1.Exceptions;

public class WeightLimitException : Exception
{
    public WeightLimitException()
    {
    }

    public WeightLimitException(string? message) : base(message)
    {
    }

    public WeightLimitException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
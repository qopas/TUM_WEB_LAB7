namespace Application.Exceptions;

public class UnauthorizedException(string message = "Unauthorized access") : ApplicationException(message);
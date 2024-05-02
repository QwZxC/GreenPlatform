using Microsoft.AspNetCore.Http;

namespace Common.Exceptions;

public class NotFoundException : BadHttpRequestException
{
    public NotFoundException(string message) 
        : base(message, 404) 
    {

    }
}

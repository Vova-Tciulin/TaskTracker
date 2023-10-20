namespace Tasks.Cmd.Domain.Exceptions;

public class NotFoundException:Exception 
{
    public NotFoundException(string msg)
        :base(msg)
    {
        
    }
}
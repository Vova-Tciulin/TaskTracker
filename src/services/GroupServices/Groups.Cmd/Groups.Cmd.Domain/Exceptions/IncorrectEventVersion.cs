namespace Groups.Cmd.Domain.Exceptions;

public class IncorrectEventVersion:Exception 
{
    public IncorrectEventVersion(string msg)
        :base(msg)
    {
        
    }
}
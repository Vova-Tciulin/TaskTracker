﻿namespace Groups.Query.Domain.Exceptions;

public class NotFoundException:Exception 
{
    public NotFoundException(string msg)
        :base(msg)
    {
        
    }
}
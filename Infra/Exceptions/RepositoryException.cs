﻿namespace Infra.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string message) : base(message)
    {
    }
}

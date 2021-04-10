using System;

namespace konnta0.Exceptions
{
    public interface IErrors
    {
        Exception Exception { get; }
        bool Is<T>() where T : Exception;
        T Get<T>() where T : Exception;
    }
}
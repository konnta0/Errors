using System;
using System.Threading.Tasks;

namespace konnta0.Exceptions
{
    public class Errors : IErrors
    {
        private Errors(Exception e)
        {
            Exception = e;
        }

        public static IErrors New(string message)
        {
            return New(new Exception(message));
        }

        public static IErrors New(Exception e)
        {
            return new Errors(e);
        }

        public static IErrors New<T>() where T : Exception, new()
        {
            return new Errors(new T());
        }

        public static IErrors Nothing()
        {
            return null;
        }

        public static bool IsOccurred(IErrors error)
        {
            return error != Nothing();
        }

        public static async Task<IErrors> TryTask(Task<IErrors> task)
        {
            if (task == null) return Nothing();

            try
            {
                var error = await task;
                if (IsOccurred(error)) return error;
            }
            catch (Exception e)
            {
                return New(e);
            }

            return Nothing();
        }

        public static async Task<(T, IErrors)> TryTask<T>(Task<(T, IErrors)> task)
        {
            if (task is null) return (default, Nothing());

            T result;
            try
            {
                var (r, error) = await task;
                if (error != null) return (default, error);
                result = r;
            }
            catch (Exception e)
            {
                return (default, New(e));
            }

            return (result, Nothing());
        }

        public static async Task<IErrors> TryTask(Task task)
        {
            if (task is null) return Nothing();

            try
            {
                await task;
            }
            catch (Exception e)
            {
                return New(e);
            }

            return Nothing();
        }

#if !UNUSE_VALUE_TASK
        public static async ValueTask<IErrors> TryValueTask(ValueTask<IErrors> task)
        {
            if (task == default) return Nothing();

            try
            {
                var error = await task;
                if (IsOccurred(error)) return error;
            }
            catch (Exception e)
            {
                return New(e);
            }

            return Nothing();
        }

        public static async ValueTask<(T, IErrors)> TryValueTask<T>(ValueTask<(T, IErrors)> task)
        {
            if (task == default) return (default, Nothing());

            T result;
            try
            {
                var (r, error) = await task;
                if (error != null) return (default, error);
                result = r;
            }
            catch (Exception e)
            {
                return (default, New(e));
            }

            return (result, Nothing());
        }

        public static async ValueTask<IErrors> ValueTryTask(ValueTask task)
        {
            if (task == default) return Nothing();

            try
            {
                await task;
            }
            catch (Exception e)
            {
                return New(e);
            }

            return null;
        }        
        
#endif
        public static IErrors Try(Action action)
        {
            if (action is null) return Nothing();

            try
            {
                action();
            }
            catch (Exception e)
            {
                return New(e);
            }

            return null;
        }

        public Exception Exception { get; }

        public bool Is<T>() where T : Exception
        {
            return Exception is T;
        }

        public T Get<T>() where T : Exception
        {
            if (!Is<T>()) return null;
            return (T) Exception;
        }
    }
}
# Errors
C# Error(Exception) handler.

# Getting Started
use NuGet
```
PM> Install-Package konnta0.Errors 
```
See below for other installation instructions.

https://www.nuget.org/packages/konnta0.Errors/


## Simply example

```cs
var errors = Errors.Try(() =>
{
    // Processing that may raise an exception here.
});

if (Errors.IsOccurred(errors))
{
    // Some kind of exception occurred !!
    return errors;
}

return Errors.Nothing();
```

You can identify the exception as follows
```cs
if (errors.Is<InvalidProgramException>())
{
    // This error is InvalidProgramException
}

```

```cs
var exception = errors.Get<InvalidProgramException>();
if (exception != null)
{
    // This error is InvalidProgramException
}
```

You can make a new Error.
```
Errors.New("new error message here.")
```


You can return any value and Error.
```cs
var (ret, error) = Errors.Try<int>(() =>
{
    // Processing that may raise an exception here.
    return 1234;
});
```



It's also compatible with Task and ValueTask.　Use `TryTask` and `ValueTask`.


ValueTask can be toggled ON and OFF with `UNUSE_VALUE_TASK` define.

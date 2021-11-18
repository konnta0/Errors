using System;
using Xunit;

namespace Errors.Test
{
    using E = konnta0.Exceptions;
    public class Test
    {
        [Fact]
        public void NewInstance()
        {
            var error = E.Errors.New("hello error");
            Assert.True(E.Errors.IsOccurred(error));
            Assert.Equal("hello error", error.Get<Exception>().Message);
        }

        [Fact]
        public void NewInstanceGeneric()
        {
            var error = E.Errors.New<NotImplementedException>();
            Assert.NotNull(error);
            Assert.True(error.Is<NotImplementedException>());
        }

        [Fact]
        public void Try()
        {
            var error = E.Errors.Try(() => throw new NotImplementedException());
            Assert.NotNull(error);
            Assert.True(error.Is<NotImplementedException>());
            error = E.Errors.Try(() => {});
            Assert.Null(error);
        }
        
        [Fact]
        public void TryGeneric()
        {
            var (ret, error) = E.Errors.Try<int>(delegate
            {
                throw new NotImplementedException();
                return 0;
            });
            Assert.NotNull(error);
            Assert.True(error.Is<NotImplementedException>());
            Assert.Equal(default(int), ret);
            (ret, error) = E.Errors.Try<int>(() => 1234);
            Assert.Null(error);
            Assert.Equal(1234, ret);

            (ret, error) = E.Errors.Try(() => (1234, E.Errors.New("dummy error")));
            Assert.NotNull(error);
            Assert.Equal("dummy error", error.Exception.Message);
            Assert.Equal(default(int), ret);
            (ret, error) = E.Errors.Try<int>(() => (1234, E.Errors.Nothing()));
            Assert.Null(error);
            Assert.Equal(1234, ret);
        }
    }
}
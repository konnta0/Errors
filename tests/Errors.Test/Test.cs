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
    }
}
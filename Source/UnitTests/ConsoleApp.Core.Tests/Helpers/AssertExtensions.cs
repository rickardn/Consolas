using System;
using Should;
using Should.Core.Exceptions;

namespace Consolas.Core.Tests.Helpers
{
    public static class AssertExtensions
    {
        public static void ShouldThrow<TException>(this Action action, Action<TException> ex) where TException : Exception
        {
            try
            {
                action();
                ((Type)null).ShouldEqual(typeof(TException));
            }
            catch (EqualException)
            {
                throw;
            }
            catch (Exception exception)
            {
                exception.GetType().ShouldEqual(typeof(TException));
                ex((TException) exception);
            }
        }
    }
}
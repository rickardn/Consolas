using System;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class CommandContextTests
    {
        [Test]
        public void Ctor_Empty_CommandIsNotSet()
        {
            var context = new CommandContext();
            context.Command.ShouldBeNull();
            context.Assembly.ShouldBeNull();
        }

        [Test]
        public void Ctor_Command_CommandIsSet()
        {
            var command = Substitute.For<Command>();
            var context = new CommandContext(command);
            context.Command.ShouldEqual(command);
        }

        [Test]
        public void Ctor_Command_AssemblyIsSet()
        {
            var command = Substitute.For<Command>();
            var assembly = command.GetType().Assembly;
            var context = new CommandContext(command);
            context.Assembly.ShouldEqual(assembly);
        }

        [Test]
        public void Ctor_CommandIsNull_ThrowsException()
        {
            Action ctor = () => new CommandContext(command: null);
            ctor.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Ctor_Assembly_AssemblyIsSet()
        {
            var assembly = GetType().Assembly;
            var context = new CommandContext(assembly);
            context.Assembly.ShouldEqual(assembly);
        }

        [Test]
        public void Ctor_AssemblyIsNull_ThrowsException()
        {
            Action ctor = () => new CommandContext(assembly: null);
            ctor.ShouldThrow<ArgumentNullException>();
        }
    }
}

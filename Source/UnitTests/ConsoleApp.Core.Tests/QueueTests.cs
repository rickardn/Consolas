using System;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class QueueTests
    {
        [Test]
        public void ReferenceQueueStory()
        {
            var que = new System.Collections.Generic.Queue<string>();

            que.Enqueue("foo");
            que.Count.ShouldEqual(1);
            que.Peek().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("foo");
            que.Count.ShouldEqual(0);

            que.Enqueue("foo");
            que.Enqueue("bar");
            que.Count.ShouldEqual(2);
            que.Peek().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("bar");
            que.Count.ShouldEqual(0);

            que.Enqueue("foo");
            que.Enqueue("bar");
            que.Enqueue("baz");
            que.Clear();
            que.ShouldBeEmpty();
        }

        [Test]
        public void QueueStory()
        {
            var que = new Queue<string>();

            que.Enqueue("foo");
            que.Count.ShouldEqual(1);
            que.Peek().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("foo");
            que.Count.ShouldEqual(0);

            que.Enqueue("foo");
            que.Enqueue("bar");
            que.Count.ShouldEqual(2);
            que.Peek().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("foo");
            que.Dequeue().ShouldEqual("bar");
            que.Count.ShouldEqual(0);

            que.Enqueue("foo");
            que.Enqueue("bar");
            que.Enqueue("baz");
            que.Clear();
            que.ShouldBeEmpty();
        }

        [Test]
        public void DeDequeue()
        {
            var que = new Queue<string>();

            que.Enqueue("foo");
            que.Enqueue("bar");

            que.Dequeue();
            que.Enqueue("baz");
            que.Peek().ShouldEqual("bar");

            que.DeDequeue("foo");
            que.Peek().ShouldEqual("foo");

            que.Clear();
            que.DeDequeue("bar");
            que.Peek().ShouldEqual("bar");
        }

        [Test]
        public void Peek_Empty_ThrowsException()
        {
            var que = new Queue<string>();
            Assert.Throws<InvalidOperationException>(() => que.Peek());
        }

        [Test]
        public void Dequeue_Empty_ThrowsException()
        {
            var que = new Queue<string>();
            Assert.Throws<InvalidOperationException>(() => que.Dequeue());
        }

        [Test]
        public void CopyTo_Array_CopiesItemsToArray()
        {
            var que = new Queue<string>();
            que.Enqueue("foo");

            var array = new string[1];
            que.CopyTo(array, 0);

            array.ShouldEqual(new []{"foo"});
        }

        [Test]
        public void SyncRoot_ShouldNotBeNull()
        {
            var que = new Queue<int>();
            que.SyncRoot.ShouldNotBeNull();
        }

        [Test]
        public void IsSynchronized_ShouldBeFalse()
        {
            var que = new Queue<int>();
            que.IsSynchronized.ShouldBeFalse();
        }
    }
}

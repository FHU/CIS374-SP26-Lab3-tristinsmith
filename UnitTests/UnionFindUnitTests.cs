using System;
using Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnionFindUnitTests
    {
        [TestMethod]
        public void TestUnion() 
        {
            string[] elements = {"a", "b", "c", "d"};

            var uf = new UnionFind<string>(elements);
            uf.Union("a","b");
            Assert.AreEqual(true, uf.Connected("a", "b"));
            Assert.AreEqual(false, uf.Connected("a", "c"));
            Assert.AreEqual(false, uf.Connected("a", "d"));
            uf.Union("b","c");

        }
       
        [TestMethod]
        public void TestAddExtractCountInt1()
        {
            MaxHeap<int> heap1 = new MaxHeap<int>();

            heap1.Add(4);
            heap1.Add(3);
            heap1.Add(2);
            heap1.Add(1);
            heap1.Add(0);
            Assert.AreEqual(5, heap1.Count);

            Assert.AreEqual(4, heap1.ExtractMax());
            Assert.AreEqual(4, heap1.Count);
            Assert.AreEqual(3, heap1.ExtractMax());
            Assert.AreEqual(3, heap1.Count);
            Assert.AreEqual(2, heap1.ExtractMax());
            Assert.AreEqual(2, heap1.Count);
            Assert.AreEqual(1, heap1.ExtractMax());
            Assert.AreEqual(1, heap1.Count);
            Assert.AreEqual(0, heap1.ExtractMax());
            Assert.AreEqual(0, heap1.Count);
            Assert.Throws<Exception>(() => heap1.ExtractMax());
            Assert.AreEqual(0, heap1.Count);
            Assert.Throws<Exception>(() => heap1.ExtractMax());
            Assert.AreEqual(0, heap1.Count);

        }
        

        [TestMethod]
        public void TestUpdateandRemoveEmpty()
        {
            MaxHeap<int> heap0 = new MaxHeap<int>();

            Assert.Throws<Exception>(() => heap0.Update(0, 10));
            Assert.Throws<Exception>(() => heap0.Remove(0));

        }
    }
}

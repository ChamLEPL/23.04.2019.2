using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PseudoEnumerable;
using PseudoLINQ;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [TestCase(new int[] { 5, -7, 8, -9 }, ExpectedResult = new int[] { -7, -9 })]
        [TestCase(new int[] { -5, -7, -8, -9 }, ExpectedResult = new int[] { -5, -7, -8, -9 })]
        [TestCase(new int[] { 5, 7, 8, 9 }, ExpectedResult = new int[] { })]
        public IEnumerable<int> Filter_ForInt_returnsFilteredSequence(int[] array)
        {
            return array.Filter(x => x < 0);
        }

        [TestCase(new string[] { "6", "555", "555" }, new string[] { "555", "555"})]
        [TestCase(new string[] { "555", "555", "555" }, new string[] { "555", "555","555"})]
        [TestCase(new string[] {"6"}, new string[] {})]
        public void Filter_ForString_returnsFilteredSequence(string[] array,string[] expected)
        {
            IEnumerable<string> sequence = array.Filter((string str) => str.Length == 3);
            CollectionAssert.AreEqual(sequence, expected);
        }

        [TestCase(new int[] { 5, 7, 8, 9 }, ExpectedResult = false)]
        [TestCase(new int[] { -5, -7, -8, -9 }, ExpectedResult = true)]
        [TestCase(new int[] { 5, 7, -8, 9 }, ExpectedResult = false)]
        public bool ForAll_returnsTrueOrFalse(int[] array)
        {
            return array.ForAll(x => x < 0);
        }

        [TestCase(new object[] { 5, 7, 8, 9 }, ExpectedResult = new int[] { 5, 7, 8, 9 })]
        [TestCase(new object[] { -5, -7, -8, -9 }, ExpectedResult = new int[] { -5, -7, -8, -9 })]
        [TestCase(new object[] { 5, 7, -8, 9 }, ExpectedResult = new int[] { 5, 7, -8, 9 })]
        public IEnumerable<int> CastTo_returnsTrueOrFalse(object[] array)
        {
            return Enumerable.CastTo<int>(array);
        }


        [Test]
        public void Filter_NullSource_ThrowArgumentNullException()
        {
            using (var iterator = Enumerable.Filter<int>(null, x => x > 0).GetEnumerator())
            {
                Assert.Throws<ArgumentNullException>(() => iterator.MoveNext());
            }
        }

        [TestCase(new object[] { 12, "hi" })]
        public void CastTo_HasIntAndStringValues_ThrowInvalidCastException(object[] source)
        {
            using (var iterator = Enumerable.CastTo<string>(source).GetEnumerator())
            {
                Assert.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        [TestCase(new int[] { 5, 7, 8, 9 }, ExpectedResult = new double[] {25, 49, 64, 81 })]
        [TestCase(new int[] { -5, -7, -8, -9 }, ExpectedResult = new double[] { 25, 49, 64, 81 })]
        [TestCase(new int[] { 5, 7, -8, 9 }, ExpectedResult = new double[] { 25, 49, 64, 81 })]
        public IEnumerable<double> Transform_returnsChangedSequence(int[] array)
        {
            return Enumerable.Transform<int, double>(array, x => Convert.ToDouble(x * x));
        }

        [TestCase(new int[] { 5, 7, 8, 9 }, ExpectedResult = new int[] { 5, 7, 8, 9 })]
        [TestCase(new int[] { -5, -17, -8, -9 }, ExpectedResult = new int[] { -5, -8, -9, -17 })]
        [TestCase(new int[] { 5, 7, -88, 9 }, ExpectedResult = new int[] { 5, 7, 9, -88 })]
        public IEnumerable<int> SortBy_SortByModule_returnsSortedSequence(int[] array)
        {
            return Enumerable.SortBy<int, int>(array, x => Math.Abs(x));
        }

        [TestCase(new int[] { 4, 7, 8, 9 }, ExpectedResult = new int[] { 4, 8, 7, 9 })]
        [TestCase(new int[] { -5, -17, -8, -9 }, ExpectedResult = new int[] { -8, -5, -17, -9 })]
        [TestCase(new int[] { 5, 7, -88, 9 }, ExpectedResult = new int[] { -88, 5, 7, 9 })]
        public IEnumerable<int> SortBy_SortByEven_returnsSortedSequenceWithComparer(int[] array)
        {
            return Enumerable.SortBy(array,  x => x % 2 == 0,
                new CompareByBoolFirstEven());
        }

        [TestCase(new int[] { 4, 7, 8, 9 }, ExpectedResult = new int[] { 4, 7, 8, 9 })]
        [TestCase(new int[] { -5, -17, -8, -9 }, ExpectedResult = new int[] { -17, -5, -8, -9 })]
        [TestCase(new int[] { 5, 7, -88, 9 }, ExpectedResult = new int[] { 5, 7, -88, 9 })]
        public IEnumerable<int> SortBy_SortByEvenAndOdd_returnsSortedSequence(int[] array)
        {
            return Enumerable.SortBy<int, int, string>(array, x => Math.Abs(x), 
                 x => (-x).ToString());
        }
    }
}
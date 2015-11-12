using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task1.BookListService.Models;
using static System.Math;

namespace Task.BinaryTree.NUnitTests {

    public struct Point2D {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point2D(int x, int y) {
            X = x;
            Y = y;
        }
    }

    public class DigitsComparer : IComparer<int> {
        public int Compare(int x, int y) => Abs(x).ToString().Length - Abs(y).ToString().Length;
    }


    [TestFixture]
    public class BinaryTreeTest {
        private IEnumerable<TestCaseData> IntegerTestDatas {
            get {
                yield return new TestCaseData(new[] { 44, 28, 85, 0, -5, 95, 32, 21, 48, 28, 34, 5, 8 }, null, -5, 95);
                yield return new TestCaseData(new[] { 44, 28, 85, 10, -5, 95, 32, 121, 48, 28, 34, 65, 78 }, new DigitsComparer(), -5, 121);
                yield return new TestCaseData(new[] { 44, 28, 85, 10, -15, 95, 32, 121, 48, 28, 34, 5, 78 }, new DigitsComparer(), 5, 121);
            }
        }

        [TestCaseSource(nameof(IntegerTestDatas))]
        public void Integer_MinMaxTest_Test(int[] array, IComparer<int> comparer,  int minValue, int maxValue) {
            BinaryTree<int> tree = comparer == null ? new BinaryTree<int>(array) : new BinaryTree<int>(array, comparer);
            Assert.AreEqual(tree.MinValue, minValue);
        }

        public void String_Test(string[] array, IComparer<string> comparer) {
            BinaryTree<string> tree = comparer == null ? new BinaryTree<string>(array) : new BinaryTree<string>(array, comparer);
        }

        public void Book_Test(Book[] array, IComparer<Book> comparer ) {
            BinaryTree<Book> tree = comparer == null ? new BinaryTree<Book>(array) : new BinaryTree<Book>(array, comparer);
        }

        public void Point2D_Test(Point2D[] array, IComparer<Point2D> comparer) {
            BinaryTree<Point2D> tree = comparer == null ? new BinaryTree<Point2D>(array) : new BinaryTree<Point2D>(array, comparer);
        }
    }
}

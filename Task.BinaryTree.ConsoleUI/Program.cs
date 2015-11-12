using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;

namespace Task.BinaryTree.ConsoleUI {

    public class DigitsComparer : IComparer<int> {
        public int Compare(int x, int y) => Abs(x).ToString().Length - Abs(y).ToString().Length;
    }

    public class StringComparer : IComparer<string> {
        public int Compare(string x, string y) => x.Length - y.Length;
    }

    public class Point2DComparer : IComparer<Point2D> {
        public int Compare(Point2D x, Point2D y) => (int) (x.Distance - y.Distance);
    }

    public class BookComparer : IComparer<Book> {
        public int Compare(Book x, Book y) => (int)(x.Price - y.Price);
    }

    public struct Point2D {
        public int X { get; }
        public int Y { get; }

        public double Distance => Pow((Pow(X,2) + Pow(Y,2)), 1.0 / 2);

        public Point2D(int x, int y) {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X},{Y})";

    }

    class Program {

        private static void OrderTree<T>(BinaryTree<T> tree) {
            WriteLine("Inorder: " + string.Join(" ", tree.Inorder()));
            WriteLine("Preorder: " + string.Join(" ", tree.Preorder()));
            WriteLine("Postorder: " + string.Join(" ", tree.Postorder()));
        }

        private static void RemoveElements<T>(BinaryTree<T> tree, T[] items) {
            WriteLine("Test remove");
            foreach(var item in items) {
                tree.Remove(item);
                WriteLine($"Tree after remove {item}");
                WriteLine(tree.ToString());
            }
        }

        private static void MinMaxValue<T>(BinaryTree<T> tree) {
            WriteLine($"Min value: {tree.MinValue}");
            WriteLine($"Max value: {tree.MaxValue}");
        }

        private static void TestTree<T>(BinaryTree<T> tree, T[] removeItems) {
            WriteLine(tree.ToString());

            OrderTree(tree);
            MinMaxValue(tree);
            RemoveElements(tree, removeItems);
            WriteLine("Clear tree");
            tree.Clear();
            WriteLine(tree.ToString());
        }

        private static void TestTree<T>(T[] array, T[] removeItems, IComparer<T> comparer) {
            WriteLine($"\tTest {typeof(T)} Binary tree with default IComparer");
            WriteLine("===============================================================================");
            BinaryTree<T> tree;
            try {
                tree = new BinaryTree<T>(array);
                TestTree(tree, removeItems);
            } catch(ArgumentException ex) {
                WriteLine($"{ex.Message}\n");
            }

            WriteLine($"\tTest {typeof(T)} Binary tree with custom IComparer");
            WriteLine("===============================================================================");
            tree = new BinaryTree<T>(array, comparer);
            TestTree(tree, removeItems);
        }

        static void Main(string[] args) {
            
            TestTree(new[] { 44, 28, 85, 0, -5, 195, 32, 21, 48, 28, 34, 5, 8 }, 
                     new[] { 0, -105, 44, 8 }, 
                     new DigitsComparer());
            WriteLine("Press any button for continue to next tree");
            ReadKey();
            Clear();

            TestTree(new[] {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "." }, 
                     new [] {"twelve", "four", "one", "six"}, 
                     new StringComparer());
            WriteLine("Press any button for continue to next tree");
            ReadKey();
            Clear();

            TestTree(new[] { new Point2D(0 ,5), new Point2D(5, 0), new Point2D(10, -5), new Point2D(3, 2), new Point2D(2, 3), new Point2D(1, 0) },
                             new[] { new Point2D(5, 0), new Point2D(2, 3), new Point2D(4, 5) }, 
                             new Point2DComparer());
            WriteLine("Press any button for continue to next tree");
            ReadKey();
            Clear();

            TestTree(new[] { new Book("", "", 128, 32000), new Book("", "", 128, 46000), new Book("", "", 256, 32000), new Book("", "", 64, 12800), new Book("", "", 55, 10500) }, 
                             new [] { new Book("", "", 64, 32000), new Book("", "", 55, 32000), new Book("", "", 128, 105000) }, 
                             new BookComparer());

            ReadKey();
        }
    }
}

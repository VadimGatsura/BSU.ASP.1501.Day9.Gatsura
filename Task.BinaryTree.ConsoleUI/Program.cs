using static System.Console;

namespace Task.BinaryTree.ConsoleUI {
    class Program {
        static void Main(string[] args) {
            BinaryTree<int> tree = new BinaryTree<int>(new[] {44, 28, 85, 0, -5, 95, 32, 21, 48, 28, 34, 5, 8});
            WriteLine(tree.ToString());

            Write("Inorder: ");
            foreach(var i in tree.Inorder()) {
                Write($"{i} ");
            }
            WriteLine();

            Write("Preorder: ");
            foreach (var i in tree.Preorder()) {
                Write($"{i} ");
            }
            WriteLine();

            Write("Postorder: ");
            foreach (var i in tree.Postorder()) {
                Write($"{i} ");
            }
            WriteLine();
            /* tree = tree.Balance();
             WriteLine(tree.ToString());*/

            ReadKey();
        }
    }
}

using static System.Console;

namespace Task.BinaryTree.ConsoleUI {
    class Program {
        static void Main(string[] args) {
            BinaryTree<int> tree = new BinaryTree<int>(new[] {44, 28, 85, 0, -5, 95, 32, 21, 48, 28, 34, 5, 8});
            WriteLine(tree.ToString());

            WriteLine("Inorder: " + string.Join(" ", tree.Inorder()));
            WriteLine("Preorder: " + string.Join(" ", tree.Preorder()));
            WriteLine("Postorder: " + string.Join(" ", tree.Postorder()));
            /* tree = tree.Balance();
             WriteLine(tree.ToString());*/

            ReadKey();
        }
    }
}

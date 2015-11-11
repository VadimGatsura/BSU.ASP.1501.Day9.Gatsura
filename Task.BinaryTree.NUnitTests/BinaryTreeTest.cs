using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task.BinaryTree.NUnitTests {
    [TestFixture]
    public class BinaryTreeTest {
        private IEnumerable<TestCaseData> IntegerTestDatas {
            get {
                yield return new TestCaseData();
            }
        }

        [TestCaseSource(nameof(IntegerTestDatas))]
        public void Integer_Test(int[] array, int minValue, int maxvalue) {
            BinaryTree<int> tree = new BinaryTree<int>(array);
        }
    }
}

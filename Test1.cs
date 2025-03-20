using KnapsackProblemFinal;

namespace KnapsackTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Setting up the problem so that the knapsack's capacity (20) is greater than the highest possible weight of an item (10)
            Problem Problem = new Problem(11, 0);
            int capacity = 20;

            // Solving the problem
            Result Result = Problem.Solve(capacity);

            // Checking if the result contains at least 1 item
            Assert.IsTrue(Result.SelectedItems.Count > 0, "Should contain at least 1 item! ");
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Setting up the problem so that the knapsack's capacity (0) is lesser than the lowest possible weight of an item (1)
            Problem Problem = new Problem(11, 0);
            int capacity = 0;

            // Solving the problem
            Result Result = Problem.Solve(capacity);

            // Checking if the result contains an empty list
            Assert.IsTrue(Result.SelectedItems.Count == 0, "Should be empty! ");
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Creating 5 specific items
            Item Item0 = new Item(1, 1, 0);     // ratio = 1
            Item Item1 = new Item(13, 21, 1);   // ratio = 0.619
            Item Item2 = new Item(10, 8, 2);    // ratio = 1.25
            Item Item3 = new Item(34, 55, 3);   // ratio = 0.618
            Item Item4 = new Item(2, 3 , 4);    // ratio = 0.67

            // Adding these items to a list
            List<Item> testItems = new List<Item> { Item0, Item1, Item2, Item3, Item4 };

            // Setting up the problem
            Problem Problem = new Problem(5, 0);
            int capacity = 20;

            // Overwriting the list of items with the test items
            Problem.listOfItems = testItems;

            // Solving the problem
            Result Result = Problem.Solve(capacity);

            // MANUAL SOLUTION:
            // Sorted list of items by ratio: Item2, Item0, Item4, Item1, Item3
            // 
            // -Adding Item2 (total value of knapsack = 10, total weight of knapsack = 8)
            // -Adding Item0 (total value of knapsack = 11, total weight of knapsack = 9)
            // -Adding Item4 (total value of knapsack = 13, total weight of knapsack = 12)
            // -Cannot add Item1 (weight of knapsack would be 33, which is greater than its capacity)
            // -Cannot add Item3 (weight of knapsack would be 67, which is greater than its capacity)
            // 
            // Final result should return a list containing 3 items (items of indexes: 2, 0, 4), with total value of 13, and total weight of 12

            Assert.IsTrue(Result.SelectedItems.Count == 3, "Should contain 3 items! ");
            Assert.AreEqual(Result.SelectedItems[0].originalIndex, 2, "The first item should be Item2");
            Assert.AreEqual(Result.SelectedItems[1].originalIndex, 0, "The second item should be Item0");
            Assert.AreEqual(Result.SelectedItems[2].originalIndex, 4, "The third item should be Item4");
            Assert.AreEqual(Result.TotalValue, 13, "The knapsack's total value should be 13");
            Assert.AreEqual(Result.TotalWeight, 12, "The knapsack's total weight should be 12");
        }

        [TestMethod]
        public void TestMethod4()
        {
            // Setting up the problem 
            Problem Problem = new Problem(11, 0);

            // Checking if all generated Items have their value and weight between 1 and 10
            for (int i = 0; i < Problem.listOfItems.Count; i++)
            {
                Assert.IsTrue(Problem.listOfItems[i].value >= 1 && Problem.listOfItems[i].value <= 10, "Value should be between 1 and 10! ");
                Assert.IsTrue(Problem.listOfItems[i].weight >= 1 && Problem.listOfItems[i].weight <= 10, "Weight should be between 1 and 10! ");
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            // Setting up the problem 
            Problem Problem = new Problem(11, 0);

            // Sorting the list of items by ratio
            Problem.Sort();

            // Checking if the list of items is sorted by ratio in descending order
            for (int i = 0; i < Problem.listOfItems.Count - 1; i++)
            {
                Assert.IsTrue(Problem.listOfItems[i].ratio >= Problem.listOfItems[i + 1].ratio, "Should be sorted by ratio in descending order! ");
            }
        }
    }
}

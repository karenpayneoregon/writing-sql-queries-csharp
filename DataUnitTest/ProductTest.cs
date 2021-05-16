using DataOperations;
using DataUnitTest.BaseClasses;
using Microsoft.VisualStudio
    .TestTools.UnitTesting;

namespace DataUnitTest
{
    [TestClass]
    public class ProductTest : Testbase
    {
        // product primary key to test
        private int _productIdentifier = 2;

        // in stock quantity to begin with
        private int _ResetProductQuantityToo = 20;

        /// <summary>
        /// Run before each test runs
        /// Reset in stock quantity to fixed amount
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            if (OrderTestNameList.Contains(TestContext.TestName))
            {
                ResetProduct(_productIdentifier, _ResetProductQuantityToo);
            }
        }
        /// <summary>
        /// Run after each test has completed.
        /// Reset in stock quantity to fixed amount
        /// </summary>
        [TestCleanup]
        public void MethodCleanUp()
        {
            if (OrderTestNameList.Contains(TestContext.TestName))
            {
                ResetProduct(_productIdentifier, _ResetProductQuantityToo);
            }
        }
        /// <summary>
        /// Test to ensure a product can be ordered when there is
        /// sufficient stock.
        /// </summary>
        [TestMethod]
        public void OrderProduct_Stocked()
        {
            // arrange
            var productIdentifier = _productIdentifier;
            var orderAmount = 2;

            // act
            var ops = new Backend();
            var results = ops.PlaceOrder(productIdentifier, orderAmount);

            // assert
            Assert.IsTrue(results == DataOperationStatus.UpdateSuccessfully,
                "Expected update successful");
        }
        /// <summary>
        /// Test the production code handles Insufficient quantity
        /// </summary>
        [TestMethod]
        public void OrderProduct_UnderStock()
        {
            // arrange
            var productIdentifier = _productIdentifier;
            var orderAmount = _ResetProductQuantityToo + 2;

            // act
            var ops = new Backend();
            var results = ops.PlaceOrder(productIdentifier, orderAmount);

            // assert
            Assert.IsTrue(results == DataOperationStatus.InsufficientQuantity,
                "Expected update to fail");
        }
        /// <summary>
        /// Test that non existing products are handled
        /// </summary>
        /// <remarks>
        /// Within the application products should be selected from
        /// a valid list yet there may be cases that this does not happen
        /// </remarks>
        [TestMethod]
        public void ProductDoesNotExists() 
        {
            // arrange
            var productIdentifier = -1;
            var orderAmount = _ResetProductQuantityToo + 2;

            // act
            var ops = new Backend();
            var results = ops.PlaceOrder(productIdentifier, orderAmount);

            // assert
            Assert.IsTrue(results == DataOperationStatus.NotFound,
                "Expected product to not exists");
        }
    }
}

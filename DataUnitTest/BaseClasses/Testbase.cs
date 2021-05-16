using System.Collections.Generic;
using System.Data.SqlClient;
using DataConnections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataUnitTest.BaseClasses
{
    public class Testbase : BaseSqlServerConnection
    {
        /// <summary>
        /// Any test method with one of these names will
        /// reset product specified in the Init method
        /// of the unit test to a specific in stock amount.
        /// </summary>
        /// <remarks>
        /// Fragile in respects if a test method name changes.
        /// </remarks>
        protected List<string> OrderTestNameList = new List<string>() 
        {
            "OrderProduct_Stocked",
            "OrderProduct_UnderStock",
            "ProductDoesNotExists"
        };

        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set => TestContextInstance = value;
        }
        public void ResetProduct(int pProductIdentifier, int pQuantity)
        {
            using (SqlConnection cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (SqlCommand cmd = new SqlCommand() {Connection = cn})
                {

                    cmd.CommandText = "UPDATE dbo.Products " + 
                                      "SET UnitsInStock = @UpdateInStock " + 
                                      "WHERE ProductID = @Id";

                    cmd.Parameters.AddWithValue("@Id", pProductIdentifier);
                    cmd.Parameters.AddWithValue("@UpdateInStock", pQuantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}

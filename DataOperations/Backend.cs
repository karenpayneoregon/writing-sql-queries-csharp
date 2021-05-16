using System;
using System.Data.SqlClient;
using System.Diagnostics;
using DataConnections;

namespace DataOperations
{
    public class Backend : BaseSqlServerConnection
    {
        public Backend()
        {
            mHasException = false;
        }
        public DataOperationStatus PlaceOrder(int pProductIdentifier, int pOrderAmount)
        {
            using (var cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand() {Connection = cn})
                {

                    try
                    {
                        cn.Open();

                        cmd.Parameters.AddWithValue("@Id", pProductIdentifier);

                        // determine if product currently exists
                        cmd.CommandText = "SELECT COUNT(ProductID) " +
                                          "FROM dbo.Products " +
                                          "WHERE ProductID = @Id";

                        var locateResult = Convert.ToInt32(cmd.ExecuteScalar());

                        if (locateResult == 0)
                        {
                            return DataOperationStatus.NotFound;
                        }


                        cmd.CommandText = "SELECT UnitsInStock " +
                                          "FROM dbo.Products " +
                                          "WHERE ProductID = @Id";

                        var inStock = Convert.ToInt16(cmd.ExecuteScalar());

                        if (inStock > pOrderAmount)
                        {
                            cmd.CommandText = "UPDATE dbo.Products " +
                                              "SET UnitsInStock = @UpdateInStock " +
                                              "WHERE ProductID = @Id";

                            int updateStock = inStock - pOrderAmount;
                            cmd.Parameters.AddWithValue("@UpdateInStock", updateStock);
                            Debug.WriteLine(cmd.ExecuteNonQuery());
                            return DataOperationStatus.UpdateSuccessfully;
                        }
                        else
                        {
                            return DataOperationStatus.InsufficientQuantity;
                        }
                    }
                    catch (Exception ex)
                    {
                        mHasException = true;
                        mLastException = ex;
                        return DataOperationStatus.RuntimeExceptionThrown;
                    }
                }
            }
        }
    }
}

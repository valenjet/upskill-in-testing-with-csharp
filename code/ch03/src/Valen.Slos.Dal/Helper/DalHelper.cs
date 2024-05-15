namespace Valen.Slos.Dal.Helper;

using System.Collections.Generic;
using System.Data;

internal class DalHelper
{
    public const string CreateStoredProcFormat = "[dbo].[dal_{0}_Create]";
    public const string RetrieveStoredProcFormat = "[dbo].[dal_{0}_Retrieve]";
    public const string UpdateStoredProcFormat = "[dbo].[dal_{0}_Update]";
    public const string DeleteStoredProcFormat = "[dbo].[dal_{0}_Delete]";

    private readonly string _connectionString;

    public DalHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataSet ExecuteStoredProcedure(
        string procedureName, 
        IEnumerable<SqlParameter> parameters)
    {
        var exceptionMessage = string.Format(
            "ExecuteStoredProcedure is not implemented for '{0}'.",
            procedureName);

        throw new NotImplementedException(exceptionMessage);
    }

    public int ExecuteStoredProcedureNonQuery(
        string procedureName,
        IEnumerable<SqlParameter> parameters)
    {
        var exceptionMessage = string.Format(
            "ExecuteStoredProcedureNonQuery is not implemented for '{0}'.",
            procedureName);

        throw new NotImplementedException(exceptionMessage);
    }
}

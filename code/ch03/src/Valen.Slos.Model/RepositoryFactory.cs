namespace Valen.Slos.Model;

using Valen.Slos.Dal;
using Valen.Slos.Dao;

// Caution: This factory implementation is intended only as an example.
public static class RepositoryFactory
{
    public static string? ConnectionString { private get; set; }

    internal static IRepository<IndividualEntity> IndividualRepo
    {
        get
        {
            if (ConnectionString == null) throw new InvalidOperationException("ConnectionString is null");
            return new IndividualDal(ConnectionString); 
        }
    }

    internal static IRepository<StudentEntity> StudentRepo
    {
        get
        {
            if (ConnectionString == null) throw new InvalidOperationException("ConnectionString is null");
            return new StudentDal(ConnectionString); 
        }
    }

    internal static IRepository<ApplicationEntity> ApplicationRepo
    {
        get
        {
            if (ConnectionString == null) throw new InvalidOperationException("ConnectionString is null");
            return new ApplicationDal(ConnectionString); 
        }
    }
}
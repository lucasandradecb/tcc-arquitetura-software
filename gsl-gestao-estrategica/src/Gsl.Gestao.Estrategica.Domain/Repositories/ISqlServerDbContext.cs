using System.Data;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Conexão com SqlServer
    /// </summary>
    public interface ISqlServerDbContext
    {
        /// <summary>
        /// GetConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CoordinatesMapWF.Domain.Models;

namespace CoordinatesMapWF.Infrastructure.Abstraction.Interfaces
{
    /// <summary>
    /// Data base connection abstraction.
    /// </summary>
    public interface IDataBaseConnection
    {
        /// <summary>
        /// Get connection.
        /// </summary>
        /// <returns>Data base connection.</returns>
        SqlConnection GetConnection();

        /// <summary>
        /// Update coordinate command. 
        /// </summary>
        /// <param name="coordinate">New coordinate data.</param>
        void UpdateMapMarker(Coordinate coordinate);

        /// <summary>
        /// Get map markers query.
        /// </summary>
        void GetMapMarkers(List<Coordinate> coordinates);
    }
}

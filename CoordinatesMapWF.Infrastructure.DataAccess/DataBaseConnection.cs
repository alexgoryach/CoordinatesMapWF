using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoordinatesMapWF.Domain.Models;
using CoordinatesMapWF.Infrastructure.Abstraction.Interfaces;

namespace CoordinatesMapWF.Infrastructure.DataAccess
{
    /// <summary>
    /// Data base connection class.
    /// </summary>
    public class DataBaseConnection : IDataBaseConnection
    {
        private const string connectionString = @"Data Source=(local); Initial Catalog=mapmarkersdb; Integrated Security=True";

        /// <summary>
        /// Get connection.
        /// </summary>
        /// <returns>Data base connection.</returns>
        public SqlConnection GetConnection()
        {
            var sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Error while open connection to DB.");
                throw;
            }

            return sqlConnection;
        }

        /// <summary>
        /// Update coordinate command. 
        /// </summary>
        /// <param name="coordinate">New coordinate data.</param>
        public void UpdateMapMarker(Coordinate coordinate)
        {
            const string updateCommand = "UPDATE Coordinates SET Latitude = @NewLatitude, Longitude = @NewLongitude WHERE Id =@CoordinateId";
            var connection = GetConnection();
            var sqlCommand = new SqlCommand(updateCommand, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@NewLatitude", SqlDbType.Float).Value = coordinate.Latitude;
            sqlCommand.Parameters.Add("@NewLongitude", SqlDbType.Float).Value = coordinate.Longitude;
            sqlCommand.Parameters.Add("@CoordinateId", SqlDbType.Int).Value = coordinate.Id;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            connection.Close();
        }

        /// <summary>
        /// Get map markers query.
        /// </summary>
        public void GetMapMarkers(List<Coordinate> coordinates)
        {
            const string getDataQuery = "SELECT * FROM Coordinates";
            using (var connection = GetConnection())
            {
                var sqlQuery = new SqlCommand(getDataQuery, connection);
                using (var reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var point = new Coordinate();
                        var pointProperties = point.GetType().GetProperties();
                        var data = new object[pointProperties.Length];
                        reader.GetValues(data);
                        var index = 0;
                        point.Id = (int) data[index++];
                        point.Name = (string)data[index++];
                        point.Latitude = (double)data[index++];
                        point.Longitude = (double)data[index];
                        coordinates.Add(point);
                    }
                }
                connection.Close();
            }
        }
    }
}

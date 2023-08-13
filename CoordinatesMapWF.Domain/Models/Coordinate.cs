using System;

namespace CoordinatesMapWF.Domain.Models
{
    /// <summary>
    /// Coordinate entity.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Point identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Point name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Point latitude.
        /// </summary>
        public double Latitude { get; set; }
        
        /// <summary>
        /// Point longitude.
        /// </summary>
        public double Longitude { get; set; }
    }
}

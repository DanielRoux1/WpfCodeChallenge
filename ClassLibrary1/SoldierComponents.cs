using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SoldierComponent.Models
{
    /// <summary>
    ///   Soldier Class that is used as primary list object in main xaml viewer application.
    /// </summary>
    public class Soldier
    {
        /// <summary>Gets or sets the soldier identifier.</summary>
        /// <value>The soldier identifier.</value>
        public int SoldierId { get; set; }
        /// <summary>Gets or sets the name of the soldier.</summary>
        /// <value>The name of the soldier.</value>
        public string SoldierName { get; set; }
        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        public decimal Latitude { get; set; }
        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        public decimal Longitude { get; set; }
    }

    /// <summary>
    ///   <br />
    /// </summary>
    public class SoldierDb : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="SoldierDb" /> class.</summary>
        public SoldierDb()
        {
            // no Implementation needed as this is a factory method.
        }

        // public Solder Db Set to be implemented for the entity Framework
        /// <summary>Gets or sets the soldiers.</summary>
        /// <value>The soldiers.</value>
        public DbSet<Soldier> Soldiers { get; set; }
 
    }
}

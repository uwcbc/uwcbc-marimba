using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marimba
{
    class User
    {
        /// <summary>
        /// Number of fields to
        /// </summary>
        public static readonly int numUserFields = 4;

        /// <summary>
        /// The login name for this user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The salted and hashed password
        /// </summary>
        public string saltAndPassword { get; set; }

        /// <summary>
        /// Priviledge level for this user
        /// </summary>
        public string priviledge { get; set; }

        /// <summary>
        /// Key XOR'd with the single hash of the user's password
        /// </summary>
        public string keyXORPassword { get; set; }
    }
}

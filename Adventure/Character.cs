using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal abstract class Character
    {
        // --- fields ---------------------------------------------------------

        protected readonly string mName;
        protected int mPower;

        // --- properties -----------------------------------------------------

        public string Name => mName;
        public int Power { get =>  mPower; set => mPower = value; }

        // --- constructors ---------------------------------------------------

        public Character(string pName)
        {
            mName = pName;
        }
    }
}
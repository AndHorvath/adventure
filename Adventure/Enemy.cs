using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Enemy : Character
    {
        // --- constructors ---------------------------------------------------
        
        public Enemy(string pName) : base(pName)
        {
            Initialize();
        }

        // --- private methods ------------------------------------------------

        private void Initialize()
        {
            Random random = new();
            mPower = random.Next(10, 31);
        }
    }
}
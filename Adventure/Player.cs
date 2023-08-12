using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Player : Character
    {
        // --- fields ---------------------------------------------------------

        private int mScore;

        // --- properties -----------------------------------------------------

        public int Score { get => mScore; set => mScore = value; }
        
        // --- constructors ---------------------------------------------------

        public Player(string pName) : base(pName)
        {
            Initialize();
        }

        // --- private methods ------------------------------------------------

        private void Initialize()
        {
            mPower = 100;
            mScore = 0;
        }
    }
}
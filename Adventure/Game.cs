using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal abstract class Game : IPlayable
    {
        // --- fields ---------------------------------------------------------

        protected readonly Player mPlayer;
        protected readonly List<Enemy> mEnemies;

        // --- properties -----------------------------------------------------

        public Player Player => mPlayer;
        public List<Enemy> Enemies => mEnemies;

        // --- constructors ---------------------------------------------------
        
        public Game(Player pPlayer)
        {
            mPlayer = pPlayer;
            mEnemies = new List<Enemy>();
        }

        // --- public methods -------------------------------------------------

        public abstract void Start();
        public abstract void Play();
        public abstract void Stop();

        // --- protected methods ----------------------------------------------

        protected virtual void InitializeEnemies(string[] pEnemyNames)
        {
            foreach (string enemyName in pEnemyNames)
            {
                mEnemies.Add(new Enemy(enemyName));
            }
        }
    }
}
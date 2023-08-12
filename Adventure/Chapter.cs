using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Chapter
    {
        // --- fields ---------------------------------------------------------

        private readonly int mId;
        private readonly string mTitle;
        private readonly List<int> mNextChapterIds;
        private readonly List<Enemy> mEnemies;

        private bool mIsReached;

        // --- properties -----------------------------------------------------

        public int Id => mId;
        public string Title => mTitle;
        public List<int> NextChapterIds => mNextChapterIds;
        public List<Enemy> Enemies => mEnemies;

        public bool IsReached { get => mIsReached; set => mIsReached = value; }

        // --- constructors ---------------------------------------------------

        public Chapter(int pId, string pTitle, List<int> pNextChapterIds)
        {
            mId = pId;
            mTitle = pTitle;
            mNextChapterIds = pNextChapterIds;
            mEnemies = new List<Enemy>();
            mIsReached = false;
        }
    }
}
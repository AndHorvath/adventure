using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Adventure : Game
    {
        // --- fields ---------------------------------------------------------

        private readonly List<Chapter> mChapters;

        // --- properties -----------------------------------------------------

        public List<Chapter> Chapters => mChapters;

        // --- constructors ---------------------------------------------------

        public Adventure(Player pPlayer) : base(pPlayer)
        {
            mChapters = new List<Chapter>();
            Initialize();
        }

        // --- public methods -------------------------------------------------

        public override void Start()
        {
            WriteGameStartToConsole();
        }

        public override void Play()
        {
            SetUpGame(out Chapter actualChapter, out bool isContinue);

            do
            {
                if (IsNextChaptersOrEnemies(actualChapter) && IsPlayerPower())
                {
                    WriteChapterToConsole(actualChapter);
                    WritePlayerToConsole(actualChapter);

                    while (IsEnemies(actualChapter) && IsPlayerPower())
                    {
                        WriteEnemiesToConsole(actualChapter);
                        Fight(actualChapter);
                        WritePlayerToConsole(actualChapter);
                    }
                    if (IsNextChapters(actualChapter) && IsPlayerPower())
                    {
                        WriteNextChaptersToConsole(actualChapter);
                        StepToNextChapter(ref actualChapter);
                    }
                }
                else if (IsNextChaptersOrEnemies(actualChapter))
                {
                    WriteDefeatToConsole(ref isContinue);
                }
                else
                {
                    WriteVictoryToConsole(ref isContinue);
                }
            } while (isContinue);
        }

        public override void Stop()
        {
            WriteGameOverToConsole();
        }

        // --- private methods ------------------------------------------------

        private void Initialize()
        {
            string[] suffices = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            string[] enemyNames = CreateEnemyNames(suffices);

            InitializeChapters(suffices);
            InitializeEnemies(enemyNames);

            AddEnemiesToChapters();
            AddLastChapterIdToChapters();
        }

        private string[] CreateEnemyNames(string[] pSuffices)
        {
            string[] enemyNames = new string[pSuffices.Length];

            for (int i = 0; i < pSuffices.Length; i++)
            {
                enemyNames[i] = "Enemy" + pSuffices[i];
            }
            return enemyNames;
        }

        private void InitializeChapters(string[] pSuffices)
        {
            List<int> nextChapterIds = new();

            for (int i = 0; i < pSuffices.Length - 1; i++)
            {
                nextChapterIds.Add(i);
            }
            for (int i = 0; i < pSuffices.Length; i++)
            {
                mChapters.Add(new Chapter(i, "Chapter" + pSuffices[i], new List<int>(nextChapterIds)));
                mChapters[^1].IsReached = false;
            }
            for (int i = 0; i < mChapters.Count; i++)
            {
                if (i != mChapters.Count - 1)
                {
                    mChapters[i].NextChapterIds.RemoveAt(i);
                }
                else
                {
                    mChapters[i].NextChapterIds.Clear();
                }
            }
        }

        private void AddEnemiesToChapters()
        {
            Random random = new();
            mEnemies.ForEach(enemy => mChapters[random.Next(1, mChapters.Count)].Enemies.Add(enemy));
        }

        private void AddLastChapterIdToChapters()
        {
            Random random = new();
            mChapters[random.Next(1, mChapters.Count - 1)].NextChapterIds.Add(mChapters.Count - 1);
        }



        private void WriteGameStartToConsole()
        {
            Console.WriteLine("\nYour adventure starts.");
            Console.WriteLine(
                $"\nYou have to reach {mChapters[^1].Title}." +
                $"\nBefore moving on to a new chapter, you have to defeat the enemies, if any, in current chapter." +
                $"\nThey are, equal in number to that of chapters, placed randomly, as well as the exit to {mChapters[^1].Title}." +
                $"\nFor entering a new chapter you score 10 points." +
                $"\nFor defeating an enemy you score 100 points." +
                $"\nIf you have run out of power, you have lost.");
        }

        private void WritePlayerToConsole(Chapter pChapter)
        {
            Console.WriteLine($"\n{mPlayer.Name} | score: {mPlayer.Score} | power: {Math.Max(mPlayer.Power, 0)}");
        }

        private void WriteChapterToConsole(Chapter pChapter)
        {
            Console.WriteLine($"\n{pChapter.Title}");
        }

        private void WriteNextChaptersToConsole(Chapter pChapter)
        {
            Console.WriteLine("\nNext chapters:");
            pChapter.NextChapterIds.ForEach(id => Console.WriteLine($"{id + 1} - {mChapters[id].Title}"));
        }

        private void WriteEnemiesToConsole(Chapter pChapter)
        {
            Console.WriteLine("\nEnemies:");
            pChapter.Enemies.ForEach(enemy => Console.WriteLine($"{enemy.Name} | power: {enemy.Power}"));
        }

        private void WriteDefeatToConsole(ref bool pIsContinue)
        {
            Console.WriteLine("\nYou lost.");
            pIsContinue = false;
        }

        private void WriteVictoryToConsole(ref bool pIsContinue)
        {
            Console.WriteLine("\nYou won.");
            pIsContinue = false;
        }

        private void WriteGameOverToConsole()
        {
            if (mChapters[^1].IsReached && mPlayer.Power > 0)
            {
                Console.WriteLine($"You reached {mChapters[^1].Title} and scored {mPlayer.Score} points.");
            }
            Console.WriteLine("\nYour adventure has ended.");
        }



        private bool IsNextChapters(Chapter pChapter)
        {
            return pChapter.NextChapterIds.Count > 0;
        }

        private bool IsNextChaptersOrEnemies(Chapter pChapter)
        {
            return IsNextChapters(pChapter) || IsEnemies(pChapter);
        }

        private bool IsPlayerPower()
        {
            return mPlayer.Power > 0;
        }

        private bool IsEnemies(Chapter pChapter)
        {
            return pChapter.Enemies.Count > 0;
        }



        private void SetUpGame(out Chapter pChapter, out bool pIsContinue)
        {
            pChapter = mChapters[0];
            pChapter.IsReached = true;
            pIsContinue = true;
        }
        
        private void Fight(Chapter pChapter)
        {
            do
            {
                Console.WriteLine("\nFor fight, press Enter.");
            } while (Console.ReadKey().Key != ConsoleKey.Enter);

            Random random = new();
            int playerFightPower = random.Next(31);
            int enemyFightPower = random.Next(31);
            int fightPowerDifference = playerFightPower - enemyFightPower;

            if (fightPowerDifference > 0)
            {
                pChapter.Enemies[0].Power -= fightPowerDifference;
                Console.WriteLine(
                    $"You won the fight by power {playerFightPower} to {enemyFightPower}. " +
                    $"{pChapter.Enemies[0].Name} lost {fightPowerDifference} power unit" +
                    (fightPowerDifference != 1 ? "s" : string.Empty) + ".");
            }
            else if (fightPowerDifference < 0)
            {
                mPlayer.Power += fightPowerDifference;
                Console.WriteLine(
                    $"You lost the fight by power {playerFightPower} to {enemyFightPower} " +
                    $"and lost {fightPowerDifference * (-1)} power unit" +
                    (fightPowerDifference != -1 ? "s" : string.Empty) + ".");
            }
            else
            {
                Console.WriteLine($"\nFight ended in a draw by power {playerFightPower} to {enemyFightPower}.");
            }
            if (0 < pChapter.Enemies.RemoveAll(enemy => enemy.Power <= 0))
            {
                mPlayer.Score += 100;
            }
        }

        private void StepToNextChapter(ref Chapter pActualChapter)
        {
            int nextChapterEntry;
            int nextChapterId;

            do
            {
                do
                {
                    Console.WriteLine("\nChoose next chapter, please.");
                } while (!int.TryParse(Console.ReadLine(), out nextChapterEntry));

                nextChapterId = nextChapterEntry - 1;
            } while (!pActualChapter.NextChapterIds.Contains(nextChapterId));

            pActualChapter = mChapters[nextChapterId];

            if (!pActualChapter.IsReached)
            {
                mPlayer.Score += 10;
                pActualChapter.IsReached = true;
            }
        }
    }
}
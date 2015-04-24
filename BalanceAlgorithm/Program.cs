using System;

namespace BalanceAlgorithm
{
    class MainClass
    {
        static MatchPlayer[] playerPool;
        private static int CalculateTeamScore(int team)
        {
            Console.WriteLine ("Team " + team + ": ");
            var score = 0;
            for (var i = 0; i < playerPool.Length; ++i)
            {
                if ((team & 1) == 1)
                {
                    Console.WriteLine (playerPool [i].Name + ": " + playerPool [i].Rating);
                    score += (int)playerPool[i].Rating;
                }
                team >>= 1;
            }
            return score;
        } 

        private static bool IsValidTeam(int team)
        {
            // determine how many bits are set, and return true if the result is 5
            // This is the slow way, but it works.
            var count = 0;
            for (var i = 0; i < playerPool.Length; ++i)
            {
                if ((team & 1) == 1)
                {
                    ++count;
                }
                team >>= 1;
            }
            return (count == 5);
        }
        
        public static void Main (string[] args)
        {
            playerPool = new MatchPlayer[]
            {
                new MatchPlayer(){Name = "Dendi", Rating = 1202},
                new MatchPlayer(){Name = "ALWAYSWANNAFLY", Rating = 1174},
                new MatchPlayer(){Name = "Blan", Rating=1160},
                new MatchPlayer(){Name = "Sachlo", Rating=1245},
                new MatchPlayer(){Name = "ArtStyle", Rating = 1100},
                new MatchPlayer(){Name = "L0likO", Rating=1454},
                new MatchPlayer(){Name = "Dread", Rating=1250},
                new MatchPlayer(){Name = "XBOCT", Rating=1250},
                new MatchPlayer(){Name = "sQreen", Rating=1075},
                new MatchPlayer(){Name = "goddam", Rating=1170}
            };

            var min = int.MaxValue;
            var minteam = 0;
            for (var team = 0; team < Math.Pow (2, playerPool.Length) - 1; ++team) 
            {
                if (IsValidTeam (team)) {
                    var opposingTeam = -team;
                    var teamScore = CalculateTeamScore (team);
                    var opposingTeamScore = CalculateTeamScore (opposingTeam);
                    var scoreDiff = Math.Abs (teamScore - opposingTeamScore);
                    Console.WriteLine ("{0}:{1} - {2}:{3} - Diff = {4}.", team, teamScore, opposingTeam, opposingTeamScore, scoreDiff);
                    if (scoreDiff < min) {
                        min = scoreDiff;
                        minteam = team;
                    }
                }
            }

            Console.WriteLine ("\nOptimal teams found!\n");
            Console.WriteLine ("Radiant: ");
            var minOpposingTeam = -minteam;
            var minTeamScore = CalculateTeamScore(minteam);
            var minOpposingTeamScore = CalculateTeamScore(minOpposingTeam);
            var minScoreDiff = Math.Abs (minTeamScore - minOpposingTeamScore);
            Console.WriteLine ("Score difference: " + minScoreDiff);
        }
    }

    class MatchPlayer
    {
        public string Name {get;set;}
        public uint Rating {get;set;}
    }
}

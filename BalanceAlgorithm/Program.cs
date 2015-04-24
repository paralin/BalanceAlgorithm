using System;

namespace BalanceAlgorithm
{
    class MainClass
    {
        static MatchPlayer[] playerPool;
        private static int CalculateTeamScore(uint team)
        {
            Console.WriteLine ("");
            var score = 0;
            for (var i = 0; i < playerPool.Length; ++i)
            {
                if ((team & 1) == 1)
                {
                    Console.WriteLine ("   " + playerPool [i].Name + ": " + playerPool [i].Rating);
                    score += (int)playerPool[i].Rating;
                }
                team >>= 1;
            }
            return score;
        } 

        private static bool IsValidTeam(uint team)
        {
            // determine how many bits are set, and return true if the result is 5
            uint v = (uint)team;
            uint c; 

            for (c = 0; v!=0; v >>= 1)
            {
                c += v & 1;
            }

            return (c == playerPool.Length/2);
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

            uint min = uint.MaxValue;
            uint minteam = 0;
            for (uint team = 0; team < (uint)(Math.Pow(2, playerPool.Length)-1); ++team) 
            {
                if (IsValidTeam (team)) {
                    var opposingTeam = ~team;
                    var teamScore = CalculateTeamScore (team);
                    var opposingTeamScore = CalculateTeamScore (opposingTeam);
                    var scoreDiff = Math.Abs (teamScore - opposingTeamScore);
                    Console.WriteLine ("{0}:{1} - {2}:{3} - Diff = {4}.", team, teamScore, opposingTeam, opposingTeamScore, scoreDiff);
                    if (scoreDiff < min) {
                        min = (uint)scoreDiff;
                        minteam = team;
                    }
                }
            }

            Console.WriteLine ("\nOptimal teams found!\n");
            var minOpposingTeam = ~minteam;
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

using System.Collections.Generic;

namespace GameUtils
{
    public static class UserScoreService
    {
        private static List<int> _userScore;

        public static List<int> GetUserScore()
        {
            if (_userScore == null)
            {
                // Load from json

                // Load not found
                _userScore = new List<int>();
                return _userScore;
            }
            else
            {
                return _userScore;
            }
        }

        public static void AddNewScore(int newScore)
        {
            if (_userScore == null)
            {
                // Load from json

                // Load not found
                _userScore = new List<int>();
                _userScore.Add(newScore);
            }
            else
            {
                _userScore.Add(newScore);
            }
            
            // save _userScore as json
        }
    }
}
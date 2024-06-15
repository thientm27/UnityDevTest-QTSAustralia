using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace GameUtils
{
    public static class UserScoreService
    {
        private static List<int> _userScore;
        private static readonly string FilePath = Application.persistentDataPath + "/" + GameConstants.JsonPath;

        public static int GetRecentScore()
        {
            List<int> userScore = GetUserScore();
            if (userScore.Count > 0)
            {
                return userScore[userScore.Count - 1];
            }

            return 0;
        }

        public static List<int> GetUserScore()
        {
            if (_userScore == null)
            {
                LoadScoresFromJson();
            }

            return _userScore;
        }

        public static void AddNewScore(int newScore)
        {
            if (_userScore == null)
            {
                LoadScoresFromJson();
            }

            _userScore.Add(newScore);
            SaveScoresToJson();
        }

        private static void LoadScoresFromJson()
        {
            if (System.IO.File.Exists(FilePath))
            {
                string json = System.IO.File.ReadAllText(FilePath);
                _userScore = JsonConvert.DeserializeObject<List<int>>(json);
            }
            else
            {
                _userScore = new List<int>();
            }
        }

        private static void SaveScoresToJson()
        {
            string json = JsonConvert.SerializeObject(_userScore);
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
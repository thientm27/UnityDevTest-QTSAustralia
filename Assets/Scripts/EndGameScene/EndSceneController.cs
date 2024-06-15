using System.Collections.Generic;
using GameUtils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGameScene
{
    public class EndSceneController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject leaderBoardPopup;
        [SerializeField] private GameObject leaderBoardItemScore;
        [SerializeField] private Transform leaderBoardContainer;

        private bool _isInit;

        private void Start()
        {
            scoreText.text = "Your Score: " + UserScoreService.GetRecentScore();
        }

        public void OnClickPlayAgain()
        {
            SceneManager.LoadScene(GameConstants.GameScene);
        }

        public void OnOpenLeaderBoard()
        {
            if (!_isInit)
            {
                var sortedList = new List<int>(UserScoreService.GetUserScore());
                sortedList.Sort();
                sortedList.Reverse();

                foreach (var score in sortedList)
                {
                    var obj = Instantiate(leaderBoardItemScore, leaderBoardContainer);
                    var scriptControl = obj.GetComponent<LeaderScoreItem>();
                    scriptControl.ScoreValue.text = score.ToString();
                }

                _isInit = true;
            }

            leaderBoardPopup.SetActive(true);
        }

        public void OnCloseLeaderBoard()
        {
            leaderBoardPopup.SetActive(false);
        }
    }
}
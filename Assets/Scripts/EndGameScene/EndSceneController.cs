using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGameScene
{
    public class EndSceneController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI lastScoreText;
        [SerializeField] private GameObject leaderBoardPopup;

        private void Start()
        {
            
        }

        public void OnClickPlayAgain()
        {
            SceneManager.LoadScene(GameConstants.GameScene);
        }

        public void OnOpenLeaderBoard()
        {
            leaderBoardPopup.SetActive(true);
        }

        public void OnCloseLeaderBoard()
        {
            leaderBoardPopup.SetActive(false);
        }
    }
}
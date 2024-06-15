using GameUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EntryScene
{
    public class EntryController : MonoBehaviour
    {
        private void Start()
        {
            UserScoreService.GetUserScore();
            SceneManager.LoadScene(GameConstants.GameScene);
        }
    }
}
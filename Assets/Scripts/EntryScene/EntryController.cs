using System;
using _06.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class EntryController : MonoBehaviour
    {
        private void Start()
        {
            var playerService = PlayerService.Instance;
            var audioService = AudioService.Instance;
            playerService.OnMusicVolumeChange = audioService.SetMusicVolume;
            playerService.OnSoundVolumeChange = audioService.SetSoundVolume;
            audioService.MusicVolume = playerService.GetMusicVolume();
            audioService.SoundVolume = playerService.GetSoundVolume();
            // audioService.MusicOn = true;
            // audioService.SoundOn = true;
            SceneManager.LoadScene(Constants.MainScene);
        }
    }
}
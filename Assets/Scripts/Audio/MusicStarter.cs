using UnityEngine;

namespace SpaceEscape.Audio
{
    public class MusicStarter : MonoBehaviour
    {
        [SerializeField] private AudioClip music;
        
        public void PlayMusic()
        {
            AudioManager.Instance.PlayMusic(music);
        }
        
        private void Start()
        {
            PlayMusic();
            AudioManager.Instance.OnMusicPlay += PlayMusic;
        }
    }
}
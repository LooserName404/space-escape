using UnityEngine;

namespace SpaceEscape.Audio
{
    public class PlaySoundOnClick : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        
        public void Play()
        {
            AudioManager.Instance?.PlaySound(clip, Camera.main.transform.position);
        }
    }
}
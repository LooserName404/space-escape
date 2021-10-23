using UnityEngine;

namespace SpaceEscape.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        private AudioManager _manager;
        private AudioSource _source;
        private bool _isPlaying;

        public void SetManager(AudioManager manager)
        {
            _manager = manager;
        }

        public void PlaySound(AudioClip clip, Vector3 pos)
        {
            transform.position = pos;
            _source.clip = clip;
            _isPlaying = true;
            _source.Play();
        }

        private void Awake()
        {
            InitializeAudioSource();
        }

        private void Update()
        {
            if (!_isPlaying) return;
            if (_source.isPlaying) return;

            _isPlaying = false;
            _manager.ReturnToPool(this);
        }

        private void InitializeAudioSource()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.loop = false;
            _source.playOnAwake = false;
            _source.volume = 0.5f;
        }
    }
}
using System.Collections;
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
            _source.transform.parent = _manager.transform;
        }

        public void PlaySound(AudioClip clip, Vector3 pos)
        {
            transform.position = pos;
            _source.clip = clip;
            _isPlaying = true;
            _source.Play();
            _manager.OnSoundToggle += StopSound;
        }

        private void StopSound()
        {
            _source.Stop();
            _isPlaying = false;
            _manager.ReturnToPool(this);
            _manager.OnSoundToggle -= StopSound;
        }

        public void PlayMusic(AudioClip clip)
        {
            transform.parent = Camera.main.transform;
            _source.clip = clip;
            _isPlaying = true;
            _source.loop = true;
            _source.Play();
            _manager.OnMusicStop += StopMusic;
        }

        private void StopMusic()
        {
            _source.Stop();
            _source.loop = false;
            _isPlaying = false;
            transform.parent = _manager.transform;
            _manager.ReturnToPool(this);
            _manager.OnMusicStop -= StopMusic;
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
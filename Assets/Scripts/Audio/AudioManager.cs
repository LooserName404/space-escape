using System;
using System.Collections.Generic;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public Action OnSoundToggle;
        public Action OnMusicPlay;
        public Action OnMusicStop;

        [SerializeField] private int initialPlayerAmount = 10;
        [SerializeField] private int createStepAmount = 2;
        [SerializeField] private BooleanVariable isSoundEnabled;
        [SerializeField] private BooleanVariable isMusicEnabled;

        private int _playerAmount;
        private Queue<AudioPlayer> _queue;

        public void PlaySound(AudioClip clip, Vector3 pos)
        {
            if (!isSoundEnabled.Value) return;
            
            var ap = GetAudioPlayer();
            ap.PlaySound(clip, pos);
        }

        public void PlayMusic(AudioClip clip)
        {
            if (!isMusicEnabled.Value) return;
            
            var ap = GetAudioPlayer();
            ap.PlayMusic(clip);
        }

        private AudioPlayer GetAudioPlayer()
        {
            if (_queue.Count <= 0)
            {
                CreateAudioPlayers(createStepAmount);
            }

            var ap = _queue.Dequeue();
            return ap;
        }

        public void ReturnToPool(AudioPlayer ap)
        {
            _queue.Enqueue(ap);
        }

        public void ToggleSound()
        {
            isSoundEnabled.Revert();
            OnSoundToggle?.Invoke();
            PlayerPrefs.SetInt("sound_enabled", isSoundEnabled.Value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void ToggleMusic()
        {
            isMusicEnabled.Revert();
            if (isMusicEnabled.Value) OnMusicPlay?.Invoke();
            else OnMusicStop?.Invoke();
            PlayerPrefs.SetInt("music_enabled", isMusicEnabled.Value ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            isSoundEnabled.SetValue(PlayerPrefs.GetInt("sound_enabled") == 1);
            isMusicEnabled.SetValue(PlayerPrefs.GetInt("music_enabled") == 1);
            
            _queue = new Queue<AudioPlayer>();
            CreateAudioPlayers();
            DontDestroyOnLoad(this);
        }

        private void CreateAudioPlayers()
        {
            CreateAudioPlayers(initialPlayerAmount);
        }

        private void CreateAudioPlayers(int playersToAdd)
        {
            for (var i = 0; i < playersToAdd; ++i)
            {
                var go = new GameObject();
                var ap = go.AddComponent<AudioPlayer>();
                ap.SetManager(this);
                _queue.Enqueue(ap);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEscape.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private int initialPlayerAmount = 10;
        [SerializeField] private int createStepAmount = 2;

        private int _playerAmount;
        private Queue<AudioPlayer> _queue;

        public void PlaySound(AudioClip clip, Vector3 pos)
        {
            if (_queue.Count <= 0)
            {
                CreateAudioPlayers(createStepAmount);
            }

            var ap = _queue.Dequeue();
            ap.PlaySound(clip, pos);
        }

        public void ReturnToPool(AudioPlayer ap)
        {
            _queue.Enqueue(ap);
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _queue = new Queue<AudioPlayer>();
            CreateAudioPlayers();
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
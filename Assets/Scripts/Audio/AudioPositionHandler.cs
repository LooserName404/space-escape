using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Audio
{
    public class AudioPositionHandler : MonoBehaviour
    {
        [SerializeField] private Vector3Variable position;
        [SerializeField] private AudioClip clip;

        public void PlayOnPosition()
        {
            AudioManager.Instance.PlaySound(clip, position.Value);
        }

        public void SetPosition(Vector3 pos)
        {
            position.SetValue(pos);
        }
    }
}
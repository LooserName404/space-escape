using UnityEngine;

namespace SpaceEscape
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        private Vector3 _offset;

        private void Start()
        {
            _offset = player.transform.position - new Vector3(0, 0, 20);
        }

        private void Update()
        {
            if (player != null) transform.position = _offset + player.transform.position;
        }
    }
}

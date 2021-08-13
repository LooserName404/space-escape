using System.Collections;
using UnityEngine;

namespace SpaceEscape
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShot : MonoBehaviour
    {
        private void Start() {
            StartCoroutine(Expire());
        }

        private IEnumerator Expire() {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
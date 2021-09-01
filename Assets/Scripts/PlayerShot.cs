using System;
using System.Collections;
using UnityEngine;

namespace SpaceEscape
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShot : MonoBehaviour
    {
        private IEnumerator _coroutine;
        private void Start()
        {
            _coroutine = Expire();
            StartCoroutine(_coroutine);
        }

        private void Update()
        {
            if (Camera.main == null)
            {
                StopCoroutine(_coroutine);
                Destroy(gameObject);
                return;
            }
            Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            var onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            
            if (onScreen) return;
            
            StopCoroutine(_coroutine);
            Destroy(gameObject);
        }

        private IEnumerator Expire() {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            StopCoroutine(_coroutine);
            
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
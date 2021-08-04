using UnityEngine;
using SpaceEscape.Utils;

namespace SpaceEscape
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float shotSpeed;
        [SerializeField] private PlayerShot shotPrefab;
        [SerializeField] private float maxVelocity;

        private Rigidbody2D _rb;

        private Vector2 _lastInput;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _lastInput = new Vector2();
        }

        private void Update()
        {
            Move();
            Shoot();
        }

        private void Move()
        {
            var dir = Vector2.zero;
            dir.x = Input.acceleration.x;
            dir.y = Input.acceleration.y;

            if (dir.sqrMagnitude > 1)
            {
                dir.Normalize();
                if (dir.x > 0 && dir.y > 0)
                {
                    dir *= 0.5f;
                }
            }

            var time = Time.deltaTime;
            
            var force = dir * (speed * time);
            _rb.AddForce(force, ForceMode2D.Force);
            
            var (x, y) = _rb.velocity;
            var (xAbs, yAbs) = _rb.velocity.Abs();
            _rb.velocity = new Vector2(xAbs > maxVelocity ? maxVelocity : x, yAbs > maxVelocity ? maxVelocity : y);
            _lastInput = dir;
            
        }

        private void Shoot()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    var touchPosition = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                    var position = transform.position;
                    var position2d = (Vector2)position;
                    var shotDirection = touchPosition - position2d;
                    shotDirection.Normalize();

                    var shot = Instantiate(shotPrefab, position2d, Quaternion.LookRotation(Vector3.forward, shotDirection.normalized));
                    var shotRb = shot.GetComponent<Rigidbody2D>();
                    shotRb.velocity = shotDirection * shotSpeed * Time.deltaTime;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
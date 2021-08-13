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
            var direction = Vector2.zero;
            direction.x = Input.acceleration.x;
            direction.y = Input.acceleration.y;

            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
                if (direction.x > 0 && direction.y > 0)
                {
                    direction *= 0.5f;
                }
            }

            var time = Time.deltaTime;
            
            var force = direction * (speed * time);
            _rb.AddForce(force, ForceMode2D.Force);
            
            var (x, y) = _rb.velocity;
            var (xAbsolute, yAbsolute) = _rb.velocity.Abs();
            _rb.velocity = new Vector2(xAbsolute > maxVelocity ? maxVelocity : x, yAbsolute > maxVelocity ? maxVelocity : y);
            _lastInput = direction;
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
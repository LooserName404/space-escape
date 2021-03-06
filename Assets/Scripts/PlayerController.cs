using System.Collections;
using SpaceEscape.EventSystem;
using SpaceEscape.Graphics;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;
using SpaceEscape.Utils;
using UnityEngine.SceneManagement;

namespace SpaceEscape
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float shotSpeed;
        [SerializeField] private float maxVelocity;
        
        [SerializeField] private PlayerShot shotPrefab;
        [SerializeField] private GameEvent onPlayerDie;
        [SerializeField] private GameEvent onPlayerMove;
        [SerializeField] private FloatVariable playerMovedDistance;
        [SerializeField] private Vector3Variable playerPosition;

        [SerializeField] private Color colorShotBlink;

        private Rigidbody2D _rb;
        private BlinkShader _blinkShader;

        private bool _canShoot;
        private IEnumerator _shotCooldownCoroutine;
        private Vector2 _currentForce;
        private Vector2 _lastPosition;

        public void OnShoot()
        {
            _blinkShader.SetTintColor(colorShotBlink);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _blinkShader = GetComponent<BlinkShader>();
            _canShoot = true;
            _lastPosition = transform.position;
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            Shoot();
            GetForce();
            HandleMovementActions();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            StartCoroutine(ResetGame());
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            onPlayerDie.Raise();
        }

        private void GetForce()
        {
            var direction = Vector2.zero;
            var inputY = Input.acceleration.y;
            direction.x = Input.acceleration.x;
            direction.y = MathUtils.RemapValue(inputY, -1f, 1f, -0.675f, 1.325f);

            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
                if (direction.x > 0 && direction.y > 0)
                {
                    direction *= 0.5f;
                }
            }
            _currentForce = direction * speed;
        }

        private void HandleMovementActions()
        {
            var pos = transform.position;
            var dist = Vector2.Distance(_lastPosition, pos);
            playerMovedDistance.Value += Mathf.Abs(dist);
            _lastPosition = pos;
            if (dist != 0)
            {
                onPlayerMove.Raise();
            }
            playerPosition.SetValue(pos);
        }

        private void Move()
        {
            _rb.AddForce(_currentForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            
            var (x, y) = _rb.velocity;
            var (xAbsolute, yAbsolute) = _rb.velocity.Abs();
            var vx = xAbsolute > maxVelocity
                ? (x > 0 ? maxVelocity : -maxVelocity)
                : x;
            var vy =
                yAbsolute > maxVelocity
                    ? (y > 0 ? maxVelocity : -maxVelocity)
                    : y;
            _rb.velocity = new Vector2(vx, vy);
        }

        private void Shoot()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && _canShoot)
                {
                    var touchPosition = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);

                    _shotCooldownCoroutine = ShotCooldown();
                    StartCoroutine(_shotCooldownCoroutine);

                    var position2d = (Vector2)transform.position;
                    var shotDirection = touchPosition - position2d;
                    shotDirection.Normalize();

                    var (lookX, lookY) = touchPosition - _rb.position;
                    var angle = Mathf.Atan2(lookY, lookX) * Mathf.Rad2Deg - 90f;
                    
                    var shot = Instantiate(shotPrefab, position2d, Quaternion.identity);
                    var shotRb = shot.GetComponent<Rigidbody2D>();
                    
                    shotRb.rotation = angle;
                    shotRb.velocity = shotDirection * shotSpeed * Time.deltaTime;
                }
            }
        }

        private IEnumerator ShotCooldown()
        {
            _canShoot = false;
            yield return new WaitForSeconds(0.35f);
            _canShoot = true;
        }

        private static IEnumerator ResetGame()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Scenes/PlayScene");
        }
    }
}
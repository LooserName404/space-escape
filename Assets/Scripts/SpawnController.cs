using System.Collections;
using UnityEngine;

namespace SpaceEscape
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private EnemyController enemyPrefab;

        [SerializeField] private int minSpawnRate;
        [SerializeField] private int maxSpawnRate;
        
        private Vector2[] _positions;
        private int[] _rate;

        private void Awake()
        {
            var cam = Camera.main;
            
            var topRight = (Vector2) cam.ViewportToWorldPoint(new Vector3(1, 1));
            var topLeft = (Vector2) cam.ViewportToWorldPoint(new Vector3(0, 1));
            var bottomRight = (Vector2) cam.ViewportToWorldPoint(new Vector3(1, 0));
            var bottomLeft = (Vector2) cam.ViewportToWorldPoint(new Vector3(0, 0));
            
            _positions = new[]
            {
                topRight + new Vector2(2, 2),
                topLeft + new Vector2(-2, 2),
                bottomRight + new Vector2(2, -2),
                bottomLeft + new Vector2(-2, -2)
            };

            _rate = new int[4];
            for (var i = 0; i < _rate.Length; i++)
            {
                _rate[i] = Random.Range(minSpawnRate, maxSpawnRate);
            }
        }

        private void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(InitializeSpawner(_positions[i], _rate[i]));
            }
        }

        private IEnumerator InitializeSpawner(Vector2 position, int rate)
        {
            while (true)
            {
                yield return new WaitForSeconds(rate);
                var enemy = Instantiate(enemyPrefab, position + (Vector2)transform.position, Quaternion.identity);
                if (player != null)enemy.SetTarget(player.transform);
                else break;
            }
        }

        private void OnDrawGizmos()
        {
            if (!(_positions?.Length > 0)) return;
            foreach (var position in _positions)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(position + (Vector2)transform.position, 0.25f);
            }
        }
    }
}
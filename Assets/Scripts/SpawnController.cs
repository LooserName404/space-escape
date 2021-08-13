using System.Collections;
using UnityEngine;

namespace SpaceEscape
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private EnemyController enemyPrefab;
        
        private Vector2[] _positions;
        private int[] _rate;

        private void Awake()
        {
            var randXP = Random.Range(0, 10);
            var randXN = Random.Range(0, 10);
            var randYP = Random.Range(0, 10);
            var randYN = Random.Range(0, 10);

            var topRight = (Vector2) Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
            var topLeft = (Vector2) Camera.main.ViewportToWorldPoint(new Vector3(0, 1));
            var bottomRight = (Vector2) Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
            var bottomLeft = (Vector2) Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
            
            _positions = new[]
            {
                topRight + new Vector2(randXP, randYP),
                topLeft + new Vector2(randXN, randYP),
                bottomRight + new Vector2(randXP, randYN),
                bottomLeft + new Vector2(randXN, randYN)
            };

            _rate = new int[4];
            for (var i = 0; i < _rate.Length; i++)
            {
                _rate[i] = Random.Range(4, 10);
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
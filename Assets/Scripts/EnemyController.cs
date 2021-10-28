using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceEscape
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        
        private Transform _target;
        private float _moveRate;

        private void Awake()
        {
            _moveRate = Random.Range(minSpeed, maxSpeed);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveRate * Time.deltaTime);
            }
        }
    }
}
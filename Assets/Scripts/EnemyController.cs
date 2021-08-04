using UnityEngine;

namespace SpaceEscape
{
    public class EnemyController : MonoBehaviour
    {
        private Transform _target;
        private float _moveRate;

        private void Awake()
        {
            _moveRate = Random.Range(1f, 2.5f);
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
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape
{
    public class MoveToPoint : MonoBehaviour
    {
        [SerializeField] private Reference<Vector3> target;

        public void Move()
        {
            transform.position = target.Value;
        }
    }
}
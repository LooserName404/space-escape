using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    [CreateAssetMenu]
    public class IntVariable : Variable<int>
    {
        public void Add(int value)
        {
            Value += value;
        }
    }
}
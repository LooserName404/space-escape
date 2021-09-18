using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    [CreateAssetMenu]
    public class FloatVariable : Variable<float>
    {
        public void Add(float value)
        {
            Value += value;
        }
    }
}
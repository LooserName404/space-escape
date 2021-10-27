using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    [CreateAssetMenu]
    public class BooleanVariable : Variable<bool>
    {
        public void Revert()
        {
            Value = !Value;
        }
    }
}
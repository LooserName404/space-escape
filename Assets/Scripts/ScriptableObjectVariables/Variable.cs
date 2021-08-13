using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    public class Variable<T> : ScriptableObject
    {
        #if UNITY_EDITOR
            [Multiline]
            public string DeveloperDescription = "";
        #endif
        
        public T Value;
        
        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(Variable<T> value)
        {
            Value = value.Value;
        }
    }
}
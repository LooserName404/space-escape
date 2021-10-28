using UnityEngine;

namespace SpaceEscape.Utils
{
    public class MathUtils
    {
        public static float RemapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            var outputDiff = Mathf.Abs(outputMax - outputMin);
            var inputDiff = Mathf.Abs(inputMax - inputMin);
            var conversionFactor = outputDiff / inputDiff;
            return outputMin + (conversionFactor * (value - inputMin));
        }
    }
}
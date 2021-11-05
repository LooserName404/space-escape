using UnityEngine;

namespace SpaceEscape.Graphics
{
    public class BlinkShader : MonoBehaviour
    {
        private Material _material;
        private Color _materialTintColor;
        private float _tintFadeSpeed;
        private static readonly int Tint = Shader.PropertyToID("_Tint");

        public void SetMaterial(Material material)
        {
            _material = material;
        }

        public void SetTintColor(Color color)
        {
            _materialTintColor = color;
            _material.SetColor(Tint, _materialTintColor);
        }

        public void SetTintFadeSpeed(float fadeSpeed)
        {
            _tintFadeSpeed = fadeSpeed;
        }
        
        private void Awake()
        {
            _materialTintColor = new Color(0, 0, 0 ,0);
            var mat = GetComponent<SpriteRenderer>().material;
            SetMaterial(mat);
            _tintFadeSpeed = 3f;
        }

        private void Update()
        {
            if (_materialTintColor.a > 0)
            {
                _materialTintColor.a = Mathf.Clamp01(_materialTintColor.a - _tintFadeSpeed * Time.deltaTime);
                _material.SetColor(Tint, _materialTintColor);
            }
        }
    }
}
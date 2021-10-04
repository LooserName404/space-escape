using System;
using UnityEngine;

namespace SpaceEscape.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer backgroundChunkPrefab;

        private GameObject[][] _background = { new GameObject[3], new GameObject[3], new GameObject[3] };

        private void Start()
        {
            
        }

        private Vector2 GetPosition(int x, int y)
        {
            var size = backgroundChunkPrefab.size;
            
            var posX = x switch
            {
                0 => -size.x,
                1 => 0,
                2 => size.x
            };

            var posY = y switch
            {
                0 => -size.y,
                1 => 0,
                2 => size.y
            };

            return new Vector2(posX, posY);
        }
    }
}
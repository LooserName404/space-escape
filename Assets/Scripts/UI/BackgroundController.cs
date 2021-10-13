using System;
using UnityEngine;

namespace SpaceEscape.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundChunkPrefab;

        private GameObject[][] _background = { new GameObject[3], new GameObject[3], new GameObject[3] };

        private int[][] _xThreshold = { new[] { -1, -1, -1 }, new[] { 0, 0, 0 }, new[] { 1, 1, 1 } };

        private int[][] _yThreshold = { new[] { 1, 0, -1 }, new[] { 1, 0, -1 }, new[] { 1, 0, -1 } };

        private Vector2 _size;
        
        private enum Direction { LEFT, RIGHT }

        private void Awake()
        {
            _size = backgroundChunkPrefab.GetComponent<SpriteRenderer>().size;
        }

        private void Start()
        {
            for (var i = 0; i < _background.Length; i++)
            {
                for (var j = 0; j < _background[i].Length; j++)
                {
                    var pos = GetPosition(i, j);
                    _background[i][j] = Instantiate(backgroundChunkPrefab, pos, Quaternion.identity);
                }
            }
        }

        private void Update()
        {
            var cam = Camera.main;

            if (cam == null) return;

            for (var i = 0; i < _background.Length; i++)
            {
                for (var j = 0; j < _background[i].Length; j++)
                {
                    if (i == 1 && j == 1) continue;

                    var xMultiplier = _xThreshold[i][j];
                    var yMultiplier = _yThreshold[i][j];

                    var pos = _background[i][j].transform.position;

                    if (xMultiplier == -1 && cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x < pos.x)
                    {
                        SwapX(Direction.RIGHT);
                    }
                    else if (xMultiplier == 1 && cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x > pos.x)
                    {
                        Debug.Log("Plus X");
                    }
                }
            }
        }

        private void SwapX(Direction direction)
        {
            var len = _background.Length;
            if (direction == Direction.LEFT)
            {
                var aux = _background[0];
                
                for (var i = 0; i < len - 1; i++)
                {
                    _background[i] = _background[i + 1];
                }

                _background[_background.Length - 1] = aux;

                for (var i = 0; i < _background.Length; i++)
                {
                    for (int j = 0; j < _background[i].Length; j++)
                    {
                        _background[i][j].transform.position += new Vector3(_size.x * 1.5f, 0, 0);
                    }
                }
            }
            else if (direction == Direction.RIGHT)
            {
                var aux = _background[len - 1];
                
                for (var i = len - 1; i > 1; i--)
                {
                    _background[i] = _background[i - 1];
                }

                _background[0] = aux;

                for (var i = 0; i < _background.Length; i++)
                {
                    for (int j = 0; j < _background[i].Length; j++)
                    {
                        _background[i][j].transform.position -= new Vector3(_size.x * 1.5f, 0, 0);
                    }
                }
            }
        }

        private Vector3 GetPosition(int x, int y)
        {
            var posX = x switch
            {
                0 => -_size.x * 1.5f,
                1 => 0,
                2 => _size.x * 1.5f,
                _ => throw new ArgumentOutOfRangeException()
            };

            var posY = y switch
            {
                0 => -_size.y / 1.38f,
                1 => 0,
                2 => _size.y / 1.38f,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Vector3(posX, posY, 10);
        }
    }
}
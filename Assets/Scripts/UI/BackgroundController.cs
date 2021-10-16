using System;
using System.Linq;
using UnityEngine;

namespace SpaceEscape.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundChunkPrefab;

        private GameObject[][] _background = {new GameObject[3], new GameObject[3], new GameObject[3]};

        private int[][] _xThreshold = {new[] {-1, 0, 1}, new[] {-1, 0, 1}, new[] {-1, 0, 1}};

        private int[][] _yThreshold = {new[] {-1, -1, -1}, new[] {0, 0, 0}, new[] {1, 1, 1}};

        private Vector2 _size;

        private Vector2 _offset = Vector2.zero;

        private enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

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

            var last = _background[_background.Length - 1];

            var minChunk = _background[0][0].transform.position;
            var maxChunk = last[last.Length - 1].transform.position;

            var negativeBoundX = minChunk.x;
            var positiveBoundX = maxChunk.x;

            var negativeBoundY = minChunk.y;
            var positiveBoundY = maxChunk.y;

            var minPos = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var maxPos = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

            if (minPos.x < negativeBoundX)
            {
                SwapX(Direction.Right);
            }
            else if (maxPos.x > positiveBoundX)
            {
                SwapX(Direction.Left);
            }

            if (minPos.y < negativeBoundY)
            {
                SwapY(Direction.Down);
            }
            else if (maxPos.y > positiveBoundY)
            {
                SwapY(Direction.Up);
            }
        }

        private void SwapX(Direction direction)
        {
            var multiplier = direction switch
            {
                Direction.Left => 1,
                Direction.Right => -1,
                _ => 0
            };

            foreach (var chunks in _background)
            {
                foreach (var chunk in chunks)
                {
                    chunk.transform.position += new Vector3(_size.x / 2 * 1.5f * multiplier, 0, 0);
                }
            }
        }

        private void SwapY(Direction direction)
        {
            var multiplier = direction switch
            {
                Direction.Up => 1,
                Direction.Down => -1,
                _ => 0
            };

            foreach (var chunks in _background)
            {
                foreach (var chunk in chunks)
                {
                    chunk.transform.position += new Vector3(0, _size.y / 2 * 1.38f * multiplier, 0);
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

            var vec = new Vector3(posX, posY, 10);
            return vec;
        }

        private void OnDrawGizmos()
        {
            var cam = Camera.main;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)), 1);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)), 1);
            Gizmos.color = Color.green;
            foreach (var line in _background)
            {
                foreach (var bg in line)
                {
                    if (bg != null)
                    {
                        Gizmos.DrawSphere(bg.transform.position, 0.5f);
                    }
                }
            }
        }
    }
}
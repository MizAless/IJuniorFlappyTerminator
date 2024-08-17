using System.Collections.Generic;
using UnityEngine;

public class DeadlyWalls : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<DeadlyWall> _walls;

    private void OnEnable()
    {
        _walls.ForEach(wall => wall.Collide += _player.Die);
    }

    private void OnDisable()
    {
        _walls.ForEach(wall => wall.Collide -= _player.Die);
    }
}

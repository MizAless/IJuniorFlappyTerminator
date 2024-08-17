using UnityEngine;

public class SpawnPoint
{
    public Enemy Enemy { get; private set; }
    public Vector3 Position { get; private set; }
    public bool IsFree { get; private set; }


    public SpawnPoint(Vector3 position)
    {
        Position = position;
        IsFree = true;
        Enemy = null;
    }

    public void SetEnemy(Enemy enemy)
    {
        Enemy = enemy;
        IsFree = false;
        enemy.Dying += Clear;
    }

    private void Clear(Enemy enemy)
    {
        enemy.Dying -= Clear;
        Enemy = null;
        IsFree = true;
    }
}

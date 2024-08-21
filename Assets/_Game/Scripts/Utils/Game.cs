using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        _player.Died += RestartGame;
    }

    public void StartGame()
    {
        _enemySpawner.StartSpawning();
        _player.StartFlying();
        _startButton.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

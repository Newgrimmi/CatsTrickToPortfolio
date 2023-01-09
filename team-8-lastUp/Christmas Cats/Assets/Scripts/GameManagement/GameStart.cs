using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;
    public int FirstGame { get; private set; }

    private void Awake()
    {
        FirstGame = PlayerPrefs.GetInt("FirstGame");
    }

    private void Start()
    {
        LoadGame();
    }

    public void GameStarted()
    {
        FirstGame = 1;
        PlayerPrefs.SetInt("FirstGame", FirstGame);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainScene");
    }
    public void LoadGame()
    {
        if(FirstGame == 1)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            audio.Play();
        }
    }
}

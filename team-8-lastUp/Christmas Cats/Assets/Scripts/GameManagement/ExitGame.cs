using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private SaveGame saveGame;
    public void Exit()
    {
        saveGame.Save();
        saveGameData.Save();
        Application.Quit();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Exit();
        }
    }

}

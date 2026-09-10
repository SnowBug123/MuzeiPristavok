using UnityEngine;
using UnityEngine.SceneManagement; // Пространство имен для работы со сценами

public class Menu : MonoBehaviour
{
    public string gameSceneName = "SampleScene";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

}
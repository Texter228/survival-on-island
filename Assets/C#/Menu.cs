using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject asktable;
    public void Play(int numberlvl)
    {
        SceneManager.LoadScene(numberlvl);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        asktable.SetActive(true);
    }
}

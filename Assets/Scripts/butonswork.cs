using UnityEngine;
using UnityEngine.SceneManagement;
public class butonswork : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("scene1");
    }

    public void LoadSaves()
    {
        SceneManager.LoadScene("saves");
    }

    public void Quitgame()
    {
        Application.Quit();
    }
    
        
}

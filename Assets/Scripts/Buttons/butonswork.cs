using UnityEngine;
using UnityEngine.SceneManagement;
public class butonswork : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene("scene1");
    }

    public void savesload()
    {
        SceneManager.LoadScene("Saves");
    }
    
    public void Quitt()
    {
        Application.Quit();
    }
    
        
}

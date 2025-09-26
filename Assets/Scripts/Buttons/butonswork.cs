using UnityEngine;
using UnityEngine.SceneManagement;
public class butonswork : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene("CutScene1");
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

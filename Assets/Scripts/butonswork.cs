using UnityEngine;
using UnityEngine.SceneManagement;
public class butonswork : MonoBehaviour
{
    public void LoadScene(string scene1 )
    {
        SceneManager.LoadScene( scene1 );
    }
}

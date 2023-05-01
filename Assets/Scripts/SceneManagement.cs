using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void ChangeScene(int scene)
    {
        if (scene == -1)
        {
            switch(SceneManager.GetActiveScene().buildIndex)
            {
                case 9:
                    SceneManager.LoadScene(0);
                    break;
                default:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
            }
        }
        else SceneManager.LoadScene(scene);
    }
}

using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < GameObject.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (GameObject.FindObjectsOfType<DontDestroy>()[i] == this) break;
            if (GameObject.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

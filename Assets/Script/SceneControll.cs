using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public void LoadScene(string Main)
    {
        SceneManager.LoadScene(Main);
    }
}
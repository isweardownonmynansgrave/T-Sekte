using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject optionsUI;

    public void OnPlay()
    {
        SceneManager.LoadScene("GameScene_Maybe");
    }

    public void OnOptions()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private void Start()
    {
        GetComponent<RectTransform>().DOLocalMoveY(1080f, 20f);
        Invoke("BackToMainMenu", 21f);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private void Start()
    {
		GetComponent<RectTransform>().DOLocalMoveY(4600f, 100f).SetEase(Ease.Linear);
        Invoke("BackToMainMenu", 101f);
    }

    private void BackToMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
}

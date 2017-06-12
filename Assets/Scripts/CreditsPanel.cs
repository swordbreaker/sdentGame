using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private void Start()
    {
		GetComponent<RectTransform>().DOLocalMoveY(4600f, 30f).SetEase(Ease.Linear);
        Invoke("BackToMainMenu", 31f);
    }

    private void BackToMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private void Start()
    {
		GetComponent<RectTransform>().DOLocalMoveY(8000f, 200f);
        Invoke("BackToMainMenu", 201f);
    }

    private void BackToMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using Assets.Scripts.Sound;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private void Start()
    {
		GetComponent<RectTransform>().DOLocalMoveY(4600f, 30f).SetEase(Ease.Linear);
        StartCoroutine(FadeOutAudioListener(2, 0.1f, 29));
        Invoke("BackToMainMenu", 32f);
    }

    private void BackToMainMenu()
    {
        EndingMusicPlayer.DestroyInstance();
        AudioListener.volume = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FadeOutAudioListener(float fadeDuration = 1.5f, float decreaseInterval = 0.1f, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        var step = decreaseInterval / fadeDuration;
        while (AudioListener.volume > 0 + step)
        {
            AudioListener.volume -= step;
            yield return new WaitForSeconds(decreaseInterval);
        }
        AudioListener.volume = 0;
    }
}

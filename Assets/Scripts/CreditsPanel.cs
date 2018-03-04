using System.Collections;
using Assets.Scripts.Sound;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private float _secondsPerScreen = 15.0f;
    private float _delayBeforeSwitch = 0f;

    private void Start()
    {
        var scrollDuration = 4600f / 1080 * _secondsPerScreen;
        var creditsDuration = scrollDuration + _delayBeforeSwitch;
        GetComponent<RectTransform>().DOLocalMoveY(4600f, scrollDuration).SetEase(Ease.Linear);
        //StartCoroutine(FadeOutAudioListener(_delayBeforeSwitch, 0.1f, scrollDuration));
        Invoke("BackToMainMenu", creditsDuration);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

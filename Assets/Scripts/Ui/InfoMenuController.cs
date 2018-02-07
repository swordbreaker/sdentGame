using ItsHarshdeep.LoadingScene.Controller;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenuController : MonoBehaviour {

    [SerializeField] private Button _back;
    [SerializeField] private Button _website;

	void Start () 
    {
        _back.onClick.AddListener(() => SceneController.LoadLevel("MainMenu"));
        _website.onClick.AddListener(() => Application.OpenURL("https://saiwa-d891b.firebaseapp.com/"));
	}

}

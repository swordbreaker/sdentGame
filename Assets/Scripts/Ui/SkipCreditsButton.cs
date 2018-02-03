using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkipCreditsButton : MonoBehaviour 
{

	void Start () 
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(0));
	}

}

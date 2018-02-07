using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour 
{

    [SerializeField] private Button skipCreditsButton;

	void Start() 
    {
        skipCreditsButton.gameObject.SetActive(false);
	}
	
	void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            skipCreditsButton.gameObject.SetActive(true);
        }
	}

    private void OnDestroy()
    {
        Destroy(GameObject.Find("EndingMusicPlayer"));
    }

}

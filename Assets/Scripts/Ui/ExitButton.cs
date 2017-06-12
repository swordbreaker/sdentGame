using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class ExitButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }
}

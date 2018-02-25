using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuPanel;
        private MoveController _moveController;

        [SerializeField]
        private GameObject _graphicSettingsPanel;

        private void Start()
        {
            _pauseMenuPanel.SetActive(false);
            _moveController = FindObjectOfType<MoveController>();
        }

        private void Update()
        {
            if (_graphicSettingsPanel.activeSelf) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pauseMenuPanel.SetActive(!_pauseMenuPanel.activeSelf);

                if (_pauseMenuPanel.activeSelf)
                {
                    _moveController.MouseLook.SetCursorLock(false);
                    Time.timeScale = 0;
                }
                else
                {
                    _moveController.MouseLook.SetCursorLock(true);
                    Time.timeScale = 1;
                }
            }
        }
    }
}

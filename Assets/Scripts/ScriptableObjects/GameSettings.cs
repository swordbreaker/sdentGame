using System;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "SAIwA/GameSetting")]
    public class GameSettings : ScriptableObject
    {
        [Serializable]
        public struct MouseSensitivty
        {
            public float X;
            public float Y;
        }

        [Serializable]
        public struct PostProcessing
        {
            public bool UseBloom;
            public bool UseMoitionBlur;
        }

        public MouseSensitivty MouseSensitivtySettings = new MouseSensitivty();
        public PostProcessing PostProcessingSettings = new PostProcessing();
    }
}

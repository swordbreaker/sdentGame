using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts.Settings
{
    [Serializable]
    public class GraphicSettings
    {
        private const string FileName = "grapic.settings";
        private static readonly string Path = Application.persistentDataPath + "/" + FileName;


        private static GraphicSettings _instance;
        private bool _useDepthOfFile;
        private bool _useBloom;
        private bool _useAmbientOccultion;
        private bool _useColorGrading;
        private bool _useMotionBlur;
        private int _qualityLevel;

        private bool _saveToFile = false;

        public static GraphicSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Load();
                }
                return _instance;
            }
        }

        public static GraphicSettings Load()
        {
            if (!File.Exists(Path))
            {
                return CreateDefault();
            }

            try
            {
                var serializer = new XmlSerializer(typeof(GraphicSettings));
                using (var reader = new StreamReader(Path))
                {
                    var settings = (GraphicSettings)serializer.Deserialize(reader);
                    settings._saveToFile = true;
                    return settings;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Graphic Settings Load:" + e);
                return CreateDefault();
            }
        }

        public static GraphicSettings CreateDefault()
        {
            var gs = new GraphicSettings
            {
                UseAmbientOccultion = true,
                UseBloom = true,
                UseColorGrading = true,
                UseDepthOfFile = true,
                UseMotionBlur = false,
                QualityLevel = QualitySettings.GetQualityLevel()
            };

            gs._saveToFile = true;
            gs.Save();

            return gs;
        }

        public void Save(bool retry = false)
        {
            if (!_saveToFile) return;
            try
            {
                var ser = new XmlSerializer(typeof(GraphicSettings));
                using (var writer = new StreamWriter(Path))
                {
                    ser.Serialize(writer, this);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Graphic Settings Save:" + e);
                if (retry) return;
                Save(true);
            }
        }

        [NonSerialized]
        private readonly PostProcessProfile postProcessProfile;

        public GraphicSettings()
        {
            postProcessProfile = Resources.Load<PostProcessProfile>("Post Process Volume Profile");
        }

        public void ChangePostProcessSettingsEnabled<T>(bool enabled) where T : PostProcessEffectSettings
        {
            T settings;
            if (postProcessProfile.TryGetSettings<T>(out settings))
            {
                settings.enabled.Override(enabled);
            }
        }

        public bool UseDepthOfFile
        {
            get
            {
                return _useDepthOfFile;
            }
            set
            {
                if (_useDepthOfFile == value) return;
                ChangePostProcessSettingsEnabled<DepthOfField>(value);
                _useDepthOfFile = value;
                Save();
            }
        }

        public bool UseBloom
        {
            get
            {
                return _useBloom;
            }
            set
            {
                if (_useBloom == value) return;
                ChangePostProcessSettingsEnabled<Bloom>(value);
                _useBloom = value;
                Save();
            }
        }

        public bool UseAmbientOccultion
        {
            get
            {
                return _useAmbientOccultion;
            }
            set
            {
                if (_useAmbientOccultion == value) return;
                ChangePostProcessSettingsEnabled<AmbientOcclusion>(value);
                _useAmbientOccultion = value;
                Save();
            }
        }

        public bool UseColorGrading
        {
            get
            {
                return _useColorGrading;
            }
            set
            {
                if (_useColorGrading == value) return;
                ChangePostProcessSettingsEnabled<ColorGrading>(value);
                _useColorGrading = value;
                Save();
            }
        }

        public bool UseMotionBlur
        {
            get
            {
                return _useMotionBlur;
            }
            set
            {
                if (_useMotionBlur == value) return;
                ChangePostProcessSettingsEnabled<MotionBlur>(value);
                _useMotionBlur = value;
                Save();
            }
        }

        public int QualityLevel
        {
            get
            {
                return _qualityLevel;
            }
            set
            {
                if (_qualityLevel == value) return;
                if (value < 0 && value > QualitySettings.names.Length) return;

                QualitySettings.SetQualityLevel(value);
                _qualityLevel = value;
                Save();
            }
        }
    }
}

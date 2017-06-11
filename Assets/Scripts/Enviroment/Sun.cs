using System;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.Enviroment
{
    public class Sun : MonoBehaviour
    {
        //private VideoPlayer _videoPlayer;
        private float _playBackSpeed = 1f;
        private int _clipId = 0;

        [SerializeField] private VideoClip[] _videoClips;
        private VideoPlayer[] _videoPlayers;

        private void Start()
        {
            _videoPlayers = new VideoPlayer[_videoClips.Length];
            for (int i = 0; i < _videoClips.Length; i++)
            {
                _videoPlayers[i] = gameObject.AddComponent<VideoPlayer>();
                _videoPlayers[i].clip = _videoClips[i];
                _videoPlayers[i].targetMaterialRenderer = GetComponent<MeshRenderer>();
                _videoPlayers[i].targetMaterialProperty = "_MainTex";
                _videoPlayers[i].renderMode = VideoRenderMode.MaterialOverride;
                _videoPlayers[i].playOnAwake = false;
                _videoPlayers[i].playbackSpeed = _playBackSpeed;
                _videoPlayers[i].loopPointReached += VideoPlayerOnLoopPointReached;
            }

            _videoPlayers[0].Play();
            _videoPlayers[1].Prepare();

            //_videoPlayer = GetComponent<VideoPlayer>();
            //_videoPlayer.clip = _videoClips[_clipId];
            //_videoPlayer.loopPointReached += VideoPlayerOnLoopPointReached;
            //_videoPlayer.playbackSpeed = _playBackSpeed;
            //_videoPlayer.Play();
        }

        private void VideoPlayerOnLoopPointReached(VideoPlayer source)
        {
            source.Stop();
            print("Switch");
            _clipId = (_clipId + 1) % (_videoClips.Length);
            var next = (_clipId + 1) % (_videoClips.Length);
            _videoPlayers[_clipId].Play();
            _videoPlayers[next].Prepare();
        }
    }
}

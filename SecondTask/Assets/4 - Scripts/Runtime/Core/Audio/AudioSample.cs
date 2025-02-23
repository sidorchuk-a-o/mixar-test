using System;
using UnityEngine;

namespace AD.ToolsCollection
{
    [Serializable]
    public class AudioSample
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField, Range(0, 1)] private float _volume = 1.0f;

        public void Play(Vector3 position)
        {
            if (_clip != null)
            {
                AudioSource.PlayClipAtPoint(_clip, position, _volume);
            }
        }
    }
}
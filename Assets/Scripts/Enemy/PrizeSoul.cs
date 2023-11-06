using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class PrizeSoul : Soul
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _jingle;

        public override void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _jingle.volume = 1;
            }
            else
                _jingle.volume = PlayerPrefs.GetFloat(SoundVolume);

            _target = GameObject.FindGameObjectWithTag(SoulCounter);
            StartCoroutine(Fly());
            _reward = 10;
            _jingle.Play();
        }

        public override IEnumerator Fly()
        {            
            yield return StartCoroutine(MoveToTarget(transform, _target.transform.position));
        }
    }
}
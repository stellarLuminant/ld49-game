using Hellmade.Sound;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [System.Serializable]
    public class AudioStepClip
    {
        public AudioClip stepClip;
        public float volume = 1;
        public float pitchVariation = 0.1f;
    }
    public AudioStepClip[] audioStep;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void DoWalkSound()
    {
        var index = Random.Range(0, audioStep.Length);
        var randomAudio = audioStep[index];

        SoundHelper.PlaySoundWithPitchVariation(
            randomAudio.stepClip, 
            randomAudio.volume, 
            randomAudio.pitchVariation);
    }
}

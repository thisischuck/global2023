using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
	public AudioClip[] clips;

	public RangedFloat volume;

	[MinMaxRange(0, 2)]
	public RangedFloat pitch;

    [MinMaxRange(-1, 1)]
	public RangedFloat stereoPan;

	public override void Play(AudioSource source = null)
	{
		if (clips.Length == 0) return;

        if(source == null) source = NewSource();

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volume.minValue, volume.maxValue);
		source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
		source.panStereo = Random.Range(stereoPan.minValue, stereoPan.maxValue);
		source.Play();
	}

    private AudioSource NewSource()
    {
        GameObject source = new GameObject(this.name + "_Dynamic");
        Destroy(source, clips[0].length + .2f);

        return source.AddComponent<AudioSource>();
    }
}
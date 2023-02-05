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

	private AudioSource _source = null;

	public override void Play(AudioSource source = null, bool loop = false)
	{
		if (clips.Length == 0) return;

        if(source == null) source = NewSource(loop);
		_source = source;

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volume.minValue, volume.maxValue);
		source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
		source.panStereo = Random.Range(stereoPan.minValue, stereoPan.maxValue);
		source.loop = loop;
		source.Play();
	}

    private AudioSource NewSource(bool destroy = false)
    {
        GameObject source = new GameObject(this.name + "_Dynamic");
		
        if (destroy)
			Destroy(source, clips[0].length + .2f);

        return source.AddComponent<AudioSource>();
    }
	public bool IsPlaying()
	{
		return _source != null;
	}

	public void Stop()
	{
		if (IsPlaying())
		{
			_source.Stop();
		}
	}
}
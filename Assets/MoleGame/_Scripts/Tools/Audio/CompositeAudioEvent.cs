using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Composite")]
public class CompositeAudioEvent : AudioEvent
{
	[Serializable]
	public struct CompositeEntry
	{
		public AudioEvent Event;
		public float Weight;
	}

	public CompositeEntry[] Entries;

	public override void Play(AudioSource source = null, bool loop = false)
	{
        if(source == null) source = NewSource();

		float totalWeight = 0;
		for (int i = 0; i < Entries.Length; ++i)
			totalWeight += Entries[i].Weight;

		float pick = Random.Range(0, totalWeight);
		for (int i = 0; i < Entries.Length; ++i)
		{
			if (pick > Entries[i].Weight)
			{
				pick -= Entries[i].Weight;
				continue;
			}

			Entries[i].Event.Play(source, loop);
			return;
		}
	}
	
    private AudioSource NewSource()
    {
        GameObject source = new GameObject(this.name + "_Dynamic");
        Destroy(source, 2f); //FIXME: Time as nothing to do with sound duration

        return source.AddComponent<AudioSource>();
    }
}
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	public static Action<bool> OnChangeState { get; set; }

	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	private float _comboPitch;

	private bool _play = true;

    private void OnEnable()
    {
		OnChangeState += SoundState;
	}
    private void OnDisable()
    {
		OnChangeState -= SoundState;
	}

    private void Awake()
	{
		Instance = this;
	}

	public void SoundState(bool state)
    {
		_play = state;
	}

	public void Play(AudioClip clip)
	{
		if (!_play) return;

		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	public void PlayMusic(AudioClip clip)
	{
		if (!_play) return;

		MusicSource.clip = clip;
		MusicSource.Play();
	}

	public void ComboSoundEffect(AudioClip clip)
    {
		if (!_play) return;

		float pitchStep = (HighPitchRange - LowPitchRange) / 3f;

		EffectsSource.pitch = Mathf.Clamp(_comboPitch, LowPitchRange, HighPitchRange);
		EffectsSource.clip = clip;
		EffectsSource.Play();

		_comboPitch += pitchStep;

		CancelInvoke();
		Invoke(nameof(ReducePitch), 1f);
	}

	private void ReducePitch()
    {
		_comboPitch = LowPitchRange;
	}
}

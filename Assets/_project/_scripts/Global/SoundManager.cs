using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; set; }
	public static Action<bool> OnChangeState { get; set; }

	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	private float _comboPitch;
	[SerializeField]
	private float _comboTime = 1f;
	[SerializeField]
	private int _comboCount = 3;

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

		float pitchStep = (HighPitchRange - LowPitchRange) / _comboCount;

		EffectsSource.pitch = Mathf.Clamp(_comboPitch, LowPitchRange, HighPitchRange);
		EffectsSource.clip = clip;
		EffectsSource.Play();

		_comboPitch += pitchStep;

		CancelInvoke();
		Invoke(nameof(ReducePitch), _comboTime);
	}

	private void ReducePitch()
    {
		_comboPitch = LowPitchRange;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private AudioClip _takeAudio;
    [SerializeField] private int BonusCount = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TakeBonus();
        }
    }

    private void TakeBonus()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.ComboSoundEffect(_takeAudio);

        for (int i = 0; i < BonusCount; i++)
        {
            ScoreCounter.Instance.IncreaseCurrentScore();
        }
    }
}

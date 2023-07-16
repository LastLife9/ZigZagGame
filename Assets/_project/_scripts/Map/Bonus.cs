using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] 
    private AudioClip _takeAudio;
    [SerializeField] 
    private int _bonusCount = 2;

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

        PopupManager.Instance.SpawnText(transform, "+" + _bonusCount.ToString());
        SoundManager.Instance.ComboSoundEffect(_takeAudio);

        for (int i = 0; i < _bonusCount; i++)
        {
            ScoreCounter.Instance.IncreaseCurrentScore();
        }
    }
}

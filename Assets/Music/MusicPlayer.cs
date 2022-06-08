using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> playlist;
    public AudioSource audioSource;
    public List<float> probs;
    public AudioClip currentClip;
    bool loop;

    private void Start()
    {
        probs.Capacity = playlist.Count;
        Play();
    }

    public void Play()
    {
        StartCoroutine(PlayPlaylist());

    }

    IEnumerator PlayPlaylist()
    {
        for(int i = 0; i < playlist.Count; i++)
        {
            audioSource.clip = playlist[i];

            yield return new WaitWhile(() => audioSource.isPlaying);
            audioSource.Play();
        }
    }

    public static int GetRouletteIndex(List<float> probs)
    {
        // add up the total of all probabilities to get the random range
        // we want to roll
        float total = 0;
        for (int i = 0; i < probs.Count; i++)
            total += probs[i];

        // roll the dice
        float dice = Random.Range(0, total);

        // step through th probabilities again, until our total has exceed the dice roll
        total = 0;
        for (int i = 0; i < probs.Count; i++)
        {
            total += probs[i];
            if (total > dice)
                return i;
        }

        // this should never happen
        return -1;
    }
}

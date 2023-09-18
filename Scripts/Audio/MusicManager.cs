using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    public float newClip;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= newClip + 1)
        {
            ShufflePlaylist();
            timer = 0;
        }
    }

    private void ShufflePlaylist()
    {
        int clipNum = Random.Range(0, clips.Length);
        
        // Play a random song from the list once the previous one has completed playing
        if (!source.isPlaying)
        {
            source.loop = true;
            source.PlayOneShot(clips[clipNum]);
        }

        newClip = clips[clipNum].length;
    }
}

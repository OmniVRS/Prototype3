using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

    //Audio Variables
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip kaboom;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) /*&& !audioSource.isPlaying*/)
        {
            audioSource.PlayOneShot(kaboom);
        }
    }
}

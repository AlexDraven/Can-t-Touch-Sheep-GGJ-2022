using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{

    [SerializeField]
    public AudioClip[] meees;
    public AudioSource meee;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!meee.isPlaying)
            {
                // Random Mee
                int index = Random.Range(0, meees.Length);
                meee.clip = meees[index];
                meee.Play();
            }
        }
    }
}

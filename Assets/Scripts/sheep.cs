using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{

    [SerializeField]
    private AudioSource meee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            meee.Play();
    }
}

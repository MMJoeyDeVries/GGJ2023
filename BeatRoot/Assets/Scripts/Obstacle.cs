
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  private AudioSource _audioSource;

  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag != "Player")
    {
      return;
    }

    Debug.Log("Collision!");

    _audioSource.Play();
  }
}

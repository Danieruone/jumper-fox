using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            SoundManager sound = GameObject.Find("Main Camera").GetComponent<SoundManager>();
            sound.camAudio.clip = sound.coin;
            sound.camAudio.volume = 0.5f;
            sound.camAudio.Play();
            collision.GetComponent<GameController>().coins++;
            Destroy(gameObject);
        }
    }
}

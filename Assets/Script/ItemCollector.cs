using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private Text cherriesText;

    private int cherries = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();
            cherries++;
            cherriesText.text = "Cherries:" + cherries;
        }    
    }
}

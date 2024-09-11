using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    AudioSource coinPickupSfx;
    [SerializeField] int coinPointValue = 100;
    bool wasCollected = false;

    private void Start() {
        coinPickupSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected){
            FindObjectOfType<GameSession>().AddToScore(coinPointValue);
            AudioSource.PlayClipAtPoint(coinPickupSfx.clip,Camera.main.transform.position);
            Destroy(gameObject);     
            wasCollected = true;                 
        }
    }
}

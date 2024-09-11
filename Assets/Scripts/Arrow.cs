using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 20f;
    Rigidbody2D arrowRigidBody;
    PlayerMovementScript player;
    float xSpeed;

    void Start()
    {
        arrowRigidBody = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovementScript>();
        xSpeed = player.transform.localScale.x*arrowSpeed;
    }

    
    void Update()
    {
        arrowRigidBody.velocity = new Vector2(xSpeed,0f);
        transform.localScale = new Vector2(-Mathf.Sign(xSpeed),1f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collided");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    

}

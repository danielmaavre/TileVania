using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    [SerializeField] float enemyMoveSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        enemyRigidBody.velocity = new Vector2(enemyMoveSpeed,0f);        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Ground"){
            enemyMoveSpeed = -enemyMoveSpeed;
            FlipEnemyFacing();
        }
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(enemyRigidBody.velocity.x),1f);
    }
}

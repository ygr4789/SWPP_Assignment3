using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;

    private Animator enemyAnimator;
    
    private float speed = 2.0f;
    public int hp;
    
    void Start()
    {
        gameManager = GameManager.Instance;
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.SetFloat("Speed_f", 0.5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            gameManager.UpdateLife(1);
            gameManager.UpdateEnemy();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            hp -= 1;
            if (hp <= 0)
            {
                gameManager.UpdateEnemy();
                gameManager.UpdateMoney(1);
                Destroy(gameObject);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private Animator animator;

    private GameObject target;

    [HideInInspector] public bool upgraded = false;
    private float detectRange = 5.0f;
    private float fireRate0 = 1.0f;
    private float fireRate1 = 0.3f;
    private Vector3 throwPos;

    void Start()
    {
        GameManager.Instance.playerManager = this;
        animator = player.GetComponent<Animator>();
        throwPos = player.transform.position + new Vector3(0, 0.5f, 0);
        StartCoroutine(Throw());
    }

    void Update()
    {
        if (target)
        {
            player.transform.forward = (target.transform.position - transform.position).normalized;
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float minDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < detectRange && distance < minDistance)
                {
                    minDistance = distance;
                    target = enemy;
                }
            }
        }
    }

    public void Upgrade()
    {
        upgraded = true;
        player.transform.Find("SF_Character_Farmer").gameObject.SetActive(false);
        player.transform.Find("SF_Character_Wrangler").gameObject.SetActive(true);
    }

    IEnumerator Throw()
    {
        while(true)
        {
            if (target)
            {
                animator.SetTrigger("Throw_t");
                yield return new WaitForSeconds(0.2f);
                Instantiate(projectile, throwPos, player.transform.rotation);
                yield return new WaitForSeconds(upgraded ? fireRate1 : fireRate0 - 0.2f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}

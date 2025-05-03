using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public bool isReleased = false;
    public bool isEnemy = false;
    public float speed = 5f;
    public int health = 3;
    public Slider healthSlider;
    public string affinity;
    public bool hasStopped = false;

    private Unit enemy;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReleased && !isEnemy && !hasStopped)
        {
            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (isReleased && isEnemy && !hasStopped)
        {
            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            enemy = collision.gameObject.GetComponent<Unit>();
            if (enemy.isEnemy != isEnemy)
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                hasStopped = true;
                if (!isEnemy)
                {
                    
                    ResolveDamage();
                }
            }
            else if (hasStopped)
            {
                rb.velocity = Vector2.zero;
                hasStopped = true;
            }
            else if (!hasStopped)
            {
                hasStopped = false; 
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ResolveDamage()
    {
        int selfDamage=0;
        int enemyDamage=0;
        string enemyAffinity = enemy.affinity;
        switch (affinity)
        {
            case "fire":
                switch (enemyAffinity)
                {
                    case "fire":
                        selfDamage = 1;
                        enemyDamage = 1;
                        break;
                    case "plant":
                        selfDamage = 1;
                        enemyDamage = 3;
                        break;
                    case "water":
                        selfDamage = 3;
                        enemyDamage = 1;
                        break;                       
                }
                break;
            case "plant":
                switch (enemyAffinity)
                {
                    case "fire":
                        selfDamage = 3;
                        enemyDamage = 1;
                        break;
                    case "plant":
                        selfDamage = 1;
                        enemyDamage = 1;
                        break;
                    case "water":
                        selfDamage = 1;
                        enemyDamage = 3;
                        break;
                }
                break;
            case "water":
                switch (enemyAffinity)
                {
                    case "fire":
                        selfDamage = 1;
                        enemyDamage = 3;
                        break;
                    case "plant":
                        selfDamage = 3;
                        enemyDamage = 1;
                        break;
                    case "water":
                        selfDamage = 1;
                        enemyDamage = 1;
                        break;
                }
                break;


        }
        
        enemy.Damage(enemyDamage);
        Damage(selfDamage);
        if(health > 0 && enemy.health>0)
        {
            Invoke("ResolveDamage", 1);
            
        }
    }
}

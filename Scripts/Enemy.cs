using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    private SpriteRenderer sr;
    private Color originalColor;
    public float flashTime;

    public GameObject dropGold;
    public GameObject dropHealthPotion;

    public int haha;


    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        haha = Random.Range(0,5);
    }
    // Update is called once per frame
    public void Update()
    {
        if(health <= 0)
        {
            if(haha == 0)Instantiate(dropHealthPotion,transform.position,Quaternion.identity);
            else Instantiate(dropGold,transform.position,Quaternion.identity);
        
                
          
            
            
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int hurtdamage)
    {
        health -= hurtdamage;
        FlashColor(flashTime);

    }

     void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }
    

    void ResetColor()
    {
        sr.color = originalColor;
    }
}

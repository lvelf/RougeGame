using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public int health;
    private SpriteRenderer sr;
    private Color originalColor;
    public float flashTime;

    public BoxCollider2D collider2D;

    public Text deadText;
   

    public float speed;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        HealBar.HealthCurrent = health;
        HealBar.HealthMax = health;

        deadText.enabled = false;
        //无敌时间
        //collider2D = GetComponent<BoxCollider2D>();
        //collider2D.enabled = false;
        //Invoke("Life",3f);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0)
        {
            transform.localScale = new Vector3(movement.x , 1 , 1);
        }

        anim.SetBool("idle",true);
        anim.SetBool("run",true);
        SwitchAnim();
      
      
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    
    void SwitchAnim()
    {
        anim.SetFloat("speed",movement.magnitude);
    }

     public void TakeDamage(int hurtdamage)
    {
        health -= hurtdamage;
        if(health < 0)health = 0;
        HealBar.HealthCurrent = health;
        FlashColor(flashTime);
          if(health <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(gameObject,0.5f);
            deadText.enabled = true;
        }
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

    public void Life()
    {
        collider2D.enabled = true;
    }
}

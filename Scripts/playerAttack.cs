using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAttack : MonoBehaviour
{
    public int damage;

    public Animator anim;
    public BoxCollider2D coll2D;

    public float time;

     public Text winnerText;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();

        
        winnerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            coll2D.enabled = true;
            anim.SetTrigger("Attack");
            StartCoroutine( disableHitBox() );
        }
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        if(other.gameObject.CompareTag("EndRoom"))
        {
            winnerText.enabled = true;
            Invoke("PutdowntheText",2f);
        }
    }

    public void PutdowntheText()
    {
        winnerText.enabled = false;
    }
}

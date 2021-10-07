using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBullet : MonoBehaviour
{

    public float speed;
    public int damage;
    public float interval = 3f;
    private float count = 0;


    private Rigidbody2D rd2D;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
        rd2D.velocity = transform.right * speed;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
       if(count >= interval)
        {
            count = 0;
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}

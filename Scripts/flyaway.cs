using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyaway : MonoBehaviour
{
    public int damage;
    public float speed;
    public int dir;
    public float interval = 0.4f;
    private float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        fly(dir);
        if(count >= interval)
        {
            count = 0;
            Destroy(this.gameObject);
        }
        
    }

    public void fly(int direction)
    {


        if(direction == -1)
        {
            this.transform.position += new Vector3((-1)*speed*Time.deltaTime,0,0);
        }
        if(direction == 0)
        {
            this.transform.position += new Vector3(0,speed*Time.deltaTime,0);
        }
        if(direction == 1)
        {
            this.transform.position += new Vector3(speed*Time.deltaTime,0,0);
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

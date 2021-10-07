using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMonsters : Enemy
{
    public float rushBSpeed;
    public float radius;
    private Transform playerTransform;
    // Start is called before the first frame update

    //怪物功能
    public float speed;
    public float startWaitTime; 
    private float waitTime;


    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;

    public void Start()
    {
        base.Start();
        //获取玩家位置

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //巡逻
        waitTime = startWaitTime;
        GameObject parent = this.transform.parent.gameObject;
        leftDownPos.localPosition = new Vector3(parent.transform.position.x - 9,parent.transform.position.y - 4,0);
        rightUpPos.localPosition = new Vector3(parent.transform.position.x + 9,parent.transform.position.y + 4,0);

       movePos.transform.localPosition = parent.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        //AI系统
        if(playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;

            if(distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,playerTransform.position,rushBSpeed * Time.deltaTime);

            }
        }

        //巡逻
        transform.localPosition = Vector2.MoveTowards(transform.localPosition , movePos.localPosition , speed * Time.deltaTime);

        if(Vector2.Distance(transform.localPosition,movePos.localPosition) < 0.1f )
        {
               if(waitTime <= 0)
           {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
           }
           else
            {
               waitTime -= Time.deltaTime;
            }
        }
    }

    //怪物其他功能
    Vector3 GetRandomPos()
   {
       Vector3 rndPos = new Vector3(  Random.Range(leftDownPos.localPosition.x,rightUpPos.localPosition.x)   ,   Random.Range(leftDownPos.localPosition.y,rightUpPos.localPosition.y) , 0   );

       return rndPos;
   }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<playercontroller>().TakeDamage(damage);
        }
    }
}

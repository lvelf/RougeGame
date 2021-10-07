using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firethebullet : MonoBehaviour
{
    public GameObject mybullet;
    Vector2 movement;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        
        if(MpBar.Mp_Current == 0)text.enabled = true;


        if(Input.GetButtonDown("Fireon"))
        {
            GameObject bullet = Instantiate(mybullet);
            bullet.transform.position = transform.position;
            bullet.GetComponent<flyaway>().dir = (int)movement.x;

            MpBar.Mp_Current -= 1;
            if(MpBar.Mp_Current < 0)MpBar.Mp_Current = 0;
        }
    }
}

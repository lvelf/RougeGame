using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstGun : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;
    public Transform muzzleTransform;

    private Vector3 mousePos;
    private Vector3 gunDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y,gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);

        //Fire on!!
        if(Input.GetMouseButtonDown(0))
        {

            //Create bullet 
            Instantiate(bullet,muzzleTransform.position , Quaternion.Euler(transform.eulerAngles));
            MpBar.Mp_Current -= 2;
            if(MpBar.Mp_Current < 0)MpBar.Mp_Current = 0;
        }
    }
}

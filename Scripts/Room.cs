using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Room : MonoBehaviour
{
    public GameObject doorLeft,doorRight,doorUp,doorDown;

    public bool roomLeft,roomRight,roomUP,roomDown;

    public int stepToStart;

    public Text text;

    public int doorNumber;
    // Start is called before the first frame update
    void Start()
    {

        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUP);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRoom(float xOffset,float yOffset)
    {   
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset ) + Mathf.Abs(transform.position.y / yOffset)   );

        text.text = stepToStart.ToString();

        if(roomUP)
            doorNumber++;
        if(roomDown)
            doorNumber++;
        if(roomLeft)
            doorNumber++;   
        if(roomRight)
            doorNumber++;      
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CameraContorller.instance.ChangeTarget(transform);

        }

    }

}

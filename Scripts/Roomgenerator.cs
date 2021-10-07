using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomgenerator : MonoBehaviour
{
    public enum Direction{ up,down,left,right };
    public Direction direction;


    [Header("敌人信息")]
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public int enemyNumber;
    public int bigEnemyNumber;

    [Header("房间信息")]
    public GameObject roomprefab;
    public int roomNumber;
    public Color startColor,endColor;
    private GameObject endRoom;




    [Header("位置信息")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public List<Room> rooms = new List<Room>();


    public int maxStep;
    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;


    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 0;i < roomNumber;i ++)
        {
            rooms.Add(   Instantiate(roomprefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>()  );

            //创造敌人
            enemyNumber = (int)Random.Range(5,8);
            bigEnemyNumber = (int)Random.Range(0,3);

            CreateEnemySpider(enemyNumber);
            CreateEnemyBat(enemyNumber);
            CreateEnemyBigMonster(bigEnemyNumber);

         

            //改变point的位置
            ChangePiontPos();
        }
        
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0].gameObject;

        foreach(var room in rooms)
        {
            //if(room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            //{
            //    endRoom = room.gameObject;

            //}

            SetupRoom(room , room.transform.position);

        }

        FindEndRoom();

        endRoom.GetComponent<SpriteRenderer>().color = endColor;


        endRoom.tag = "EndRoom";
      


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePiontPos()
    {
        do
        {
            direction = (Direction)Random.Range(0,4);

            switch(direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3( 0 , yOffset , 0 );
                break;

                case Direction.down:
                    generatorPoint.position += new Vector3( 0 , -yOffset , 0 );
                break;

                case Direction.left:
                    generatorPoint.position += new Vector3( -xOffset , 0 , 0 );
                break;

                case Direction.right:
                    generatorPoint.position += new Vector3( xOffset , 0 , 0 );
                break;
            }




        }while(Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));

    }


    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUP = Physics2D.OverlapCircle(roomPosition + new Vector3(0 , yOffset , 0) , 0.2f , roomLayer  );
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0 , -yOffset , 0) , 0.2f , roomLayer  );
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset , 0 , 0) , 0.2f , roomLayer  );
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset , 0 , 0) , 0.2f , roomLayer  );

        newRoom.UpdateRoom(xOffset,yOffset);

        switch(newRoom.doorNumber)
        {
            case 1:
                if(newRoom.roomUP) 
                    Instantiate(wallType.singleUp , roomPosition , Quaternion.identity);
                if(newRoom.roomDown) 
                    Instantiate(wallType.singleDown , roomPosition , Quaternion.identity);
                if(newRoom.roomLeft) 
                    Instantiate(wallType.singleLeft , roomPosition , Quaternion.identity);
                if(newRoom.roomRight) 
                    Instantiate(wallType.singleRight , roomPosition , Quaternion.identity);
                break;

            case 2:
                if(newRoom.roomUP && newRoom.roomDown)
                    Instantiate(wallType.doubleUD , roomPosition , Quaternion.identity);
                if(newRoom.roomUP && newRoom.roomLeft)
                    Instantiate(wallType.doubleUL , roomPosition , Quaternion.identity);
                if(newRoom.roomUP && newRoom.roomRight)
                    Instantiate(wallType.duobleUR , roomPosition , Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.doubleDL , roomPosition , Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.doubleDR , roomPosition , Quaternion.identity);
                if(newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR , roomPosition , Quaternion.identity);
                break;
            
            case 3:
                if(newRoom.roomUP && newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.TriUDL , roomPosition , Quaternion.identity);
                if(newRoom.roomUP && newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.TriUDR , roomPosition , Quaternion.identity);
                if(newRoom.roomUP && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.TriULR , roomPosition , Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.TriDLR , roomPosition , Quaternion.identity);
                break;

            case 4:
                    Instantiate(wallType.fourDoors , roomPosition , Quaternion.identity);
                    break;

        }

    }

    public void FindEndRoom()
    {

        //获得最大值
        for(int i = 0;i < rooms.Count;i ++)
        {
            if(rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }


        //获得次大值和最大值房间
        foreach(var room in rooms)
        {
            if(room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if(room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }

        for(int i = 0;i < farRooms.Count;i ++)
        {
            if(farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }

         for(int i = 0;i < lessFarRooms.Count;i ++)
        {
            if( lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add( lessFarRooms[i]);
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[ Random.Range(0 , oneWayRooms.Count )];
        }
        else
        {
            endRoom = farRooms[ Random.Range(0 , farRooms.Count)]; 
        }
    }

    public void CreateEnemySpider(int tot)
    {
        for(int i = 0;i < tot;i ++)
        {   
            GameObject myEnemy =Instantiate(Enemy1,generatorPoint.position,Quaternion.identity);
   
        }

    }

    public void CreateEnemyBat(int tot)
    {
        for(int i = 0;i < tot;i ++)
        {   
            GameObject myEnemy =Instantiate(Enemy2,generatorPoint.position,Quaternion.identity);
   
        }
    }

    public void CreateEnemyBigMonster(int tot)
    {
        for(int i = 0;i < tot;i ++)
        {   
            GameObject myEnemy =Instantiate(Enemy3,generatorPoint.position,Quaternion.identity);
   
        }
    }
   
}

[System.Serializable]

public class WallType
{
    public GameObject singleLeft,singleRight,singleUp,singleDown;
    public GameObject doubleUD,doubleUL,duobleUR,doubleDL,doubleDR,doubleLR;
    public GameObject TriUDL,TriULR,TriDLR,TriUDR;
    public GameObject fourDoors;

}

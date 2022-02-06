using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject NoDoorRoom; //Legit just flat plane
    public GameObject StandardWall; //Wall for the edges 
    public GameObject StandardDoorway; //Doorways

    public GameObject ParentObject;

    private int RoomsToCreate; //keep track of how many rooms need to be created
    private Vector3 LastPlacedRoom; //keep track of the last placed room so that the next placed room is based off of this one
    public List<Vector3> AreasPlaced; //keep track of every area that is placed -> necessary for walls

    private float TimeToWait = 0.1f; //float used for the coroutines 

    // Start is called before the first frame update
    void Start()
    {
        RoomsToCreate = Random.Range(10, 100); //initialize how many rooms need to be created, useful for further/more complex levels etc.
        AreasPlaced = new List<Vector3>(); //initialize area placement list
        GameObject FlatRoom = Instantiate(NoDoorRoom, new Vector3(0, 0, 0), Quaternion.identity); //instantiate first room at 0,0,0
        LastPlacedRoom = NoDoorRoom.transform.position; //ensure vector is updated to this position so all rooms can be based off of it
        AreasPlaced.Add(LastPlacedRoom); //add this position into list
        FlatRoom.transform.parent = ParentObject.transform;
        //CreateMaze();
        StartCoroutine(CreateMaze());
    }

    // -----------------------------
    // Commented out method -> this one is for instant generation
    // -----------------------------

    /*    void CreateMaze()
        {
            while (RoomsToCreate > 0)
            {
                bool newX = Random.Range(0, 2) == 0 ? true : false;
                if (newX)
                {
                    int newSpot = Random.Range(0, 2);
                    switch (newSpot)
                    {
                        case 0:
                            Vector3 NewPos = new Vector3(LastPlacedRoom.x + 10, LastPlacedRoom.y, LastPlacedRoom.z);
                            if (CheckPos(NewPos))
                            {
                                break;
                            }
                            Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                            Instantiate(StandardDoorway, new Vector3(NewPos.x - 5, NewPos.y, NewPos.z), Quaternion.identity);
                            LastPlacedRoom = NewPos;
                            AreasPlaced.Add(LastPlacedRoom);
                            break;
                        case 1:
                            NewPos = new Vector3(LastPlacedRoom.x - 10, LastPlacedRoom.y, LastPlacedRoom.z);
                            if (CheckPos(NewPos))
                            {
                                break;
                            }
                            Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                            Instantiate(StandardDoorway, new Vector3(NewPos.x + 5, NewPos.y, NewPos.z), Quaternion.identity);
                            LastPlacedRoom = NewPos;
                            AreasPlaced.Add(LastPlacedRoom);
                            break;
                    }
                }
                else
                {
                    int newSpot = Random.Range(0, 2);
                    switch (newSpot)
                    {
                        case 0:
                            Vector3 NewPos = new Vector3(LastPlacedRoom.x, LastPlacedRoom.y, LastPlacedRoom.z + 10);
                            if (CheckPos(NewPos))
                            {
                                break;
                            }
                            Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                            GameObject Wall = Instantiate(StandardDoorway, new Vector3(NewPos.x, NewPos.y, NewPos.z - 5), Quaternion.identity);
                            Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                            LastPlacedRoom = NewPos;
                            AreasPlaced.Add(LastPlacedRoom);
                            break;
                        case 1:
                            NewPos = new Vector3(LastPlacedRoom.x, LastPlacedRoom.y, LastPlacedRoom.z - 10);
                            if (CheckPos(NewPos))
                            {
                                break;
                            }
                            Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                            Wall = Instantiate(StandardDoorway, new Vector3(NewPos.x, NewPos.y, NewPos.z + 5), Quaternion.identity);
                            Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                            LastPlacedRoom = NewPos;
                            AreasPlaced.Add(LastPlacedRoom);
                            break;
                    }
                }

                RoomsToCreate--;
            }

            CreateWalls();
        }*/

    // -----------------------------
    // IEnumerator method -> generation is happening on run time but can see each placement 
    // -----------------------------

    IEnumerator CreateMaze()
    {
        while (RoomsToCreate > 0)
        {
            bool Placed = false;
            bool newX = Random.Range(0, 2) == 0 ? true : false;
            if (newX)
            {
                int newSpot = Random.Range(0, 2);
                switch (newSpot)
                {
                    case 0:
                        Vector3 NewPos = new Vector3(LastPlacedRoom.x + 10, LastPlacedRoom.y, LastPlacedRoom.z);
                        if (CheckPos(NewPos))
                        {
                            break;
                        }
                        GameObject FlatRoom = Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                        GameObject Door = Instantiate(StandardDoorway, new Vector3(NewPos.x - 5, NewPos.y, NewPos.z), Quaternion.identity);
                        LastPlacedRoom = NewPos;
                        AreasPlaced.Add(LastPlacedRoom);
                        FlatRoom.transform.parent = ParentObject.transform;
                        Door.transform.parent = ParentObject.transform;
                        Placed = true;
                        break;
                    case 1:
                        NewPos = new Vector3(LastPlacedRoom.x - 10, LastPlacedRoom.y, LastPlacedRoom.z);
                        if (CheckPos(NewPos))
                        {
                            break;
                        }
                        FlatRoom = Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                        Door = Instantiate(StandardDoorway, new Vector3(NewPos.x + 5, NewPos.y, NewPos.z), Quaternion.identity);
                        LastPlacedRoom = NewPos;
                        AreasPlaced.Add(LastPlacedRoom);
                        FlatRoom.transform.parent = ParentObject.transform;
                        Door.transform.parent = ParentObject.transform;
                        Placed = true;
                        break;
                }
            }
            else
            {
                int newSpot = Random.Range(0, 2);
                switch (newSpot)
                {
                    case 0:
                        Vector3 NewPos = new Vector3(LastPlacedRoom.x, LastPlacedRoom.y, LastPlacedRoom.z + 10);
                        if (CheckPos(NewPos))
                        {
                            break;
                        }
                        GameObject FlatRoom = Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                        GameObject Wall = Instantiate(StandardDoorway, new Vector3(NewPos.x, NewPos.y, NewPos.z - 5), Quaternion.identity);
                        Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                        LastPlacedRoom = NewPos;
                        AreasPlaced.Add(LastPlacedRoom);
                        FlatRoom.transform.parent = ParentObject.transform;
                        Wall.transform.parent = ParentObject.transform;
                        Placed = true;
                        break;
                    case 1:
                        NewPos = new Vector3(LastPlacedRoom.x, LastPlacedRoom.y, LastPlacedRoom.z - 10);
                        if (CheckPos(NewPos))
                        {
                            break;
                        }
                        FlatRoom = Instantiate(NoDoorRoom, NewPos, Quaternion.identity);
                        Wall = Instantiate(StandardDoorway, new Vector3(NewPos.x, NewPos.y, NewPos.z + 5), Quaternion.identity);
                        Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                        LastPlacedRoom = NewPos;
                        AreasPlaced.Add(LastPlacedRoom);
                        FlatRoom.transform.parent = ParentObject.transform;
                        Wall.transform.parent = ParentObject.transform;
                        Placed = true;
                        break;
                }
            }
            if (Placed)
            {
                yield return new WaitForSeconds(TimeToWait);
            }

            RoomsToCreate--;
        }

        StartCoroutine(CreateWalls());
    }

    // -----------------------------
    // Commented out method -> this one is for instant generation
    // -----------------------------

    /*    void CreateWalls()
        {
            for (int i = 0; i < AreasPlaced.Count; i++)
            {
                if (!CheckPos(new Vector3(AreasPlaced[i].x - 10, AreasPlaced[i].y, AreasPlaced[i].z)))
                {
                    Instantiate(StandardWall, new Vector3(AreasPlaced[i].x - 5, AreasPlaced[i].y, AreasPlaced[i].z), Quaternion.identity);
                }
                if (!CheckPos(new Vector3(AreasPlaced[i].x + 10, AreasPlaced[i].y, AreasPlaced[i].z)))
                {
                    Instantiate(StandardWall, new Vector3(AreasPlaced[i].x + 5, AreasPlaced[i].y, AreasPlaced[i].z), Quaternion.identity);
                }
                if (!CheckPos(new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z - 10)))
                {
                    GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z - 5), Quaternion.identity);
                    Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
                if (!CheckPos(new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z + 10)))
                {
                    GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z + 5), Quaternion.identity);
                    Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
            }
        }*/

    // -----------------------------
    // IEnumerator method -> generation is happening on run time but can see each placement 
    // -----------------------------

    IEnumerator CreateWalls()
    {
        for (int i = 0; i < AreasPlaced.Count; i++)
        {
            bool Placed = false;
            if (!CheckPos(new Vector3(AreasPlaced[i].x - 10, AreasPlaced[i].y, AreasPlaced[i].z)))
            {
                GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x - 5, AreasPlaced[i].y, AreasPlaced[i].z), Quaternion.identity);
                Wall.transform.parent = ParentObject.transform;
                Placed = true;
            }
            if (!CheckPos(new Vector3(AreasPlaced[i].x + 10, AreasPlaced[i].y, AreasPlaced[i].z)))
            {
                GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x + 5, AreasPlaced[i].y, AreasPlaced[i].z), Quaternion.identity);
                Wall.transform.parent = ParentObject.transform;
                Placed = true;
            }
            if (!CheckPos(new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z - 10)))
            {
                GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z - 5), Quaternion.identity);
                Wall.transform.parent = ParentObject.transform;
                Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                Placed = true;
            }
            if (!CheckPos(new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z + 10)))
            {
                GameObject Wall = Instantiate(StandardWall, new Vector3(AreasPlaced[i].x, AreasPlaced[i].y, AreasPlaced[i].z + 5), Quaternion.identity);
                Wall.transform.parent = ParentObject.transform;
                Wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                Placed = true;
            }
            if (Placed)
            {
                yield return new WaitForSeconds(TimeToWait);
            }
        }
    }

    // Various 'valid' checks are used to see if the tile needs to be placed or not
    bool CheckPos(Vector3 PositionToCheck)
    {
        for (int i = 0; i < AreasPlaced.Count; i++)
        {
            if (AreasPlaced[i] == PositionToCheck)
            {
                return true;
            }
        }

        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGridMovement : MonoBehaviour
{

    [SerializeField] AnimationController aController;
    [SerializeField] Grid gameGrid;

    public Tile currentTile;
    public bool isMoving;
    //public Tile currentTile;
    private int stepNum = 0;
    //Vector3 moveDir = new Vector3();
    List<Vector3> path = new List<Vector3>();
    public float speed = 0.2f;

    //public float elevation = 0.05f;




    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;

        // GetComponent<AnimationController>();

        //Invoke("Test", 2);

    }

    void SetTile(Tile location)
    {
        currentTile = location;

    }

    public void MoveToDestination(Tile dest)
    {
        MoveCharacter(currentTile, dest, 100);//100=range, to be implemented later
    }

    void MoveCharacter(Tile start, Tile end, int range)
    {
        (bool success, List<Tile> path) = gameGrid.MovePath(start, end, range);


        if (success)
        {
            Debug.Log("Found Path!");
        }
        else
        {
            Debug.Log("No Path Found");
        }

        MoveTiles(path);
    }

    // test movement
    void Test()
    {
        ///////////////////////////
        //Starter Path
        CubeIndex firstCube, lastCube;
        //bool success;
        //List<Tile> path = new List<Tile>();

        firstCube = new CubeIndex(0, 0, 0);
        lastCube = new CubeIndex(-1, 2, -1);

        

        (bool success, List<Tile> path) = gameGrid.MovePath(firstCube, lastCube, 4);


        if (success)
        {
            Debug.Log("Found Path!");
        } else
        {
            Debug.Log("No Path Found");
        }

        MoveTiles(path);

    }


    // Update is called once per frame
    void Update()
    {


        //Only triggers after MoveTiles is called
        if(isMoving == true)
        {

            //Move along path with time
            float step = speed * Time.deltaTime;
            Vector3 tar = path[stepNum];
            tar.y = tar.y + .08f;
            transform.position = Vector3.MoveTowards(transform.position, tar, step);
            
            if ((transform.position - tar).magnitude < 0.01)
            {

                //Set next step for next destination
                stepNum++;
                
                //If it is the last step, end there.
                if (stepNum >= path.Count)
                {
                    //Turn off animation
                    aController.AnimateIdle();
                    //Turn off movement
                    isMoving = false;
                    path.Clear();
                    stepNum = 0;
                    //Rotate
                    //transform.LookAt(path[stepNum]);
                }
                else
                {
                    Vector3 tarb = path[stepNum];
                    tarb.y = tarb.y + .08f;
                    transform.LookAt(tarb);
                }

            }
            
        }        
    }

    public void MoveTiles(List<Tile> movePath)
    {
        isMoving = true; //Starts update function for position
        //Read the path in as vectors

        //Turn on movement animation
        //animator.SetBool("move", true);
        aController.AnimateMove();

        path = new List<Vector3>();
        for(int ind = 0; ind < movePath.Count; ind++)
        {
            path.Add(movePath[ind].transform.position);
        }

        SetTile(movePath[movePath.Count-1]);
        
    }

}

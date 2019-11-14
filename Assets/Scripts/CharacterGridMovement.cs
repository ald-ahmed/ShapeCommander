using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGridMovement : MonoBehaviour
{

    [SerializeField] AnimationController aController;
    [SerializeField] Grid gameGrid;

    public bool isMoving;
    //public Tile currentTile;
    public int stepNum = 0;
    //Vector3 moveDir = new Vector3();
    List<Vector3> path = new List<Vector3>();
    public float speed = 0.2f;

    //public float elevation = 0.05f;

    


    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;


        ///////////////////////////
        //Starter Path
        CubeIndex nextCube;
        List<Tile> starterPath = new List<Tile>();
        
        nextCube = new CubeIndex(0,0,0);
        starterPath.Add(gameGrid.TileAt(nextCube));


        nextCube = new CubeIndex(1, 0, -1);
        starterPath.Add(gameGrid.TileAt(nextCube));


        nextCube = new CubeIndex(2, 0, -2);
        starterPath.Add(gameGrid.TileAt(nextCube));


        nextCube = new CubeIndex(1, 1, -2);
        starterPath.Add(gameGrid.TileAt(nextCube));


        nextCube = new CubeIndex(0, 2, -2);
        starterPath.Add(gameGrid.TileAt(nextCube));


        nextCube = new CubeIndex(-1, 2, -1);
        starterPath.Add(gameGrid.TileAt(nextCube));
 

       /* nextCube = new CubeIndex(-2, 2, 0);
        starterPath.Add(gameGrid.TileAt(nextCube));
        if (gameGrid.TileAt(nextCube) != null)
        {
            Debug.Log("Not null in hereg");
        }

        nextCube = new CubeIndex(0, 0, 0);
        starterPath.Add(gameGrid.TileAt(nextCube));
        if (gameGrid.TileAt(nextCube) != null)
        {
            Debug.Log("Not null in hereh");
        }*/
        Debug.Log("starterPath.length: " + starterPath.Count);
       // MoveTiles(starterPath);//for testing
    }

    public void AddToPath(Tile t)
    {
        Vector3 tar = t.transform.position;
        tar.y = tar.y + .1f;
        path.Add(tar);
    }


    // Update is called once per frame
    void Update()
    {
        //Only triggers after MoveTiles is called
        if(isMoving == true)
        {
            //Move along path with time
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, path[stepNum], step); //+new Vector3(0,elevation,0)

            if ((transform.position - path[stepNum]).magnitude < 0.01)
            {

                //Set next step for next destination
                stepNum++;
                //Rotate
                transform.LookAt(path[stepNum]);
                //If it is the last step, end there.
                if (stepNum >= path.Count-1)
                {
                    //Turn off animation
                    //aController.AnimateIdle();
                    //Turn off movement
                    path.Clear();
                    isMoving = false;
                    //
                    
                }
            }
        }        
    }
    public void Navigate()
    {
        isMoving = true;
        Debug.Log("moving");
    }

    public void MoveTiles(List<Tile> movePath)
    {
       
        //Read the path in as vectors

        //Turn on movement animation
        //animator.SetBool("move", true);
        //aController.AnimateMove();

        path = new List<Vector3>();
        for(int ind = 0; ind < movePath.Count; ind++)
        {
            Vector3 tar = movePath[ind].transform.position;
            tar.y = tar.y + .1f;
            path.Add(tar);
        }
        isMoving = true; //Starts update function for position
       
    }

}

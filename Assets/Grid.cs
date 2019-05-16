using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * transform.position gives u the center of the world, aka the center position as a reference point to start with.
 *vector3.right means new Vector(1,0,0); this means that vector 3.right is just a fancy way of writing that vector 
 * 
 */

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;//size of the grid, the area the grid is going to cover
    public float nodeRadius;//size of each node, each box of the grid
    Node[,] grid;
     float nodeDiameter;//diameter of the node
    int gridSizeX, gridSizeY;//for calculating how many nodes we can fit onto each side

    //how many nodes can we fit into our grid, the start method will do thid before the start of the program
     void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);//how many nodes can we fit along the x side
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//how many nodes can we fit along the y side
        CreateGrid();
    }
    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];//Node array with gridx and gridy as sizes 
        //transform.position - Vector3.right * gridWorldSize.x / 2------>gives the left edge of the world
        //Vector3.forward ----------> gives the z axis in 3D space  
        // - Vector3.forward * gridWorldSize.y/2 ----------> gives us the bottom left corner of the world when added with the statement above
        Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y/2;  
        //go through all the pos the nodes are going to be int to check whether they are walkable or not
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++)
            {
                //world point is defining a square
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                    Vector3.forward * (y * nodeDiameter + nodeRadius);
                //collision check for the points
                //bool is true if we dont collide with anything in the unwalkable mask
                //physics.checksphere returns true if there is a collision
                //if there is collision and it returns the value to be true, then make walkable false
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));
                //assigning values to the constructor of Node class
                grid[x, y] = new Node(walkable, worldPoint,x,y);
                    
            }
        }
    }

    //No idea of whats going on in this method 
    //try to understand it first before moving forward to the next steps
    //not using array bcz we dont know how many nodes are going to surround that node 
    //
    //where is this node on our grid for that we can ediyt the constructor of the ndoe class and let it keep track of the main node
    //qith the x and y ints in the constructor 
    public List<Node> GetNeighbours (Node node) {
        //creating a list of nodes for the neighbours
        List<Node> neighbours = new List<Node>();
        //the loop searches in a 3 X 3 block 
        //when x and y both = 0, then we r in the enter of the block 
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //for skipping the center t=noed, because that node was already given to us '
                if (x == 0 && y == 0) {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                //checking if the node is actually iside the grid or not 
                //gridSizeX is the size of our array
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //for the position of the player
    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        //left 0
        //middle 0.5
        //right 1
        //same for the y axis bottom 0 and so onwards 
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        //so that it alwas remains between 0 and 1
        //spo that if the person is outside the world it doesnt give us error
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        //-1 so that were not outside of the int array
        //setting the size of the array 
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        //to return the size of the node 
      
        
        return grid[x,y];

    }

    public List<Node> path;

    //for drawing a cube on the grid, world
    private void OnDrawGizmos()
    {
        //the reason we have y is beacuse in y axis view our y arrow of the cude(Gizmo) represents the z axis
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        //if there is a grid
        if (grid != null) {

            //Node playerNode = NodeFromWorldPoint(player.position);

            //go through all the nodes in the grid
            foreach (Node n in grid) {
                //if there os no collision then draw the cube white and if there is a collision then draw it red
                //? means then (//pro if statement tool)
                //: means or set it to this (//also a pro if tool)
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null) {
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                }
                
                /* if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                
                */
                // Draw a Gizmos cube 
                Gizmos.DrawCube(n.worldposition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

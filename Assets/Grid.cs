using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*transform.position gives u the center of the world, aka the center position as a reference point to start with.
 *vector3.right means new Vector(1,0,0); this means that vector 3.right is just a fancy way of writing that vector 
 */

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;//size of the grid
    public float nodeRadius;//size of each node
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
        grid = new Node[gridSizeX, gridSizeY];//
        //transform.position - Vector3.right * gridWorldSize.x / 2------>gives the left edge of the world
        //Vector3.forward ----------> gives the z axis in 3D space  
        // - Vector3.forward * gridWorldSize.y/2 ----------> gives us the bottom left corner of the world when added with the statement above
        Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y/2;  
        //go through all the pos the nodes are going to be int to check whether they are walkable or not
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++)
            {
                //world point is defining a square//i think not sure 
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                    Vector3.forward * (y * nodeDiameter + nodeRadius);

                    
            }
        }
    }

    private void OnDrawGizmos()
    {
        //the reason we have y is beacuse in y axis view our y arrow of the cude(Gizmo) represents the z axis
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
    }
}

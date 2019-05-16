using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;//is there a wall or not
    public Vector3 worldposition;//what is the posi5tion on the grid

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node Parent;
    public Node(bool _walkable, Vector3 _worldPos, int _gridx, int _gridy) {
        walkable = _walkable;
        worldposition = _worldPos;
        gridX = _gridx;
        gridY = _gridy;
    }
    public int fCost {
        //don't actually need to assign a value to fCost, only get the value by adding both 
        get { 
        return gCost + hCost;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;//is there a wall or not
    public Vector3 worldposition;//what is the posi5tion on the grid

    public Node(bool _walkable, Vector3 _worldPos) {
        walkable = _walkable;
        worldposition = _worldPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *open and closed list, open list represents the nodes which need to be evaluated and closed list represents the lists which have already been evaluated
 * add the starting node to the open list 
 * loop:
 * temp variable which has the current node ( with the lowest F Cost) from the open list 
 * add current node to the closed list 
 * if current nd eis the target nide, path found the we exit out of the loop and then return the current node as the path
 * other wise go through the neighbouring nodes and check 
 * 
 * 
 * 
 */
public class PathFinding : MonoBehaviour
{

    public Transform seeker, Target;
    //to get a component attatched to the game object, through the code
    //were getting the game object of the Grid class, probably  the gizmos for the location of the objects 
    Grid grid;
    //awake() for before the game starts 
    private void Awake()
    {
        grid = GetComponent<Grid>(); 

    }
    private void Update()
    {
        FindPath(seeker.position, Target.position);
    }
    void FindPath(Vector3 StartPosition, Vector3 TargetPosition) {
        //convert the world position into nodes
        Node startNode = grid.NodeFromWorldPoint(StartPosition);
        Node targetNode = grid.NodeFromWorldPoint(TargetPosition);

        List<Node> openSet = new List<Node>();
        //hashset dso that the last element is the smallest onr 
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Node CurrentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                //if the f cost of the node in the array is less then current node the set the current node to openset[i]
                //and if the fcost are the same then look at the hcost and which ever one has smaller then set it to openset[i]
                if (openSet[i].fCost < CurrentNode.fCost || openSet[i].fCost == CurrentNode.fCost && openSet[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = openSet[i];
                }

            }
            //removing the checked node from the open node and adding it to the closed set //replace set with list 
            openSet.Remove(CurrentNode);
            closedSet.Add(CurrentNode);
            if (CurrentNode == targetNode) {
                RetracePath(startNode, targetNode);
            return;
            }
            //check if the neighbour is not walkable or if it is in the closed list the skip onto the next node 
            foreach (Node neighbour in grid.GetNeighbours(CurrentNode)) {
                //check if the neighbour is not walkable or if it is in the closed list 
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }
                //for gettign the distance between the current Node and the neghbours
                int CostFOrMOvementToNewNode = CurrentNode.gCost + GetDistance(CurrentNode , neighbour);
                if (CostFOrMOvementToNewNode < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = CostFOrMOvementToNewNode;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    //want to set the parnet of the node to the current node and the neighbour of the nodes are going to be their children
                    neighbour.Parent = CurrentNode;

                    if (!openSet.Contains(neighbour)) {
                        openSet.Add(neighbour);
                    }

                }
            }
            void RetracePath(Node StartNode, Node EndNode) {
                List<Node> path = new List<Node>();
                Node currentNode = EndNode;
                while (currentNode != startNode) {
                    path.Add(currentNode);
                    currentNode = currentNode.Parent;                                       
                }
                path.Reverse();
                grid.path = path;
            }
        }

    }
    //working on this part 
    /*
     * 
     * 
     */ 
     //for getting the diatance between two nodes 
    int GetDistance(Node nodeA, Node nodeB) {
        //gridX is the point on the world// int other words the node number on the world it think  
        int DistX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int DistY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (DistX > DistY) { 
            return DistY * 14 + 10 * (DistX - DistY);
        }
        else {
            return DistX * 14 + 10 * (DistY - DistX);
        }
    }
}

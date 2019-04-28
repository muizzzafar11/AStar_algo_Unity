 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{

    public LayerMask hitLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if the player has left clicked
        {
            Vector3 mouse = Input.mousePosition;//get the mouse position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);//cats a ray to where the mouse is
            RaycastHit hit;//stores the position where the ray hits
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayer))//if the raycast doesn't hit a wall
            {
                this.transform.position = hit.point;//move the traget to the mouse position
            }


        }
        
    }
}

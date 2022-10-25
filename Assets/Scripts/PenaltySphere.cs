using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltySphere : MonoBehaviour
{
    //hit object
    private RaycastHit hitObject;
    //local position of the hit point relative to the hit object
    private Vector3 localHitPoint;
    private float normalFactor=0.1f;

    private void Update()
    {
        //at each frame, convert the local space position of the penalty object to a world space position, whithout the two transformation,
        //the penalty object will stay at the same place (the world space coordinate of the hit point ) 
        transform.position = hitObject.transform.TransformPoint(localHitPoint);
        //rotation
        //transform.LookAt(hitObject.collider.transform);
       transform.rotation=hitObject.transform.rotation;

      // transform.Rotate(90, 0,0, Space.Self);

    }

    //set the value of the hitted Object and the local hit point ()
    public void setRaycastHit(RaycastHit hit1)
    {
        hitObject=hit1;
        localHitPoint=hitObject.transform.InverseTransformPoint(hitObject.point+hitObject.normal*normalFactor);
    }
public Vector3 getLocalHitPoint(){
    return localHitPoint;
}

}

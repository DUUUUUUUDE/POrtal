using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {

    public Transform PickUpPoint;
    public LayerMask PickUpObjectLayer;
    public float PickUpDist;
    public float ThrowForce;

    public Transform Obj;

    public void TakeObj ()
    {
        if (!Obj)
            PickUp();
    }

    public void ReleaseObj ()
    {
        if (Obj)
            Release();

    }

    public void ThrowOBJ ()
    {
        Debug.Log("X");

        Obj.GetComponent<Rigidbody>().isKinematic = false;
        Obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * ThrowForce);
        Obj.parent = null;
        Obj = null;


        if (MoveObjCO != null)
        {
            StopCoroutine(MoveObjCO);
        }
    }

    void PickUp ()
    {

        RaycastHit hit;
        Ray newRay = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        if (Physics.Raycast (newRay,out hit, 4 , PickUpObjectLayer))
        {
            hit.collider.GetComponent<Rigidbody>().isKinematic = true;
            Obj = hit.collider.transform;
            Obj.parent = PickUpPoint;

            MoveObjCO = StartCoroutine(MoveObject());
        }
    }

    void Release ()
    {
        Obj.GetComponent<Rigidbody>().isKinematic = false;
        Obj.parent = null;
        Obj = null;

        if (MoveObjCO != null)
        {
            StopCoroutine(MoveObjCO);
        }
    }

    bool onMoving;
    Coroutine MoveObjCO;
    float rotationSpeed;
    IEnumerator MoveObject ()
    {
        rotationSpeed = 0;
        while (Obj.position != PickUpPoint.position)
        {
            Obj.position = Vector3.MoveTowards(Obj.position, PickUpPoint.position,1);
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(Obj.transform.forward, PickUpPoint.forward,step ,0);
            Obj.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }
    }


}

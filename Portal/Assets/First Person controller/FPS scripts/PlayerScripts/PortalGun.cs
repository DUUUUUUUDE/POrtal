using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {

    public LayerMask portalgunCol;

    public Transform CheckPoolB;
    public Transform CheckPoolO;

    public LayerMask BackColMask;
    public LayerMask AllMask;

    public Transform PortalB;
    public Transform PortalO;

    bool blueOk;
    bool orangeOk;

    public void CheckPortal (bool mouse1)
    {
        if (!mouse1)
        {

            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit, 100 ,portalgunCol))
            {
                PortalO.position = hit.point;
                PortalO.forward = hit.normal;
            }

            if (CheckSpheres(GetPoints(CheckPoolO)))
            {
                PortalO.gameObject.SetActive(true);
                
                orangeOk = true;

                if (orangeOk && blueOk)
                {
                    //ACtivatePortals
                }
            }
            else
            {
                PortalO.gameObject.SetActive(false);
                orangeOk = false;
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, 100, portalgunCol))
            {
                PortalB.position = hit.point;
                PortalB.forward = hit.normal;
            }
            else
            {
                return;
            }


            if (CheckSpheres(GetPoints(CheckPoolB)))
            {
                PortalB.gameObject.SetActive(true);

                blueOk = true;

                if (orangeOk && blueOk)
                {
                    //ACtivatePortals
                }
            }
            else
            {
                PortalB.gameObject.SetActive(false);
                blueOk = false;
            }
        }
    }


    bool CheckSpheres (List <Vector3> portal)
    {
        foreach (Vector3 point in portal)
        {
            if (Physics.CheckSphere(point, 0.1f, BackColMask))
            {
                return false;
            }

            if (!Physics.CheckSphere(point, 0.1f, AllMask))
            {
                return false;
            }
        }

        return true;
    }

    List<Vector3> GetPoints(Transform pool)
    {
        List<Vector3> list = new List<Vector3>();
        foreach (Transform t in pool.GetComponentsInChildren<Transform>(true))
        {
            if (t.gameObject != pool.gameObject)
                list.Add(t.position);
        }
        return list;
    }
}

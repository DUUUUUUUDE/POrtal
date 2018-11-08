using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;
    public Camera otherCamera;

	private bool playerIsOverlapping = false;

    // Update is called once per frame
    void TPlayer()
    {
        Vector3 portalToPlayer = player.position - transform.position;

        float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
        rotationDiff += 180;

        player.eulerAngles = new Vector3(0, otherCamera.transform.eulerAngles.y,0);
        player.GetComponentInChildren<Camera>().transform.localEulerAngles = new Vector3(otherCamera.transform.eulerAngles.x, 0, 0);

        Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
        player.position = reciever.position + positionOffset + reciever.forward*1.5f;

        if ((reciever.forward.y > 0.1 || reciever.forward.y < -0.1) || (transform.forward.y > 0.1 || transform.forward.y < -0.1))
        {
            PlayerManager._Movement._Velocity += reciever.forward * 15;
        }

        PlayerManager._Movement._Velocity += reciever.forward * 5;
        PlayerManager._Movement._Velocity += Vector3.up * 2;



        deactivateTrigger = StartCoroutine(DeactivateTriggerCO());

    }

    void TPProp ()
    {

    }

    public void Reset()
    {
        if (onCoroutine)
        {
            StopCoroutine(deactivateTrigger);
            reciever.GetComponent<Collider>().enabled = true;
            onCoroutine = false;
        }
    }

    bool onCoroutine;
    Coroutine deactivateTrigger;
    IEnumerator DeactivateTriggerCO ()
    {
        onCoroutine = true;
        reciever.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        reciever.GetComponent<Collider>().enabled = true;
        onCoroutine = false;
    }

    void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
            TPlayer();
        }
        else if (other.tag == "Prop")
        {

        }
	}
    
}

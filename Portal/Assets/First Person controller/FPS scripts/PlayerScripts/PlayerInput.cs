using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [HideInInspector]
    public Vector3 _MovementForward;

    Vector3 GetForwardMovement()
    {
        float HMovement = Input.GetAxis("Horizontal");
        float VMovement = Input.GetAxis("Vertical");

        Vector3 moveDirSide = PlayerManager._Movement.transform.right * HMovement;
        Vector3 moveDirForward = PlayerManager._Movement.transform.forward * VMovement;

        Vector3 dir = new Vector3(moveDirSide.x + moveDirForward.x, 0, moveDirSide.z + moveDirForward.z);

        if (dir.magnitude > 1)
            return dir.normalized;
        else
            return dir;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (PlayerManager._PickUp.Obj==null)
                PlayerManager._PortalGun.CheckPortal(false);

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (PlayerManager._PickUp.Obj)
                PlayerManager._PickUp.ThrowOBJ();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            PlayerManager._PortalGun.CheckPortal(true);
        }

        if (Input.GetKeyDown (KeyCode.E))
        {
            PlayerManager._PickUp.TakeObj();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            PlayerManager._PickUp.ReleaseObj();
        }
        #region MOVEMENT

        //MOVEMENT VECTOR
        _MovementForward = GetForwardMovement();
        //2D MOVEMENT
        PlayerManager._Movement.SetMoveDirection(_MovementForward);

        // JUMP START
        if (Input.GetButtonDown("Jump"))
        {
            PlayerManager._Movement.StartJump();
        }
        // JUMP END
        if (Input.GetButtonUp("Jump"))
        {
            PlayerManager._Movement.EndJump();
        }

        //RUN START
        if (Input.GetButtonDown("Fire3"))
        {
            PlayerManager._Movement.Run();
        }
        //RUN END
        if (Input.GetButtonUp("Fire3"))
        {
            PlayerManager._Movement.Walk();
        }

        #endregion
    }
    
}

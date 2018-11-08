using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager _PlayerManager;

    public static PlayerMovement _Movement { get; set; }
    public static PortalGun _PortalGun { get; set; }
    public static PickUpObject _PickUp { get; set; }

    private void Awake()
    {
        if (_PlayerManager == null)
            _PlayerManager = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
            
    }

    private void Start()
    {
        _Movement = GetComponent<PlayerMovement>();
        _PortalGun = GetComponent<PortalGun>();
        _PickUp = GetComponent<PickUpObject>();
    }
}

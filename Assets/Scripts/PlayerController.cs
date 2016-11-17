using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	CursorLockMode wantedMode;
	
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	public GameObject cameraPlayer;
	
	public static CursorLockMode lockState; 
	
	public float mouseSensitivityX = 250f;		//sensibilité de la vue souris
	public float mouseSensitivityY = 250f;
	public float walkSpeed = 4f;				//Vitesse
	public float jumpForce = 220;				//Hauteur de Saut
	public LayerMask groundedMask;				//Définit le sol pour ne pas sauter alors que le joueur est dans le vide
												//Ne pas oublier de créer un Layout par exemple Ground pour le sol, et un collision pour sauter depuis des objets
												//Définir ces Layouts dans l'inspector

	Transform cameraT;							
	float verticalLookRotation;
	
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	
	bool grounded;
	
	// Apply requested cursor state													//Bloquer le curseur
	void SetCursorState ()
	{
		Cursor.lockState = wantedMode;
		// Hide cursor when locking
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
	
	void OnGUI ()
	{
		GUILayout.BeginVertical ();
		// Release cursor on escape keypress
		if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.lockState = wantedMode = CursorLockMode.None;
			
		switch (Cursor.lockState)
		{
			case CursorLockMode.None:Matrix4x4:
				GUILayout.Label ("Cursor is normal");
				if (GUILayout.Button ("Lock cursor"))
					wantedMode = CursorLockMode.Locked;
				if (GUILayout.Button ("Confine cursor"))
					wantedMode = CursorLockMode.Confined;
				break;
			case CursorLockMode.Confined:
				GUILayout.Label ("Cursor is confined");
				if (GUILayout.Button ("Lock cursor"))
					wantedMode = CursorLockMode.Locked;
				if (GUILayout.Button ("Release cursor"))
					wantedMode = CursorLockMode.None;
				break;
			case CursorLockMode.Locked:
				GUILayout.Label ("Cursor is locked");
				if (GUILayout.Button ("Unlock cursor"))
					wantedMode = CursorLockMode.None;
				if (GUILayout.Button ("Confine cursor"))
					wantedMode = CursorLockMode.Confined;
				break;
		}

		GUILayout.EndVertical ();

		SetCursorState ();
	}

	
	void Start () {																	//Camera souris
	
		cameraT = Camera.main.transform;
		
		if (!isLocalPlayer) //si ce perso ne m'appartient pas
			{
			Destroy(cameraPlayer);
			this.enabled = false;
			}
			
		wantedMode = CursorLockMode.Locked;
	}

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
	
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);										//Camera souris
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;												//
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);													//
		cameraT.localEulerAngles = Vector3.left * verticalLookRotation;														//
		
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;			//Mouvement
		Vector3 targetMoveAmount = moveDir * walkSpeed;																		//
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount, ref smoothMoveVelocity, .15f);							//
		
		if (Input.GetButtonDown("Jump")){																					//Saut
			if (grounded){																									//
				GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);												//
			}																												//
			
		}
		
		grounded = false;																									//Sur le sol ?
		Ray ray = new Ray(transform.position,-transform.up);																//
		RaycastHit hit;																										//
		
		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)){															//
			grounded = true;																								//
		}																													//

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFire();
        }
    }
	
	void FixedUpdate() {
		if (!isLocalPlayer)
        {
            return;
        }
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount));		//Mouvement
	}

	// This [Command] code is called on the Client …
    // … but it is run on the Server!
	[Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
		
		// Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);        
    }

    public override void OnStartLocalPlayer ()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
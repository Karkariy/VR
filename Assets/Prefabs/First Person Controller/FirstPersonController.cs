using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

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
	
	// Use this for initialization
	void Start () {								//Camera souris
		cameraT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
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
		
		
	}
	
	void FixedUpdate() {
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount));		//Mouvement
	}
}
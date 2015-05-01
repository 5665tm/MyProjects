using UnityEngine;
using System.Collections;

public class sPlayer: MonoBehaviour
{
	//public int acceleration;
	// Гравитация
	public float Gravity;
	// Ссылка на Character Controller
	private CharacterController characterController;
	public float JumpValue;
	private Vector3 vMotion;
	public float Speed;
	private Vector3 curImpulse;

	void Start()
	{
		// Запоминаем ссылку на Character Controller
		characterController = GetComponent<CharacterController>();
		// Изначально вектор перемещения равен нулю
		vMotion = Vector3.zero;
	}

	void FixedUpdate()
	{
		vMotion.x *= Time.deltaTime;
		vMotion.y *= Time.deltaTime;
		vMotion.z *= Time.deltaTime;
		vMotion.x = -(Input.GetAxis("Horizontal") * Speed);
		vMotion.z = -(Input.GetAxis("Vertical") * Speed);
		vMotion.y -= Gravity;
		
		// Перед применением метода Move добавим прыжок, если нажата клавиша "Jump"
		if( Input.GetButton("Jump") )
		//for (Input.touches touch;;)
		{
			//if (touch.phase == TouchPhase.Began)
			//{
				vMotion.y = JumpValue;
			//}
		}
		characterController.Move(vMotion);
	}
}


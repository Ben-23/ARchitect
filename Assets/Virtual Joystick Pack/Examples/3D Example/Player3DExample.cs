using UnityEngine;

public class Player3DExample : MonoBehaviour {

    public float moveSpeed = 1f;
    public Joystick joystick;

	void Update () 
	{
        Vector3 moveVector = (Vector3.right * 1 + Vector3.forward * 1);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            //transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }   
	}
}
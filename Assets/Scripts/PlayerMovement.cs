using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        // if (IsOwner)
        // {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(
                    transform.position.x, 
                    transform.position.y + moveSpeed * Time.deltaTime,
                    transform.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(
                    transform.position.x, 
                    transform.position.y - moveSpeed * Time.deltaTime,
                    transform.position.z);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime, 
                    transform.position.y,
                    transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(
                    transform.position.x + moveSpeed * Time.deltaTime, 
                    transform.position.y,
                    transform.position.z);
            }
        // }
    }
}
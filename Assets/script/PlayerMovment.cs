using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
public float moveSpeed = 3.5f;
public float rotateSpeed = 100.0f;
private Rigidbody rb;
// Start is called before the first frame update
void Start()
{
rb = GetComponent<Rigidbody>();
}
// Update is called once per frame
void FixedUpdate()
{
Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
Vector3 rotateY = new Vector3(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
if (movement != Vector3.zero)
{
rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
}
rb.MovePosition(rb.position + transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime +
transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
}
}
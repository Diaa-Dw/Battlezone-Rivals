using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
public float moveSpeed = 3.5f;
private Rigidbody rb;
// Start is called before the first frame update
void Start()
{
rb = GetComponent<Rigidbody>();
}
// Update is called once per frame
void Update()
{
rb.MovePosition(rb.position + transform.forward * Input.GetAxis
("Vertical") * moveSpeed * Time.deltaTime + transform.right *
Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
}
}
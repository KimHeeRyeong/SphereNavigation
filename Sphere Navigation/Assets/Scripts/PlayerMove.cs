using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public Transform planet;
    public float grav = 9.8f;
    public float speed = 10f;
    public LayerMask terrainLayer;
    private void FixedUpdate()
    {
        Vector3 gravityUp = (transform.position-planet.position).normalized;
        Physics.gravity = gravityUp * grav;
        Ray ray = new Ray(transform.position, -gravityUp);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, terrainLayer))
        {
            Vector3 normal = hit.normal.normalized;
            Quaternion target = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);

        }
        transform.position += (transform.forward * Input.GetAxis("Vertical")
        + transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * speed;
    }
}

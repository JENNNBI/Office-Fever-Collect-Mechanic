using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float lerpValue;
    [SerializeField] private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) &&
                hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                transform.position = Vector3.Lerp
                    (transform.position, new Vector3(hit.point.x, hit.point.y, hit.point.z), lerpValue * Time.deltaTime);
            }
        }
    }
}

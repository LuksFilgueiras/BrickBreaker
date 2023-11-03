using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed;

    float horizontalMovementLimit = 0f;

    void Start(){
        horizontalMovementLimit = GetCameraWidth() / 2 - transform.localScale.x / 2;
    }

    void Update(){
        Movement();
    }

    private void Movement(){
        float x = Input.GetAxisRaw("Horizontal");

        rigidBody2D.velocity = new Vector2(x * moveSpeed, 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -horizontalMovementLimit, horizontalMovementLimit), transform.position.y, 0);
    }

    private float GetCameraWidth(){
        float cameraHeight = 2 * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return cameraWidth;
    }

}

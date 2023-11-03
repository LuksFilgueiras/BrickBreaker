using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 1.5f;
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float horizontalLimit = 0f;
    [SerializeField] private float verticalLimit = 0f;
    [SerializeField] private bool hitted = false;

    [Header("Initialize Components")]
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Camera mainCamera;

    public Rigidbody2D GetRigidbody2D{
        get{return rigidBody2D;}
    }

    void Start(){
        horizontalLimit = GetCameraWidth() / 2 - transform.localScale.x / 2;
        verticalLimit = mainCamera.orthographicSize - transform.localScale.x / 2;

        verticalSpeed *= -1f;
    }

    void FixedUpdate(){
        Movement();
    }

    private void Movement(){
        if(transform.position.x >= horizontalLimit){
            ChangeDirectionHorizontal();
        }

        if(transform.position.x <= -horizontalLimit){
            ChangeDirectionHorizontal();
        }

        if(transform.position.y >= verticalLimit){
            ChangeDirectionVertical();
        }

        if(rigidBody2D.velocity.y < 0 && hitted){
            ChangeDirectionVertical();
            hitted = false;
        }

        if(transform.position.y <= -verticalLimit){
            transform.position = new Vector3(0, 0, 0);
        }

        rigidBody2D.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    public void ChangeDirectionVertical(){
        verticalSpeed *= -1f;
    }

    public void ChangeDirectionHorizontal(){
        horizontalSpeed *= -1f;
    }

    private float GetCameraWidth(){
        float cameraHeight = 2 * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return cameraWidth;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            hitted = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 startPoint;
    
    [Header("Life Attributtes")]
    [SerializeField] private int lifePoints = 3;
    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI lifeTextMeshPro;

    float horizontalMovementLimit = 0f;

    void Start(){
        startPoint = transform.position;
        horizontalMovementLimit = GetCameraWidth() / 2 - transform.localScale.x / 2;
    }

    void Update(){
        Movement();
        UpdateLifeUI();
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

    public void RemoveLife(int lifePoints){
        this.lifePoints -= lifePoints;

        if(this.lifePoints <= 0){
            gameController.DeathScreen(true);
            return;
        }
    }

    private void UpdateLifeUI(){
        lifeTextMeshPro.text = "LIFES: " + lifePoints.ToString();
    }
}

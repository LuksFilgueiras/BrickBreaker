using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public float brickSizeHorizontal = 0.6f;
    public float brickSizeVertical = 0.2f;

    [Header("Brick Health")]
    public int healthAmount = 2;
    public Color32[] visualHealthColors;
    public SpriteRenderer spriteRenderer;
    public Ball ball;

    void Start(){
        ball = FindObjectOfType<Ball>();
        spriteRenderer.color = visualHealthColors[healthAmount];
        transform.localScale = new Vector3(brickSizeHorizontal, brickSizeVertical, 0f);
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Ball"){
            healthAmount--;

            if(healthAmount <= 0){
                Destroy(gameObject);
            }

            spriteRenderer.color = visualHealthColors[healthAmount];
        }
    }
}
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
            Vector3 hit = col.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);

            if(angle == 0 || angle == 180){
                ball.ChangeDirectionVertical();
            }else{
                ball.ChangeDirectionHorizontal();
            }
            
            healthAmount--;

            if(healthAmount <= 0){
                Destroy(gameObject);
            }

            spriteRenderer.color = visualHealthColors[healthAmount];
        }
    }
}


// float angle = Vector3.Angle(hit.normal,Vector3.forward);

// if(Mathf.Approximately(angle, 0))// back
// if(Mathf.Approximately(angle, 180))// front
// if(Mathf.Approximately(angle, 90)){
//    Vector3 cross = Vector.Cross(Vector3.forward,hit.normal);
//    if(cross.y > 0) // Right
//     else // left
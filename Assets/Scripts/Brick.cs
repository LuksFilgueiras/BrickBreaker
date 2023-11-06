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

    public void TakeDamage(){
        healthAmount --;
        
        if(healthAmount <= 0){
            Destroy(gameObject);
        }

        spriteRenderer.color = visualHealthColors[healthAmount];
    }
}
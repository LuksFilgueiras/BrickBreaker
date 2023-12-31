using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed;
    
    [Header("Life Attributtes")]
    [SerializeField] private int lifePoints = 3;
    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI lifeTextMeshPro;

    float horizontalMovementLimit = 0f;

    void Start(){
        horizontalMovementLimit = ICamera.GetCameraWidth(mainCamera) / 2 - transform.localScale.x / 2;
    }

    void Update(){
        Movement();
        UpdateLifeUI();
    }

    private void Movement(){
        float x = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -horizontalMovementLimit, horizontalMovementLimit), transform.position.y, 0);
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

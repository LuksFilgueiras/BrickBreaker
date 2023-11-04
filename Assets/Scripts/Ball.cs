using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 1.5f;
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float horizontalLimit = 0f;
    [SerializeField] private float verticalLimit = 0f;
    [SerializeField] private Vector3 startPoint = Vector3.zero;
    [SerializeField] private bool freeBall = false;
    [SerializeField] private GameObject releaseTextGameobject; 

    [Header("Initialize Components")]
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Camera mainCamera;


    public Rigidbody2D GetRigidbody2D{
        get{return rigidBody2D;}
    }

    void Start(){
        horizontalLimit = ICamera.GetCameraWidth(mainCamera) / 2 - transform.localScale.x / 2;
        verticalLimit = mainCamera.orthographicSize - transform.localScale.x / 2;
        startPoint = transform.position;
    }

    void Update(){
        releaseTextGameobject.SetActive(!freeBall);

        if(!freeBall){
            if(Input.GetKeyDown(KeyCode.Space)){
                if(verticalSpeed < 0){
                    verticalSpeed *= -1f;
                }
                if(horizontalSpeed < 0){
                    horizontalSpeed *= -1f;
                }
                freeBall = true;
            }
        }

        Movement();
    }

    private void Movement(){
        if(rigidBody2D.velocity == Vector2.zero){
            ChangeDirectionHorizontal();
            ChangeDirectionVertical();
        }

        if(transform.position.x >= horizontalLimit){
            transform.position = new Vector3(horizontalLimit, transform.position.y, 0);
            ChangeDirectionHorizontal();
        }

        if(transform.position.x <= -horizontalLimit){
            transform.position = new Vector3(-horizontalLimit, transform.position.y, 0);
            ChangeDirectionHorizontal();
        }

        if(transform.position.y >= verticalLimit){
            transform.position = new Vector3(transform.position.x, verticalLimit, 0);
            ChangeDirectionVertical();
        }

        if(transform.position.y <= -verticalLimit){
            freeBall = false;
            FindObjectOfType<Player>().RemoveLife(1);
        }

        if(freeBall){
            rigidBody2D.velocity = new Vector3(horizontalSpeed, verticalSpeed);
        }else{
            transform.position = new Vector3(player.transform.position.x, startPoint.y);
        }
    }

    public void ChangeDirectionVertical(){
        verticalSpeed *= -1f;
    }

    public void ChangeDirectionHorizontal(){
        horizontalSpeed *= -1f;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            Vector2 hit = col.GetContact(0).point;
            hit = col.gameObject.GetComponent<Transform>().InverseTransformPoint(hit);

            if(hit.x > 0 && horizontalSpeed < 0){
                ChangeDirectionHorizontal();
            }
            if(hit.x < 0 && horizontalSpeed > 0){
                ChangeDirectionHorizontal();
            }

            ChangeDirectionVertical();
        }
        if(col.gameObject.tag == "Brick"){
            Vector3 hit = col.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);

            if(angle == 0f || angle == 180f){
                ChangeDirectionVertical();
            }
            else if(angle == 90f){
                ChangeDirectionHorizontal();
            }
            else{
                ChangeDirectionHorizontal();
                ChangeDirectionVertical();
            }
        }
    }
}

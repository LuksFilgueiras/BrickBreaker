using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int brickLinesAmount = 3;


    [Header("Wall positioning")]
    [SerializeField] private float horizontalWallOffset = 0.2f;
    [SerializeField] private float verticalWallOffset = 0.3f;
    [SerializeField] private float horizontalBrickGap = 0.05f;
    [SerializeField] private float verticalBrickGap = 0.05f;
    [SerializeField] private float brickWallHorizontalSize = 0f;
    [SerializeField] private float brickWallVerticalSize = 0f;


    void Start(){
        float brickAmount = (GetCameraWidth() - 2 * horizontalWallOffset) / (brickPrefab.brickSizeHorizontal + horizontalBrickGap);

        Vector3 wallStartPosition = new Vector3(-GetCameraWidth() / 2 + horizontalWallOffset, mainCamera.orthographicSize - verticalWallOffset, 0f);

        brickWallHorizontalSize = 0f;
        brickWallVerticalSize = 0f;
        for(int lines = 0; lines < brickLinesAmount; lines++){
            for(int i = 0; i < Mathf.FloorToInt(brickAmount); i++){
                Brick brickInstance = Instantiate(brickPrefab, transform);
                brickInstance.transform.position = new Vector3(wallStartPosition.x + brickWallHorizontalSize, wallStartPosition.y - brickWallVerticalSize, 0f);
                brickWallHorizontalSize += brickPrefab.brickSizeHorizontal + horizontalBrickGap;
            }

            brickWallHorizontalSize = 0f;
            brickWallVerticalSize += brickPrefab.brickSizeVertical + verticalBrickGap;
        }

        brickWallHorizontalSize = Mathf.FloorToInt(brickAmount) * (brickPrefab.brickSizeHorizontal + horizontalBrickGap);

        CenterBrickWall();
    }

    private float GetCameraWidth(){
        float cameraHeight = 2 * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return cameraWidth;
    }

    private void CenterBrickWall(){
        float horizontalOffset = (GetCameraWidth() - brickWallHorizontalSize - horizontalWallOffset) / 2;
        transform.position += new Vector3(horizontalOffset, 0, 0);
    }
}

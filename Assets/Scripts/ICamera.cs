using UnityEngine;

public interface ICamera 
{
    public static float GetCameraWidth(Camera camera){
        float cameraHeight = 2 * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        return cameraWidth;
    }
}
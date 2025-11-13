using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera; 
    [SerializeField] private float distance = 0.5f; 

    void LateUpdate()
    {
        if (targetCamera == null) return;

        // Position : devant la caméra
        transform.position = targetCamera.transform.position + targetCamera.transform.forward * distance;

        // Rotation : même orientation que la caméra
        transform.rotation = targetCamera.transform.rotation;
    }
}
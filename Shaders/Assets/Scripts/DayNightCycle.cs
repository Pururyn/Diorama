using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float dayLengthSeconds = 60f;
    [SerializeField] private float timeOfDay = 0.5f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public Gradient skyColorGradient;

    private Light directionalLight; 

    void Start()
    {
        directionalLight = GetComponent<Light>();   
        rotationSpeed = 360f / dayLengthSeconds; //degreees per seconds
    }

    
    void Update()
    {
        // increment time
        timeOfDay += Time.deltaTime / dayLengthSeconds; 
        timeOfDay %= 1f;

        // rotate light
        transform.rotation = Quaternion.Euler((timeOfDay * 360f) - 90,0,0); 

        // set Ambient Light Color
        RenderSettings.skybox.SetColor("_Tint", skyColorGradient.Evaluate(timeOfDay));
    }
}

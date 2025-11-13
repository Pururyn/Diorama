using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float dayLengthSeconds = 40f;
    [SerializeField, Range(0f, 1f)] private float timeOfDay = 0.5f;
    [SerializeField] public Gradient skyColorGradient;
    [SerializeField] private Gradient lightColorGradient;

    private Light directionalLight;
    private float rotationSpeed;

    void Start()
    {
        directionalLight = GetComponent<Light>();   
        rotationSpeed = 360f / dayLengthSeconds; //degreees per seconds
    }

    
    void Update()
    {
        if (directionalLight == null)
            return;

        // increment time
        timeOfDay += Time.deltaTime / dayLengthSeconds; 
        timeOfDay %= 1f;

        // rotate light
        transform.rotation = Quaternion.Euler((timeOfDay * 360f) - 90f, 0f ,0f);

        //update light color + intensity
        directionalLight.color = lightColorGradient.Evaluate(timeOfDay);

        //update sky color

        RenderSettings.skybox.SetColor("Tint", skyColorGradient.Evaluate(timeOfDay));

        Shader.SetGlobalVector("LightDirection", -directionalLight.transform.forward);
        Shader.SetGlobalColor("LightColor", directionalLight.color * directionalLight.intensity);

        float nightBlend = Mathf.Clamp01(Mathf.Abs(Vector3.Dot(Vector3.down, directionalLight.transform.forward)));
        Shader.SetGlobalFloat("NightBlend", nightBlend); 

        
    }
}

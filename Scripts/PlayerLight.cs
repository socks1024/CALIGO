using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private Rigidbody2D rb;
    private Light2D light2D;
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 8.0f;
    [SerializeField] private float baseIntensity = 2.0f;
    [SerializeField] private float intensityShakeRange = 0.2f;
    [SerializeField] private float intensityShakeTime = 0.2f;
    private float shakeTimer = 0.0f;
    private float shakeIntensity = 0.0f;
    private float startIntensity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PositionUpdate();
        IntensityUpdate();
    }

    private void PositionUpdate()
    {
        Vector2 v = player.position - transform.position;
        v.y *= 0.5f;
        rb.velocity = v * followSpeed;
    }

    private void IntensityUpdate()
    {
        if (shakeTimer > 0.0f)
        {
            shakeTimer -= Time.deltaTime;            
        }
        else
        {
            shakeIntensity = baseIntensity + (Random.Range(-1.0f, 1.0f) * intensityShakeRange);
            shakeTimer = intensityShakeTime;
            startIntensity = light2D.intensity;
        }

        light2D.intensity = Mathf.Lerp(startIntensity, shakeIntensity, (intensityShakeTime - shakeTimer) / intensityShakeTime);
    }
}

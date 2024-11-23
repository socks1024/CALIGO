using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private Rigidbody2D rb;
    private Light2D light2D;
    private CircleCollider2D circleCollider2D;

    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 8.0f;

    private float baseLightRadius = 0.0f;
    private float BaseLightRadius
    {
        get
        {
            if (oil >= oilUpperLimit)
            {
                return baseLightRadius;
            }
            else
            {
                return baseLightRadius * oil / oilUpperLimit;
            }
        }
        set { baseLightRadius = value; }
    }

    private float baseColliderRadius = 0.0f;
    private float BaseColliderRadius
    {
        get
        {
            if (oil >= oilUpperLimit)
            {
                return baseColliderRadius;
            }
            else
            {
                return baseColliderRadius * oil / oilUpperLimit;
            }
        }
        set { baseColliderRadius = value; }
    }

    #region oil

    private float oil = 0.0f;
    private float Oil
    {
        get { return oil; }
        set
        {
            if (value < 0.0f)
            {
                value = 0.0f;
            }
            
            oil = value;
        }
    }

    [SerializeField] private float oilUpperLimit = 100.0f;
    [SerializeField] private float oilInitialAmount = 1000.0f;

    [SerializeField] private float oilConsumeSpeed = 0.1f;
    [SerializeField] private float oilConsumeSpeedSkill = 0.5f;

    #endregion

    #region intensity shake property

    [SerializeField] private float baseIntensity = 2.0f;//基础强度
    [SerializeField] private float intensityShakeRange = 0.2f;
    [SerializeField] private float intensityShakeTime = 0.2f;
    private float shakeTimer = 0.0f;
    private float shakeIntensity = 0.0f;//最终强度
    private float startIntensity = 0.0f;//起始强度

    #endregion

    #region burn property

    private bool isBurning = false;
    private float currBurnPower = 1.0f;
    private float CurrBurnPower
    {
        get { return currBurnPower; }
        set
        { 
            currBurnPower = value;
            light2D.pointLightOuterRadius = BaseLightRadius * currBurnPower;
            circleCollider2D.radius = BaseColliderRadius * currBurnPower;
        }
    }

    [SerializeField] private float burnPower = 2.0f;

    [SerializeField] private float delayBeforeBurn = 5.0f;
    [SerializeField] private float delayAfterBurn = 5.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        light2D = GetComponent<Light2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        BaseLightRadius = light2D.pointLightOuterRadius;
        BaseColliderRadius = circleCollider2D.radius;

        Oil = oilInitialAmount;
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
        #region skill set radius

        if (Input.GetButtonDown("Burn") && !isBurning)
        {
            isBurning = true;
        }

        if (Input.GetButtonUp("Burn") && isBurning)
        {
            isBurning = false;
        }

        if (isBurning && currBurnPower < burnPower)
        {
            CurrBurnPower += (burnPower - 1.0f) / delayBeforeBurn * Time.fixedDeltaTime;
        }
        else if (!isBurning && currBurnPower > 1.0f)
        {
            CurrBurnPower -= (burnPower - 1.0f) / delayAfterBurn * Time.fixedDeltaTime;
        }
        else
        {
            CurrBurnPower = CurrBurnPower;
        }
        

        #endregion

        #region shake

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

        #endregion

        #region oil

        if (isBurning)
        {
            Oil -= oilConsumeSpeedSkill * Time.fixedDeltaTime;
        }
        else
        {
            Oil -= oilConsumeSpeed * Time.fixedDeltaTime;
        }

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;

    public float floatSpeed = 0.003f;

    public Vector3 floatDirection = new Vector3(0, 1, 0);

    public TextMeshProUGUI textMeshPro;

    RectTransform rectTransform;

    Color startColor;

    float timeElapsed = 0.0f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startColor = textMeshPro.color; 
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += floatDirection * floatSpeed * Time.fixedDeltaTime ;

        textMeshPro.color = new Color(startColor.r , startColor.g , startColor.b, 1 - (timeElapsed / timeToLive));
        
        if (timeElapsed > timeToLive) 
        {
            Destroy(gameObject);
        }   
    }
}

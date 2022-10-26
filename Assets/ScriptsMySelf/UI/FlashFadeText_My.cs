using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFadeText_My : MonoBehaviour
{
    [SerializeField] Text DangerText;
    [SerializeField] Color flashColor = new Color(0.7f, 0f, 0f, 0f);
    [SerializeField] float flashSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        DangerText = GetComponent<Text>();
    }
    public void Flash()
    {
        StopCoroutine("Fade");
        DangerText.color = flashColor;
        StartCoroutine("Fade");
    }
    IEnumerator Fade()
    {
        while(DangerText.color.a>0.01f)
        {
            DangerText.color = Color.Lerp(DangerText.color, Color.clear, flashSpeed*Time.deltaTime);
            yield return null;
        }
        DangerText.color = Color.clear;
    }
}

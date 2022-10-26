using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFadeImage_My : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.1f);//�任����ɫ
    [SerializeField] float flashSpeed = 5f;//�任�ٶ�
    // Start is called before the first frame update
    void Reset()
    {
        image = GetComponent<Image>();
    }

    public void Flash()
    {
        StopCoroutine("Fade");
        image.color = flashColor;
        StartCoroutine("Fade");
    }
    IEnumerator Fade()
    {
        //���ͼƬ��Alphaֵ����0.01
        while(image.color.a>0.01f)
        {
            //���ͼƬ��ɫ
            image.color = Color.Lerp(image.color, Color.clear, flashSpeed*Time.deltaTime);
            yield return null;
        }
        image.color = Color.clear;
        yield return null;
    }
}

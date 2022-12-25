using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text loading;
    [SerializeField] private MainMenu mainMenu;
    private float loadTime=3000f;
    void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        var t = DateTime.Now;
        while ((DateTime.Now - t).TotalMilliseconds < loadTime)
        {
            slider.value = (float)(DateTime.Now - t).TotalMilliseconds / loadTime;
            loading.text = "Загрузка "+((int)((DateTime.Now - t).TotalMilliseconds / loadTime * 100)).ToString()+"%";
            yield return null;
        }

        gameObject.SetActive(false);
        mainMenu.ShowMenu1();
    } 
}

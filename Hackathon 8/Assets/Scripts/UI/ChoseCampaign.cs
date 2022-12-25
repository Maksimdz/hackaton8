using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseCampaign : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button go;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Image campaign1;
    [SerializeField] private Image campaign2;
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(() =>
        {
            campaign1.gameObject.SetActive(false);
            campaign2.gameObject.SetActive(true);
        });
        button2.onClick.AddListener(() =>
        {
            campaign2.gameObject.SetActive(false);
            campaign1.gameObject.SetActive(true);
        });
        go.onClick.AddListener(() =>
        {
          mainMenu.ShowChooseHeroScreen();
          gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MainMenu mainMenu;
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            mainMenu.ShowMenu2();
        });
    }
}

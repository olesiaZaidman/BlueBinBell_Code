using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{

    [SerializeField] GameObject menu;
    private Button button;
    [SerializeField] Sprite _sleep;
    [SerializeField] Sprite _awake;

    bool isMenuOpen = false;

    void Start()
    {
        button = GetComponent<Button>();
       // GetComponent<Image>().sprite = _sleep;
        button.image.sprite = _sleep;
        menu.SetActive(false);
    }

    private void Update()
    {
        OpenMenuOnInput();
    }

    public void OpenMenu()
    { 
        if (!isMenuOpen)
        { 
            menu.SetActive(true);
           // button.image.sprite = _awake;
            isMenuOpen = true;
        }
        else if (isMenuOpen)
        {
            menu.SetActive(false);
           // button.image.sprite = _sleep;
            isMenuOpen = false;
        }
    }

     void OpenMenuOnInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
            {
                menu.SetActive(true);
                isMenuOpen = true;
            }
            else if (isMenuOpen)
            {
                menu.SetActive(false);
                isMenuOpen = false;
            }
        }
    }

    public void ChangeButtonImage()
    {
        button.image.sprite = _awake;
    }

   
}

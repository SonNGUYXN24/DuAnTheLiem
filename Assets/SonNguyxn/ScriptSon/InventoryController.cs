using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public GameObject statusCanvas;
    public GameObject itemCanvas;
    public GameObject weaponsCanvas;
    public GameObject pauseController;
    private bool inItems = false;
    private bool inWeapon = false;
    private bool inStatus = false;
    private bool inInventory = false;

    void Update()
    {
        // Hiển thị InventoryCanvas khi nhấn phím Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(true);
            itemCanvas.SetActive(true);
            pauseController.SetActive(false);
            inItems = true;
            inInventory = true;
        }

        // Thoát khỏi InventoryCanvas khi nhấn phím Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideAllCanvases();
            pauseController.SetActive(true);
        }
    }

    public void ShowStatusCanvas()
    {
        statusCanvas.SetActive(true);
        itemCanvas.SetActive(false);
        weaponsCanvas.SetActive(false);
        inStatus = true;
        inItems = false;
        inWeapon = false;
        Debug.Log("Button Hoat Dong");
    }

    public void ShowItemCanvas()
    {
        inItems = true;
        inWeapon = false;
        inStatus = false;
        itemCanvas.SetActive(true);
        weaponsCanvas.SetActive(false);
        statusCanvas.SetActive(false);
    }

    public void ShowWeaponsCanvas()
    {

        inWeapon = true;
        inStatus = false;
        inItems = false;
        weaponsCanvas.SetActive(true);
        statusCanvas.SetActive(false);
        itemCanvas.SetActive(false);
    }

    public void HideAllCanvases()
    {
        
            itemCanvas.SetActive(false);
            inItems = false;
        
        
            weaponsCanvas.SetActive(false);
            inWeapon = false;
        
        
            statusCanvas.SetActive(false);
            inStatus = false;
       
        
            inventoryCanvas.SetActive(false);
            inInventory = false;
        
    }
}

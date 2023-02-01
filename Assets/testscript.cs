using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
     public GameObject[] all3d;
 
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[0].SetActive(true);
        }
 
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[1].SetActive(true);
        }
 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[2].SetActive(true);
        }
 
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[3].SetActive(true);
        }
 
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[4].SetActive(true);
        }
 
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 0; i < all3d.Length; i++)
            {
                all3d[i].SetActive(false);
            }
 
            all3d[5].SetActive(true);
        }
    }
}

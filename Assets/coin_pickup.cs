using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public float timeBoost = 5f;
    public DoubleSlider doubleSlider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doubleSlider != null)
            {
                doubleSlider.addTime();
            }

              
        
            this.gameObject.SetActive(false); // Deactivate the coin when picked up
        
        }
    }
}

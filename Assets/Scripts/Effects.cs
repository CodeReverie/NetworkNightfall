using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Effects : MonoBehaviour
{
    public GameObject Attack1, Attack2, Defense1, Defense2;

     public void Openattack1()
    {
        Attack1.SetActive(true);
        Attack2.SetActive(false);
        Defense1.SetActive(false);
        Defense2.SetActive(false);
    }
        public void Openattack2()
    {
        Attack1.SetActive(false);
        Attack2.SetActive(true);
        Defense1.SetActive(false);
        Defense2.SetActive(false);
    }
        public void Opendefense1()
    {
        Attack1.SetActive(false);
        Attack2.SetActive(false);
        Defense1.SetActive(true);
        Defense2.SetActive(false);
    }
        public void Opendefense2()
    {
        Attack1.SetActive(false);
        Attack2.SetActive(false);
        Defense1.SetActive(false);
        Defense2.SetActive(true);
    }
    
}

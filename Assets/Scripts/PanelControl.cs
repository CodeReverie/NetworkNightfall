using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelControl : MonoBehaviour
{
    public GameObject Panel1, Panel2, Panel3, Panel4, Panel5, Panel6, Panel7, Panel8;

    public void OpenPanel1()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
         Panel8.SetActive(false);

    }
    public void OpenPanel2()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
         Panel8.SetActive(false);

    }
       public void OpenPanel3()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(true);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
         Panel8.SetActive(false);

    }
       public void OpenPanel4()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(true);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
         Panel8.SetActive(false);

    }
       public void OpenPanel5()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(true);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
        Panel8.SetActive(false);

    }
       public void OpenPanel6()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(true);
        Panel7.SetActive(false);
         Panel8.SetActive(false);

    }
       public void OpenPanel7()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(true);
        Panel8.SetActive(false);

    }
     public void OpenPanel8()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
        Panel8.SetActive(true);

    }





}

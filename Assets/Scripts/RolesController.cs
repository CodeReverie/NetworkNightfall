using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RolesController : MonoBehaviour
{
     public GameObject Panel,PanelFW, PanelAV, PanelDB, PanelQM, PanelNMT, PanelRAT;

      public void OpenOptions()
    {
        Panel.SetActive(true);
        PanelFW.SetActive(false);
        PanelAV.SetActive(false);
        PanelDB.SetActive(false);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelFW()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(true);
        PanelAV.SetActive(false);
        PanelDB.SetActive(false);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelAV()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(false);
        PanelAV.SetActive(true);
        PanelDB.SetActive(false);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelDB()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(false);
        PanelAV.SetActive(false);
        PanelDB.SetActive(true);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelQM()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(false);
        PanelAV.SetActive(false);
        PanelDB.SetActive(false);
        PanelQM.SetActive(true);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelNMT()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(false);
        PanelAV.SetActive(false);
        PanelDB.SetActive(false);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(true);
        PanelRAT.SetActive(false);

    }
     public void OpenPanelRAT()
    {
        Panel.SetActive(false);
        PanelFW.SetActive(false);
        PanelAV.SetActive(false);
        PanelDB.SetActive(false);
        PanelQM.SetActive(false);
        PanelNMT.SetActive(false);
        PanelRAT.SetActive(true);

    }
    
}

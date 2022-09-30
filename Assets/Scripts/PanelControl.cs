using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Open;
    public GameObject[] Close;
    
    public void panelControl() {
        if (Open != null) {
            foreach (var i in Open)
                i.SetActive(true);
        }
        if (Close != null) {
            foreach (var i in Close)
                i.SetActive(false);
        }
    }
}

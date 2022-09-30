using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lista : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject painel;
    
    public void SetActive() {
        if (painel != null) {
            painel.SetActive(!painel.activeInHierarchy);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painel : MonoBehaviour
{
    public GameObject painel;
    public void openPanel() {
        if (painel != null) {
            painel.SetActive(true);
        }
    }

    public void closePanel() {
        if (painel != null) {
            painel.SetActive(false);
        }
    }
}

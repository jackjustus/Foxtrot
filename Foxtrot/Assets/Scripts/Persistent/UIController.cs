using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject blackScreen;

    void Awake() {
        blackScreen.SetActive(false);
    }

    public void BlackoutScreen(bool fadeOut) {
        if (fadeOut) {
            Debug.Log("[UICTRL] Fading out screen");
            blackScreen.SetActive(true);
    } else
            blackScreen.SetActive(false);

    }
}

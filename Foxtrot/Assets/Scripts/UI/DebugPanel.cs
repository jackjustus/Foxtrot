using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentSceneObject;

    public void Update()
    {
        currentSceneObject.text = GameController.currentScene;
    }

}

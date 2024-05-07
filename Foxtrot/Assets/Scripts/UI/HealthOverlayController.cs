using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject healthOverlayUI;
    private Image healthUIImage;
    [SerializeField] TextMeshProUGUI healthTextUI;
    private TMP_Text healthText;

    public bool debugEnabled = false;


    public void Awake() {
        healthUIImage = healthOverlayUI.GetComponent<Image>();
        healthText = healthOverlayUI.GetComponent<TMP_Text>();
    }

    // public void Update() {
    //     if (debugEnabled) {
    //         SetHealthOverlay(currentHealth, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().getPlayerMaxHealth());
    //     }
    // }
    public void SetHealthOverlay(float health, float maxHealth)
    {
        healthUIImage.fillAmount = (float)health / maxHealth;

        healthTextUI.text = health + "/" + maxHealth;
    }
}

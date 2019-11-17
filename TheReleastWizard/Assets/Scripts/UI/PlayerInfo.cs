using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField] Image healthImg;
    [SerializeField] Image xp;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] PlayerController pc;

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = pc.GetHealthAsPercent();
        healthText.SetText(pc.GetHealth() + "/" + pc.GetMaxHealth());

        xp.fillAmount = pc.GetXpAsPercent();
    }
}

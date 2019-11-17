using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField] Image healthImg;
    [SerializeField] Image xp;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] Image levelNotifierBg;
    [SerializeField] TextMeshProUGUI levelNotifierText;

    [SerializeField] Image baubelNotifierBg;

    [SerializeField] PlayerController pc;

    public int levelLogMax = 4;
    public int baubelLogMax = 4;

    public float maxLevelLogDur = 10.0f;
    public float maxBaubelLogDur = 10.0f;

    public float delayToDisappearLevelLog = 5.0f;
    public float delayToDisappearBaubelLog = 5.0f;

    List<string> levelNotifier = new List<string>();
    List<string> baubelNotifier = new List<string>();

    bool updatedLevelLog = false;
    bool updatedBaubelLog = false;

    bool resetLogTimer = false;

    float timeSinceLastLevelLog = 0.0f;
    float timeSinceLastBaubelLog = 0.0f;

    float levelLogDelayTimer = 0.0f;
    float baubelLogDelayTimer = 0.0f;

    private void Start()
    {
        pc.AddListenerForLevelUp(LevelUpNotifier);
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = pc.GetHealthAsPercent();
        healthText.SetText(pc.GetHealth() + "/" + pc.GetMaxHealth());

        xp.fillAmount = pc.GetXpAsPercent();

        if(timeSinceLastLevelLog < maxLevelLogDur)
        {
            if(updatedLevelLog)
            {
                levelNotifierText.SetText("");

                foreach (string s in levelNotifier)
                    levelNotifierText.SetText(levelNotifierText.text + s + "\n");

                updatedLevelLog = false;
            }

            levelNotifierText.color = new Color(levelNotifierText.color.r, levelNotifierText.color.g, levelNotifierText.color.b,
                    1 - timeSinceLastLevelLog / maxLevelLogDur);

            if(timeSinceLastLevelLog > maxLevelLogDur / 2)
                levelNotifierBg.color = new Color(levelNotifierBg.color.r, levelNotifierBg.color.g, levelNotifierBg.color.b,
                        1 - timeSinceLastLevelLog / maxLevelLogDur);
            else
                levelNotifierBg.color = new Color(levelNotifierBg.color.r, levelNotifierBg.color.g, levelNotifierBg.color.b,
                        0.5f);
        }

        if(resetLogTimer)
        {
            timeSinceLastLevelLog = 0.0f;

            levelLogDelayTimer += Time.deltaTime;

            if(levelLogDelayTimer >= delayToDisappearLevelLog)
            {
                resetLogTimer = false;
                levelLogDelayTimer = 0.0f;
            }
        }
        else
        {
            timeSinceLastLevelLog += Time.deltaTime;
        }

    }

    void LevelUpNotifier(int level)
    {
        if (levelNotifier.Count > levelLogMax)
        {
            levelNotifier.RemoveAt(0);
        }

        levelNotifier.Add("You acheived level " + level + "!");

        updatedLevelLog = true;

        resetLogTimer = true;
    }

}

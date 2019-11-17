using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelLog : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    [SerializeField] Image levelNotifierBg;
    [SerializeField] TextMeshProUGUI levelNotifierText;

    bool updatedLevelLog = false;

    bool displayLevelLog = false;

    bool resetLogTimer = false;

    float timeSinceLastLevelLog = 0.0f;

    float levelLogDelayTimer = 0.0f;

    List<string> levelNotifier = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

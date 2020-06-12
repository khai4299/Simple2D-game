using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveCountUI : MonoBehaviour
{
    public Text liveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        liveText.text = "Lives: " + GameMaster.currentLive.ToString();
    }
}

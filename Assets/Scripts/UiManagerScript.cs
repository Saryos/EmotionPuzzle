using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerScript : MonoBehaviour {

    private Text timerText;
    private Text runningPointsText;
    private Text dogPointsText;
    private Text breakPointsText;
    private Text bridgePointsText;

    private float timer;

    public float timeLeft = 30.0f;

    public enum Actions { Run, Shield, Break, Build };

    public int runningPoints = 0;
    public int dogPoints = 0;
    public int breakPoints = 0;
    public int bridgePoints = 0;

    // Use this for initialization
    void Start () {
        timerText = GameObject.Find("TimeText").GetComponent<Text>();
        runningPointsText = GameObject.Find("RunningPointsText").GetComponent<Text>();
        dogPointsText = GameObject.Find("DogPointsText").GetComponent<Text>();
        breakPointsText = GameObject.Find("BreakPointsText").GetComponent<Text>();
        bridgePointsText = GameObject.Find("BridgePointsText").GetComponent<Text>();

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        timerText.text = string.Format("TIME LEFT: {0:0.##}", timeLeft - timer);
	}

    public void PointsChanged( Actions action, bool plus )
    {
        int pointChange = plus ? 1 : -1;

        switch (action)
        {
            case Actions.Run:
                runningPoints += 1;
                runningPointsText.text = string.Format("Running Points: {0}", runningPoints);
                break;
            case Actions.Shield:
                dogPoints += 1;
                dogPointsText.text = string.Format("Dog Points: {0}", dogPoints);
                break;
            case Actions.Break:
                breakPoints += 1;
                breakPointsText.text = string.Format("Break Points: {0}", breakPoints);
                break;
            case Actions.Build:
                bridgePoints += 1;
                bridgePointsText.text = string.Format("Bridge Points: {0}", bridgePoints);
                break;
        }
    }
}

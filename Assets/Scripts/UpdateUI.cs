using UnityEngine;
using TMPro;
using NodeCanvas.Framework;

public class UpdateUI : MonoBehaviour
{
    public TextMeshProUGUI alertText;
    public GameObject drone;

    public Color patrol = new Color(0, 1, 0);
    public Color alert = new Color(1, 0, 0);
    public Color searching = new Color(1, 1, 0);

    private Blackboard droneBlackboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (drone != null)
        {
            droneBlackboard = drone.GetComponent<Blackboard>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float alerted = droneBlackboard.GetVariable<float>("alerted").value;
        if (alerted <= 0f)
        {
            alertText.text = "";
            alertText.color = patrol;
        }
        else if (alerted < 1)
        {
            alertText.text = "[Searching...]";
            alertText.color = searching;
        }
        else
        {
            alertText.text = "[Alerted!: You got spotted!]";
            alertText.color = alert;
        }
    }
}

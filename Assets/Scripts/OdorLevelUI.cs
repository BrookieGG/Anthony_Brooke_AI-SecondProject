using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.UI;

public class OdorLevelUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Blackboard otherObject;
    public Image odorBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float odorLevel = otherObject.GetVariableValue<float>("odorLevel");

        odorBar.fillAmount = (odorLevel / 100);
    }
}

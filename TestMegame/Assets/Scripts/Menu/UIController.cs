using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameObject UICanvas;
    private TextMeshProUGUI[] TextPanels;
    private TextMeshProUGUI PointsPanelText;
    private TextMeshProUGUI HealthPanelText;

    private void Awake()
    {
        UICanvas = gameObject;
        TextPanels = UICanvas.GetComponentsInChildren<TextMeshProUGUI>();
        PointsPanelText = TextPanels[0];
        HealthPanelText = TextPanels[1];
    }

    public void PointsChange(int points)
    {
        PointsPanelText.text = "Points: " + points;
    }
    public void HealthChange(int hp)
    {
        HealthPanelText.text = "Health: " + hp;
    }
}

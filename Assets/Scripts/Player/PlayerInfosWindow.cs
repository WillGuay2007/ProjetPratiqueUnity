using TMPro;
using UnityEngine;

public class PlayerInfosWindow : Window
{
    [SerializeField] private TextMeshProUGUI m_dashAmountText;
    [SerializeField] private TextMeshProUGUI m_canFlipText;

    public void SetCanFlipText(string text) => m_canFlipText.text = text;
    public void SetDashAmountText(string text) => m_dashAmountText.text = text;
}
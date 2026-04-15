using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueWindow : Window
{
    [SerializeField] private TMP_Text m_textLabel;
    public TMP_Text TextLabel => m_textLabel;


    public IEnumerator SetText(string text, IDialogueStrategy displayStrategy)
    {
        yield return displayStrategy.DisplayDialogue(this, text);
    }

    public void ResetWindow()
    {
        m_textLabel.text = string.Empty;
    }
    public override void Show()
    {
        base.Show();
        ResetWindow();
    }
}

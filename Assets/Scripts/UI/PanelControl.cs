using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public GameObject targetPanel; // 제어할 패널

    // 패널을 켜는 메서드
    public void ShowPanel()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
        }
    }

    // 패널을 끄는 메서드
    public void HidePanel()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }
    }
}

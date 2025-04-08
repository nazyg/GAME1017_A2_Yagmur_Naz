using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public void ToggleOptions()
    {
        bool isActive = optionsPanel.activeSelf;
        optionsPanel.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f;
    }
}


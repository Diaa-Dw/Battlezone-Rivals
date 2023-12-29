using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    void Start()
    {
        // Assuming panels are initially enabled or disabled as needed
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void SwitchPanels()
    {
        // Toggle the active state of Panel1 and Panel2
        panel1.SetActive(!panel1.activeSelf);
        panel2.SetActive(!panel2.activeSelf);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    public Text levelText;      // Shows the current level
    public Text xpText;         // Shows XP in numbers (e.g., 45/100)
    public Slider xpSlider;     // Progress bar for XP

    private void Start()
    {
        XPManager.Instance.OnXPUpdated += UpdateXPUI;
        UpdateXPUI(XPManager.Instance.currentXP, XPManager.Instance.currentLevel);
    }

    private void UpdateXPUI(int xp, int level)
    {
        levelText.text = "Level: " + level;
        xpText.text = $"{xp} / {XPManager.Instance.xpToNextLevel} XP";

        xpSlider.maxValue = XPManager.Instance.xpToNextLevel;
        xpSlider.value = xp;
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuseMenu : MonoBehaviour
{
    public TextMeshProUGUI baseTowerDisplayName;
    public Image baseTowerImageDisplay;
    public TextMeshProUGUI formulaPreviewText;
    public Button fuseExecuteButton;
    public TextMeshProUGUI fuseCosttext;

    private string baseTowerName = "";
    private string selectedTowerName = "";

    private int currentRecipeCost = 0;

    public void SetupMenu(string name, Sprite towerSprite)
    {
        baseTowerName = name;
        baseTowerDisplayName.text = "Selected: " + name;
        selectedTowerName = ""; // Reset choice
        formulaPreviewText.text = "Pick a component...";
        fuseExecuteButton.interactable = false;

        if (towerSprite != null)
        {
            baseTowerImageDisplay.sprite = towerSprite;
            baseTowerImageDisplay.enabled = true;
        }
        else
        {
            baseTowerImageDisplay.enabled = false;
        }
    }


    public void SelectComponent(string component)
    {
        selectedTowerName = component;
        formulaPreviewText.text = baseTowerName + " + " + component;
        currentRecipeCost = GameMaster.instance.GetFusionCost(baseTowerName, selectedTowerName);
        fuseCosttext.text = "$"+ currentRecipeCost;

        if(GameMaster.instance.money >= currentRecipeCost)
        {
            fuseExecuteButton.interactable = true;
            fuseCosttext.color = Color.yellow;
        }
        else
        {
            fuseExecuteButton.interactable = false;
            fuseCosttext.color = Color.red;
        }
    }

    public void OnClickFuse()
    {
        GameMaster.instance.CombineTower(baseTowerName, selectedTowerName, currentRecipeCost);
    }
}

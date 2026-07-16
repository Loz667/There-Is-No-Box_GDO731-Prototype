using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveCharacterUI : UIPanel
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI moraleText;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image portrait;
    
    private Character character;

    public void SetCharacter(Character unit)
    {
	    character = unit;
	    healthText.text = unit.CurrentHealth.ToString();
	    moraleText.text = unit.CurrentMorale.ToString();
	    actionPointsText.text = unit.ActionPoints.ToString();
	    characterName.text = unit.Name;
	    portrait.sprite = unit.Data.Portrait;
	    /*
	    model = curInvestigator;
		textStamina.Text = curInvestigator.CurrentStamina.ToString();
		textSanity.Text = curInvestigator.CurrentSanity.ToString();
		progressSanity.SetProgress((float)curInvestigator.CurrentSanity / (float)curInvestigator.MaxSanity);
		progressStamina.SetProgress((float)curInvestigator.CurrentStamina / (float)curInvestigator.MaxStamina);
		ShowAbilityEnabled(isEnabled: false);
		SetInventoryState(InventoryBagState.Disabled);
		LoadInvestigatorTexture(curInvestigator);
		return 0f;

	    */
    } 
    
}

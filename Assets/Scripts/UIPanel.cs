using TMPro;
using Units;
using UnityEngine;

public class UIPanel : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Player player;

    private void Awake(){
        player.OnChangedMoney += UpdateMoneyUI;
        UpdateMoneyUI();
    }


    private void UpdateMoneyUI(){
        moneyText.text = player.AmountOfMoney + @" <sprite=""MoneyIcon"" name=""MoneyIcon"">";
    }
}
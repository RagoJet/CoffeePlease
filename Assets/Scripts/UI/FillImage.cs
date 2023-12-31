using System;
using DG.Tweening;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class FillImage : MonoBehaviour{
    [SerializeField] private Image fillImage;
    [SerializeField] private int price;
    private int _moneyRemains;
    [SerializeField] private TextMeshProUGUI moneyRemainsText;
    private Coroutine _coroutine;

    private Tween _tween;

    public event Action OnFilled;

    private void Awake(){
        _moneyRemains = price;
        UpdateUI();
    }

    private void UpdateUI(){
        _tween.Kill();
        _tween = fillImage.DOFillAmount((float) (price - _moneyRemains) / price, 0.3f);
        moneyRemainsText.text = _moneyRemains + @" <sprite=""MoneyIcon"" name=""MoneyIcon"">";
    }


    public void GetMoney(Player player){
        int money = 5;
        if (player.TryGiveMoney(money)){
            _moneyRemains -= money;

            if (_moneyRemains <= 0){
                OnFilled.Invoke();
                _tween.Kill();
            }

            UpdateUI();
        }
    }

    public void Refresh(){
        _moneyRemains = price;
        UpdateUI();
    }

    private void OnDestroy(){
        _tween.Kill();
    }
}
using System.Collections.Generic;
using DG.Tweening;
using Services;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class StackOfMoney : MonoBehaviour{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image moneyImagePrefab;
    [SerializeField] private RectTransform moneyUI;
    private List<MoneyObject> _stackOfMonies = new List<MoneyObject>();

    public void AddMoney(){
        MoneyObject moneyObject = AllServices.Instance.Get<IGameObjectsFactory>()
            .CreateMoney(transform.position + new Vector3(0, _stackOfMonies.Count * 0.4f, 0));
        _stackOfMonies.Add(moneyObject);
    }

    public void GiveMoney(Player player){
        if (_stackOfMonies.Count <= 0){
            return;
        }

        float duration = 0.6f;
        foreach (var moneyObject in _stackOfMonies){
            Destroy(moneyObject.gameObject);
            CashOut(player, duration);
            duration += 0.15f;
        }

        _stackOfMonies.Clear();
        AllServices.Instance.Get<IAudioPlayer>().PlayMoneysSound();
    }


    public void CashOut(Player player, float duration){
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Image moneyImage = Instantiate(moneyImagePrefab, pos, Quaternion.identity, canvas.transform);

        moneyImage.rectTransform.DOScale(moneyImage.rectTransform.localScale / 2, duration * 0.8f);
        moneyImage.rectTransform.DOMove(moneyUI.position, duration).OnComplete(() => {
            player.AddMoney(7);
            Destroy(moneyImage.gameObject);
        });
    }
}
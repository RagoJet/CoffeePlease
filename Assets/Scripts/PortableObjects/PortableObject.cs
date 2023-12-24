using DG.Tweening;
using UnityEngine;

public class PortableObject : MonoBehaviour{
    private Tween _tween;

    public void ChangeDOTweenPos(Vector3 pos){
        _tween.Kill();
        _tween = transform.DOLocalMove(pos, 0.5f);
    }

    public void ChangeDOTweenPosThenDestroy(Vector3 pos){
        _tween.Kill();
        _tween = transform.DOLocalMove(pos, 0.5f).OnComplete(() => { Destroy(this.gameObject); });
    }
}
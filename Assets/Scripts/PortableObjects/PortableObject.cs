using System;
using DG.Tweening;
using UnityEngine;

public class PortableObject : MonoBehaviour{
    private Tween _tween;

    public void MoveTo(Vector3 pos, float duration, Action func = null){
        _tween.Kill();
        _tween = transform.DOLocalJump(pos, 1, 1, duration).OnComplete(() => { func?.Invoke(); });
    }

    private void OnDestroy(){
        _tween.Kill();
    }
}
using System;
using DG.Tweening;
using UnityEngine;

public class PortableObject : MonoBehaviour{
    private Tween _tween;

    public void MoveTo(Vector3 pos, Action func = null){
        _tween.Kill();
        _tween = transform.DOLocalJump(pos, 1, 1, 0.4f).OnComplete(() => { func?.Invoke(); });
    }

    private void OnDestroy(){
        _tween.Kill();
    }
}
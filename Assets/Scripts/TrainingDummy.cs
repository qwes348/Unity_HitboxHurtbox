using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class TrainingDummy : MonoBehaviour
{
    public Material myMat;
    public string ColorPropertyName = "_Color";

    private TweenerCore<Color, Color, ColorOptions> runningTween;
    private Color originColor = Color.white;

    private void Awake()
    {
        if (myMat != null)
            originColor = myMat.GetColor(ColorPropertyName);
    }

    public void OnGetDamage(DamageInfo info)
    {
        Debug.LogFormat("GetDamage!! Attacker Is {0}", info.attackerHitbox.name);

        if (runningTween.IsActive())
        {
            runningTween.Kill(true);
            myMat.SetColor(ColorPropertyName, originColor);
        }

        runningTween = myMat.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
    }
}

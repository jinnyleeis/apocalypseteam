using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ControlColorCurve : MonoBehaviour
{
    Volume volume;
    ColorCurves curves;
    public TextureCurve textureCurve;
    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out curves);
    }

    public void OnOffColorCurve(bool on)
    {
        curves.active = on;
    }

    public void GeneralSetting()
    {
        
    }

    private void Update()
    {
        curves.hueVsSat.Override(textureCurve);
    }
}

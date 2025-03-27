using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public int maxHealth;
    public int value;
    public float barSpeed;

    public RectTransform _topBar;
    public RectTransform _bottomBar;
    //protected GameObject damageObject;
    
    protected float _fullBarWidth;
    protected float targetWidth => value * _fullBarWidth / maxHealth;
    protected Coroutine _adjustBarWidthCoroutine;
    
    //protected DamageBehavior damageBehavior;
    ///private int damageAmount;

    private void Start()
    {
        _fullBarWidth = _topBar.rect.width;
        
    }

    public void Change(int amount)
    {
        value = Mathf.Clamp(value + amount, 0, maxHealth);
        if (_adjustBarWidthCoroutine != null)
        {
            StopCoroutine(_adjustBarWidthCoroutine);
        }
        _adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }
    

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? _bottomBar : _topBar;
        var slowChangeBar = amount >= 0 ? _topBar : _bottomBar;
        suddenChangeBar.SetWidth(targetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, targetWidth, Time.deltaTime * barSpeed));
            yield return null;
        }
        
        slowChangeBar.SetWidth(targetWidth);
    }
}

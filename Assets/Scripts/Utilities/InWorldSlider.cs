using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldSlider : MonoBehaviour
{
    #region Value
    public float value
    {
        get { return m_value; }
        set
        {
            if (m_value == value) return;
            m_value = value;
            if (OnVariableChange != null)
                OnVariableChange(m_value);
        }
    }

    float m_value;

    public delegate void OnVariableChangeDelegate(float newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    #endregion

    public float maxValue = 50;

    public Transform slider;
    public Transform background;

    public Dir scaleDirection;
    public enum Dir
    {
        X,
        Y
    }
    private void Start()
    {
        OnVariableChange += UpdateSlider;
    }

    public void UpdateSlider(float val)
    {
        if(scaleDirection == Dir.X)
        {
            float targetScale = background.localScale.x * (val / maxValue);
            float targetPos = (targetScale - background.localScale.x) / 2f;
            slider.localScale = new Vector2(targetScale, slider.localScale.y);
            slider.localPosition = new Vector2(targetPos, slider.localPosition.y);
        }
        else if(scaleDirection == Dir.Y)
        {
            float targetScale = (val * background.localScale.y);
            float targetPos = val - background.localScale.y;
            slider.localScale = new Vector2(slider.localScale.x, targetScale);
            slider.localPosition = new Vector2(slider.localScale.x, targetPos);
        }
    }
}

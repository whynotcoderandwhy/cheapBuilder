using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Building
{
    /// <summary>
    /// probability of requiring a repair
    /// </summary>
    protected float m_integrity; 
    public float Integrity => m_integrity;
    protected float m_value;
    public float Value => m_value;

    public void Init(float value, float integrity) { m_integrity = integrity; m_value = value; }
}

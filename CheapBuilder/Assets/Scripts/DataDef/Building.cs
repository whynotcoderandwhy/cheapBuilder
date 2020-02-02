﻿using System.Collections;
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

    protected bool m_hasWorkOrder;
    public bool HasWorkOrder => m_hasWorkOrder;
    public void SetHasWorkOrder(bool value) { m_hasWorkOrder = value; }
    public void Init(float value, float integrity) { m_integrity = integrity; m_value = value; }
}

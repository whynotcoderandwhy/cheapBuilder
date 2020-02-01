using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrder 
{
    protected float m_baseCost;
    protected Building m_building;
    protected List<ProductOrder> m_desiredMaterialList;
    protected List<ProductOrder> m_actualMaterialList;
    protected float m_manHours;
    protected float m_spentManHours;
    protected int m_dueDate;
    protected int m_startDate;

    /// <summary>
    /// Lets the player predict impact aside from worker factor
    /// </summary>
    /// <returns>new integrity value for building at current configuration</returns>
    public float PredictIntegrityImpact()
    {
        throw new System.NotImplementedException();
    }

    public bool SplitActualList(int actualMaterialIndex)
    {
        return false;
    }

    public void LockQuantity(int actualMaterialIndex)
    {

    }

    /// <summary>
    /// Quantity of an item can never be less than one
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="newMaterialCount"></param>
    public void UpdateQuantity(int actualMaterialIndex, int newMaterialCount)
    {

    }

    public bool RemoveActualMaterial(int acutalMaterialIndex)
    {
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    /// <summary>
    /// this takes the index of the material list, ensures that it exists, then creates a new mat list after it in the list with halved quantities
    /// (in the event of an uneven number, old list gets the spare)
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <returns>true if split successfully, false if index didn't exist</returns>
    public bool SplitActualList(int actualMaterialIndex)
    {
        if (m_actualMaterialList.ElementAt(actualMaterialIndex) == null)
            return false;
        ProductOrder newmatlist = m_actualMaterialList[actualMaterialIndex];
        m_actualMaterialList[actualMaterialIndex].m_quantity = Mathf.Ceil(m_actualMaterialList[actualMaterialIndex].m_quantity / 2.0f);
        newmatlist.m_quantity = Mathf.Floor(newmatlist.m_quantity / 2.0f);
        m_actualMaterialList.Insert(actualMaterialIndex + 1, newmatlist);
        return true;
    }

    /// <summary>
    /// ensures that the quantity is locked and should not be changed
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="Lock">true if the quantity is to be locked</param>
    public void LockQuantity(int actualMaterialIndex, bool Lock = true)
    {
        if (m_actualMaterialList.ElementAt(actualMaterialIndex) == null)
            return;

        m_actualMaterialList[actualMaterialIndex].m_quantityLocked = Lock;
    }

    /// <summary>
    /// Quantity of an item can never be less than one
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="newMaterialCount"></param>
    public void UpdateQuantity(int actualMaterialIndex, int newMaterialCount)
    {
        if (m_actualMaterialList.ElementAt(actualMaterialIndex) == null)
            return;
        if (m_actualMaterialList[actualMaterialIndex].m_quantityLocked) //this should never be true, as hopefully people checked for it to be locked before trying to update
            return;
        m_actualMaterialList[actualMaterialIndex].m_quantity += newMaterialCount;
    }

    /// <summary>
    /// compares the materials of the 2 items 
    /// returns true if they both exist and contain the same material (assumes only 1 material match)
    /// </summary>
    /// <param name="firstMaterialIndex"></param>
    /// <param name="secondMaterialIndex"></param>
    /// <returns></returns>
    protected bool AreTheseTheSameMaterial(int firstMaterialIndex, int secondMaterialIndex)
    {
        if (m_actualMaterialList.ElementAt(firstMaterialIndex) == null || m_actualMaterialList.ElementAt(secondMaterialIndex) == null) //they need to exist
            return false;
        //this checks if there are any values shared by both this material and the one above it
        Material.MaterialType comparison = m_actualMaterialList[firstMaterialIndex].m_material.MaterialFlags
                                         & m_actualMaterialList[secondMaterialIndex].m_material.MaterialFlags;

        return ((comparison & Material.MaterialType.AllMaterials) == 0);
    }

    /// <summary>
    /// this removes a material from the "actual" list and allocates quantity elsewhere
    /// </summary>
    /// <param name="acutalMaterialIndex"></param>
    /// <returns></returns>
    public bool RemoveActualMaterial(int actualMaterialIndex)
    {
        if (!AreTheseTheSameMaterial(actualMaterialIndex, actualMaterialIndex-1))
            return false; //if there are no materials shared, then this is the top of the list of the type of material, and we should not remove it

        float ReDist = m_actualMaterialList[actualMaterialIndex].m_quantity;
        Material MatType = m_actualMaterialList[actualMaterialIndex].m_material;
        m_actualMaterialList.RemoveAt(actualMaterialIndex);

        //redist mats of same type 
        List<int> indexesOfSameMats = null;
        foreach (ProductOrder p in m_actualMaterialList)
        {
            if (AreTheseTheSameMaterial(actualMaterialIndex - 1, m_actualMaterialList.IndexOf(p)) 
                && !m_actualMaterialList[m_actualMaterialList.IndexOf(p)].m_quantityLocked)
                indexesOfSameMats.Add(m_actualMaterialList.IndexOf(p));
        }
        if (indexesOfSameMats.Count <= 1)
        {
            //this only happens if there is only the base left. force-load mats back to it, ignoring lock
            m_actualMaterialList[actualMaterialIndex - 1].m_quantity += ReDist;
        }
        else
        {
            int sharedAmount = (int) ReDist / indexesOfSameMats.Count;
            int leftovers = (int) ReDist % indexesOfSameMats.Count;


            foreach(int sharedMat in indexesOfSameMats )
            {
                m_actualMaterialList[sharedMat].m_quantity += sharedAmount + ((leftovers-- >= 0) ? 1: 0); 
            }
        }

        return true;

    }
}

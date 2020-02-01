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

    protected int m_numberOfWorkers;


    public bool CreateInitialWorkOrderSpecs(Building building)
    {

        if (m_numberOfWorkers < 1)
            return false;
        if (m_desiredMaterialList != default)
            return false;
        if (m_actualMaterialList != default)
            return false;

        m_desiredMaterialList = new List<ProductOrder>();
        m_actualMaterialList = new List<ProductOrder>();//these need to be initialized


        m_building = building;
        m_startDate = WorldState.Instance.Day;

        float quantityofmaterials = 0;
        float totalbuildingmaterialscost = 0;

        //assuming both base value and integrity are values out of 100
        //determine desired material list
        //value changes quality of materials, integrity changes quantity
        for (int i = 0;
            i < Random.Range(
                Mathf.Clamp(1 * Mathf.FloorToInt(building.Integrity / 20), 1, 5),
                Mathf.Clamp(2 * Mathf.FloorToInt(building.Integrity / 20), 2, 10));
            i++) //# of orders 
        {
            ProductOrder po = new ProductOrder();
            po.m_quantity = Random.Range(1, 100) * (1 + building.Integrity / 100);
            po.m_quantityLocked = false;
            po.m_material = Material.GenerateRandomMaterial(building.Value);
            quantityofmaterials += po.m_quantity;
            totalbuildingmaterialscost += po.m_material.Cost;
            m_desiredMaterialList.Add(po);
        }


        //determine manhours
        m_manHours = quantityofmaterials /( (WorldState.Instance.DailyPersonalMaterialConsumption/24.0f) * m_numberOfWorkers);


        //determine due date
        m_dueDate = m_startDate+Mathf.CeilToInt(m_manHours / (m_numberOfWorkers*8.0f)); //start date plus 8 man hours/day per worker


        //determine base cost
        m_baseCost = (m_manHours * WorldState.Instance.WorkerWages) + totalbuildingmaterialscost;

        return true;

    }








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
        if (m_actualMaterialList[actualMaterialIndex] == null)
            return false;
        ProductOrder np;
        if (m_actualMaterialList[actualMaterialIndex].SplitThisOrder(out np))
        {
            m_actualMaterialList.Insert(actualMaterialIndex + 1, np);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ensures that the quantity is locked and should not be changed
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="Lock">true if the quantity is to be locked</param>
    public void LockQuantity(int actualMaterialIndex, bool Lock = true)
    {
        if (m_actualMaterialList[actualMaterialIndex] == null)
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
        if (m_actualMaterialList[actualMaterialIndex] == null)
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
        if (m_actualMaterialList[firstMaterialIndex] == null || m_actualMaterialList[secondMaterialIndex] == null) //they need to exist
            return false;
        //this checks if there are any values shared by both this material and the one above it
        Material.MaterialType comparison = m_actualMaterialList[firstMaterialIndex].m_material.MaterialFlags
                                         & m_actualMaterialList[secondMaterialIndex].m_material.MaterialFlags;

        return ((comparison & Material.MaterialType.AllMaterials) != 0); //if the comparison got a match, and it's a material, it should be non-0
    }

    /// <summary>
    /// this removes a material from the "actual" list and allocates quantity elsewhere
    /// </summary>
    /// <param name="acutalMaterialIndex"></param>
    /// <returns></returns>
    public bool RemoveActualMaterial(int actualMaterialIndex)
    {
        if (actualMaterialIndex < 1)
            return false;
        if (!AreTheseTheSameMaterial(actualMaterialIndex, actualMaterialIndex-1))
            return false; //if there are no materials shared, then this is the top of the list of the type of material, and we should not remove it

        float ReDist = m_actualMaterialList[actualMaterialIndex].m_quantity;
        Material MatType = m_actualMaterialList[actualMaterialIndex].m_material;
        m_actualMaterialList.RemoveAt(actualMaterialIndex);

        Redistribute(actualMaterialIndex, ReDist);

        return true;

    }

    /// <summary>
    /// pass in the original index of the one destroyed, it'll use that to get the one above it which is assumed to be the same material
    /// If this is called without the destroyer, just add 1 to the index you wish to use the material of
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="amountToRedistribute"></param>
    protected void Redistribute(int actualMaterialIndex, float amountToRedistribute)
    {
        //redist mats of same type 
        List<int> indexesOfSameMats = new List<int>();
        foreach (ProductOrder p in m_actualMaterialList)
        {
            if (AreTheseTheSameMaterial(actualMaterialIndex - 1, m_actualMaterialList.IndexOf(p))
                && !m_actualMaterialList[m_actualMaterialList.IndexOf(p)].m_quantityLocked)
                indexesOfSameMats.Add(m_actualMaterialList.IndexOf(p));
        }
        if (indexesOfSameMats.Count <= 1)
        {
            //this only happens if there is only the base left. force-load mats back to it, ignoring lock
            m_actualMaterialList[actualMaterialIndex - 1].m_quantity += amountToRedistribute;
        }
        else
        {
            int sharedAmount = (int)amountToRedistribute / indexesOfSameMats.Count;
            int leftovers = (int)amountToRedistribute % indexesOfSameMats.Count;


            foreach (int sharedMat in indexesOfSameMats)
            {
                m_actualMaterialList[sharedMat].m_quantity += sharedAmount + ((leftovers-- >= 0) ? 1 : 0);
            }
        }
    }



}

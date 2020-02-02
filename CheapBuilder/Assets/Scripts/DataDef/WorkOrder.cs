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
    protected List<ProductOrder> m_sortedbyCostMaterialList;
    protected int m_indexofLastProduct;
    protected int m_remainingProduct;  
    protected float m_manHours;
    protected float m_spentManHours;
    protected int m_dueDate;
    protected int failureCount;

    public bool WorkComplete => m_spentManHours >= m_manHours || WorkFailed;

    public bool WorkFailed => m_dueDate < GameState.GameDay + GameState.CompletetionTolerance; 

    /// <summary>
    /// Note this function should effect the quality of the building each day by a factor of materials and other features.
    /// </summary>
    /// <param name="">workers particpatating for the day.</param>
    public float ResolveDayAndCalculateCost(List<Worker> workees)
    {
        GenerateSortedListIfNeeded();
        float spentManhours = workees.Count * GameState.WorkHoursPerDay;

        float materialCost = CalcuateMaterialCost(spentManhours);
        m_spentManHours += spentManhours;

        return materialCost;
    }

    protected float CalcuateMaterialCost(float spentManhours)
    {
        //completedMaterials 



        return default;
    }


    protected void GenerateSortedListIfNeeded()
    {
        if (m_sortedbyCostMaterialList != default)
            return;
        m_sortedbyCostMaterialList = m_actualMaterialList.ToList();
        m_sortedbyCostMaterialList.Sort((first, second) => { return first.Material.Cost.CompareTo(second.Material.Cost); });
    }



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
        //m_startDate = WorldState.Instance.Day;

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
            ProductOrder po = new ProductOrder(m_building);
            quantityofmaterials += po.Quantity;
            totalbuildingmaterialscost += po.Material.Cost * po.Quantity;
            m_desiredMaterialList.Add(po);
        }


        //determine manhours
        m_manHours = Mathf.Ceil(quantityofmaterials / GameState.HourProductivity);


        //determine due date
        m_dueDate = GameState.GameDay * (int) Random.Range(m_manHours / GameState.WorkHoursPerDay / Mathf.Log(GameState.LogImpact, m_building.Value), m_manHours / GameState.WorkHoursPerDay);// m_startDate+Mathf.CeilToInt(m_manHours / (m_numberOfWorkers*8.0f)); //start date plus 8 man hours/day per worker


        //determine base cost
        m_baseCost =  (m_manHours * GameState.ManHourSurchange * m_building.Value) + totalbuildingmaterialscost;

        return true;
    }


    /// <summary>
    /// Lets the player predict impact aside from worker factor
    /// </summary>
    /// <returns>new integrity value for building at current configuration</returns>
    public float PredictIntegrityImpact(List<Worker> workees)
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

        m_actualMaterialList[actualMaterialIndex].SetLock(Lock);
    }

    /// <summary>
    /// Quantity of an item can never be less than one
    /// </summary>
    /// <param name="actualMaterialIndex"></param>
    /// <param name="newMaterialCount"></param>
    public void UpdateQuantity(int actualMaterialIndex, int newMaterialCount)
    {
        if (newMaterialCount < 1)
            return;

        uint currentQuanity = m_actualMaterialList[actualMaterialIndex].Quantity;
        m_actualMaterialList[actualMaterialIndex].UpdateQuanity((uint)newMaterialCount);

        Redistribute(actualMaterialIndex, currentQuanity - newMaterialCount);
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
        //this checks if the second material is the same core material type as the first one
        Material.MaterialType maskedValue = m_actualMaterialList[firstMaterialIndex].Material.MaterialFlags
                                         & Material.MaterialType.AllMaterials;

        return m_actualMaterialList[secondMaterialIndex].Material.MaterialFlags.HasFlag(maskedValue); 
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

        float ReDist = m_actualMaterialList[actualMaterialIndex].Quantity;
        Material MatType = m_actualMaterialList[actualMaterialIndex].Material;

        Redistribute(actualMaterialIndex, ReDist);
        m_actualMaterialList.RemoveAt(actualMaterialIndex);
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
        int startingPoint = actualMaterialIndex;
        while (AreTheseTheSameMaterial(--startingPoint, actualMaterialIndex))
        {
            if (!m_actualMaterialList[startingPoint].IsLocked)
                indexesOfSameMats.Add(startingPoint);
        }
        int endPoint = actualMaterialIndex;
        while (AreTheseTheSameMaterial(++endPoint, actualMaterialIndex))
        {
            if (!m_actualMaterialList[endPoint].IsLocked)
                indexesOfSameMats.Add(endPoint);
        }
        //if there are no unlocked values force it to the first one
        if (indexesOfSameMats.Count == 0)
        {
            //this only happens if there is only the base left. force-load mats back to it, ignoring lock
            m_actualMaterialList[startingPoint + 1].UpdateQuanity(m_actualMaterialList[startingPoint + 1].Quantity + (uint)amountToRedistribute);
            return;
        }

        uint sharedAmount = (uint) (amountToRedistribute / indexesOfSameMats.Count);
        uint leftovers = (uint) (amountToRedistribute % indexesOfSameMats.Count);
        foreach (int sharedMat in indexesOfSameMats)
            m_actualMaterialList[sharedMat].UpdateQuanity(m_actualMaterialList[sharedMat].Quantity + sharedAmount + (uint)((leftovers-- >= 0) ? 1 : 0));

    }



}

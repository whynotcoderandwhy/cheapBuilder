using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class WorkOrderTests : WorkOrder
{
    [Test]
    public void SplitTest()
    {

        m_actualMaterialList = new List<ProductOrder>();

        ProductOrder p = new ProductOrder();
        p.m_quantity = 5;
        p.m_material = new Material();
        Material.MaterialType mt = new Material.MaterialType();
        mt = (Material.MaterialType.Adhesive | Material.MaterialType.Damaged);
        p.m_material.TestingInit(mt, "1", 3, 100);
        m_actualMaterialList.Add(p);


        Assert.That(m_actualMaterialList[0].m_quantity == 5, string.Format("Started with {0}", m_actualMaterialList[0].m_quantity));
        Assert.IsTrue(SplitActualList(m_actualMaterialList.IndexOf(p)));
        Assert.That(m_actualMaterialList[0].m_quantity == 3, string.Format("Should be 3, got {0}", m_actualMaterialList[0].m_quantity));
        Assert.That(m_actualMaterialList[1].m_quantity == 2, string.Format("Should be 2, got {0}", m_actualMaterialList[1].m_quantity));

    }








}



/*
    public float PredictIntegrityImpact()


    public void LockQuantity(int actualMaterialIndex, bool Lock = true)

    public void UpdateQuantity(int actualMaterialIndex, int newMaterialCount)

    protected bool AreTheseTheSameMaterial(int firstMaterialIndex, int secondMaterialIndex)

    public bool RemoveActualMaterial(int actualMaterialIndex)

    protected void Redistribute(int actualMaterialIndex, float amountToRedistribute)
*/
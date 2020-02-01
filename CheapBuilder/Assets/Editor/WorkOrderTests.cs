using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class WorkOrderTests : WorkOrder
{

    protected void setupTestingOrder()
    {

        m_actualMaterialList = new List<ProductOrder>();

        ProductOrder p = new ProductOrder();
        p.m_quantity = 5;
        p.m_material = new Material();
        Material.MaterialType mt = new Material.MaterialType();
        mt = (Material.MaterialType.Adhesive | Material.MaterialType.Damaged);
        p.m_material.TestingInit(mt, "1", 3, 100);
        m_actualMaterialList.Add(p);
    }

    [Test]
    public void SplitTest()
    {
        setupTestingOrder();

        Assert.That(m_actualMaterialList[0].m_quantity == 5, string.Format("Started with {0}", m_actualMaterialList[0].m_quantity));
        Assert.IsTrue(SplitActualList(0));
        Assert.That(m_actualMaterialList[0].m_quantity == 3, string.Format("Should be 3, got {0}", m_actualMaterialList[0].m_quantity));
        Assert.That(m_actualMaterialList[1].m_quantity == 2, string.Format("Should be 2, got {0}", m_actualMaterialList[1].m_quantity));

    }

    [Test]
    public void LockTest()
    {
        setupTestingOrder();
        LockQuantity(0, true);
        Assert.That(m_actualMaterialList[0].m_quantityLocked == true, string.Format("Should be true, got {0}", m_actualMaterialList[0].m_quantityLocked));
        LockQuantity(0, false);
        Assert.That(m_actualMaterialList[0].m_quantityLocked == false, string.Format("Should be false, got {0}", m_actualMaterialList[0].m_quantityLocked));
    }

    [Test]
    public void UpdateQuantityTest()
    {
        setupTestingOrder();
        UpdateQuantity(0, 1332);

        Assert.That(m_actualMaterialList[0].m_quantity == 1337, string.Format("Should be 1337, got {0}", m_actualMaterialList[0].m_quantity));
        UpdateQuantity(0, -1332);

        Assert.That(m_actualMaterialList[0].m_quantity == 5, string.Format("Should be 5, got {0}", m_actualMaterialList[0].m_quantity));
    }

    [Test]
    public void SameMatTest()
    {
        setupTestingOrder();
        ProductOrder p = new ProductOrder();
        p.m_quantity = 50;
        p.m_material = new Material();
        Material.MaterialType mt = new Material.MaterialType();
        mt = (Material.MaterialType.Mechanical | Material.MaterialType.Branded);
        p.m_material.TestingInit(mt, "2", 5, 700);
        m_actualMaterialList.Add(p);




        Assert.That(m_actualMaterialList[0].m_material.MaterialFlags == (Material.MaterialType.Adhesive | Material.MaterialType.Damaged), string.Format("Should be true"));
        Assert.That(m_actualMaterialList[1].m_material.MaterialFlags == mt, string.Format("Should be true"));

        Assert.That(AreTheseTheSameMaterial(0, 1)==false, string.Format("Should be false"));

        Assert.That(m_actualMaterialList[0].m_material.MaterialFlags == (Material.MaterialType.Adhesive | Material.MaterialType.Damaged), string.Format("Should be true"));
        Assert.That(m_actualMaterialList[1].m_material.MaterialFlags == mt, string.Format("Should be true"));
    }

    [Test]
    public void RemoveMatTest()
    {
        setupTestingOrder();
        SplitActualList(0);
        Assert.That(!RemoveActualMaterial(0), "didn't properly detect that this is the origin");
        Assert.That(RemoveActualMaterial(1), "didn't properly delete the new mat");

    }

    [Test]
    public void RedistTest()
    {
        setupTestingOrder();
        Redistribute(m_actualMaterialList.Count, 500); //using Count so that it'll distribute to the last item on list
        Assert.That(m_actualMaterialList[m_actualMaterialList.Count-1].m_quantity == 505, "didn't properly distribute");


    }






}
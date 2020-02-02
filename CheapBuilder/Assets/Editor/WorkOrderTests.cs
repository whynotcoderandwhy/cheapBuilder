using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class WorkOrderTests : WorkOrder
{

    [Test]
    public void initializationTest()
    {
        Building b = new Building();
        b.Init(50, 50);

        //GameObject ws = GameObject.FindObjectOfType<WorldState>().gameObject;
        //Assert.That(ws!=default, "couldn't init world state");

        Assert.That(!CreateInitialWorkOrderSpecs(b), string.Format("Somehow started without workers"));
        m_numberOfWorkers = 2;
        Assert.That(CreateInitialWorkOrderSpecs(b), string.Format("Failed to start after workers added"));

        Assert.That(m_desiredMaterialList != default, string.Format("Desired Materials List {0}", m_desiredMaterialList.Count));
        Assert.That(m_actualMaterialList != default, string.Format("Actual Materials List {0}", m_actualMaterialList.Count));
        Assert.That(m_desiredMaterialList.Count != 0, string.Format("Desired Materials List Size {0}", m_desiredMaterialList.Count));

        Assert.That(m_manHours != 0, string.Format("Manhours {0}", m_manHours));
        Assert.That(m_dueDate >0, string.Format("Due Date {0}", m_dueDate));
        Assert.That(m_baseCost >0, string.Format("Base Cost {0}", m_baseCost));

    }











    protected void setupTestingOrder()
    {

        m_actualMaterialList = new List<ProductOrder>();

        ProductOrder p = new ProductOrder(new Material(Material.MaterialType.Adhesive | Material.MaterialType.Damaged, "1", 3, 100),5);
        m_actualMaterialList.Add(p);
    }

    [Test]
    public void SplitTest()
    {
        setupTestingOrder();

        Assert.That(m_actualMaterialList[0].Quantity == 5, string.Format("Started with {0}", m_actualMaterialList[0].Quantity));
        Assert.IsTrue(SplitActualList(0));
        Assert.That(m_actualMaterialList[0].Quantity == 3, string.Format("Should be 3, got {0}", m_actualMaterialList[0].Quantity));
        Assert.That(m_actualMaterialList[1].Quantity == 2, string.Format("Should be 2, got {0}", m_actualMaterialList[1].Quantity));

    }

    [Test]
    public void LockTest()
    {
        setupTestingOrder();
        LockQuantity(0, true);
        Assert.That(m_actualMaterialList[0].IsLocked == true, string.Format("Should be true, got {0}", m_actualMaterialList[0].IsLocked));
        LockQuantity(0, false);
        Assert.That(m_actualMaterialList[0].IsLocked == false, string.Format("Should be false, got {0}", m_actualMaterialList[0].IsLocked));
    }

    [Test]
    public void UpdateQuantityTest()
    {
        setupTestingOrder();
        UpdateQuantity(0, 1332);

        Assert.That(m_actualMaterialList[0].Quantity == 1337, string.Format("Should be 1337, got {0}", m_actualMaterialList[0].Quantity));
        UpdateQuantity(0, -1332);

        Assert.That(m_actualMaterialList[0].Quantity == 5, string.Format("Should be 5, got {0}", m_actualMaterialList[0].Quantity));
    }

    [Test]
    public void SameMatTest()
    {
        setupTestingOrder();
        Material.MaterialType mt = Material.MaterialType.Mechanical | Material.MaterialType.Branded;
        ProductOrder p = new ProductOrder(new Material(mt, "2", 5, 700),50);
        m_actualMaterialList.Add(p);




        Assert.That(m_actualMaterialList[0].Material.MaterialFlags == (Material.MaterialType.Adhesive | Material.MaterialType.Damaged), string.Format("Should be true"));
        Assert.That(m_actualMaterialList[1].Material.MaterialFlags == mt, string.Format("Should be true"));

        Assert.That(AreTheseTheSameMaterial(0, 1)==false, string.Format("Should be false"));

        Assert.That(m_actualMaterialList[0].Material.MaterialFlags == (Material.MaterialType.Adhesive | Material.MaterialType.Damaged), string.Format("Should be true"));
        Assert.That(m_actualMaterialList[1].Material.MaterialFlags == mt, string.Format("Should be true"));
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
        Assert.That(m_actualMaterialList[m_actualMaterialList.Count-1].Quantity == 505, "didn't properly distribute");


    }






}
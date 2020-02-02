using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class WorkerManagerTesting : WorkerManager
{
    [OneTimeSetUp]
    public void InitWork()
    {
        GameState.CompetenceRatio = .03f;
        GameState.LuckModifier = 1.1f;
    }

    [Test]
    public void TestingHigheringPool()
    {
        List<Worker> possiblity = GenerateHigheringPool(20, .5f);
        foreach (Worker workee in possiblity)
            Assert.IsTrue(workee.QualityModifier < 1);

         possiblity = GenerateHigheringPool(20, 1f);
        foreach (Worker workee in possiblity)
            Assert.IsTrue(workee.QualityModifier < 1);

        possiblity = GenerateHigheringPool(40, 1f);
        foreach (Worker workee in possiblity)
            Assert.IsTrue(workee.QualityModifier < 2);

        possiblity = GenerateHigheringPool(40, 2f);
        foreach (Worker workee in possiblity)
            Assert.IsTrue(workee.QualityModifier < 2);
    }

    [Test]
    public void TestHigheringAndFiringofWorker()
    {
        List<Worker> possiblity = GenerateHigheringPool(20, .5f);
        Assert.AreEqual(base.m_workerPool.Count, 0);
        base.NewWorker(possiblity[1]);

        Assert.AreEqual(base.m_workerPool.Count, 1);
        Assert.AreEqual(possiblity[1], m_workerPool[0]);
        base.FireWorker(possiblity[1]);
        Assert.AreEqual(base.m_workerPool.Count, 0);
    }

    [Test]
    public void TestToggingBehavoir()
    {
        List<Worker> possiblity = GenerateHigheringPool(20, .5f);
        Assert.AreEqual(base.m_workerPool.Count, 0);
        base.NewWorker(possiblity[1]);

        Assert.IsFalse(base.isWorking(possiblity[1]));
        Assert.IsTrue(base.SwitchWorker(possiblity[1],true));
        Assert.IsTrue(base.isWorking(possiblity[1]));
        base.SwitchWorker(possiblity[1], true);
        Assert.IsTrue(base.isWorking(possiblity[1]));
        base.SwitchWorker(possiblity[1], false);
        Assert.IsFalse(base.isWorking(possiblity[1]));
        base.SwitchWorker(possiblity[1], false);
        Assert.IsFalse(base.isWorking(possiblity[1]));
        base.SwitchWorker(possiblity[1], true);
        Assert.IsTrue(base.isWorking(possiblity[1]));
        base.FireWorker(possiblity[1]);
    }

}


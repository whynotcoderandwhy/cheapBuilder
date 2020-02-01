using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class MaterialsTest : Material
{

    [Test]
    public void GeneratorTest()
    {
        Material m = GenerateRandomMaterial(50);
        Assert.That(m.Cost != 0, string.Format("Cost {0}", m.Cost));
        Assert.That(m.Name != null, string.Format("Name {0}", m.Name));
        Assert.That(m.Quality != 0, string.Format("Quality {0}", m.Quality));
        Assert.That((m.MaterialFlags | MaterialType.AllMaterials) != 0, string.Format("MaterialFlags {0}", m.MaterialFlags));
    }




}
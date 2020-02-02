using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListOfAllMaterials : MonoBehaviour
{
    [SerializeField]
    protected List<Material> m_allMaterials;



    public Material GetMaterialOfFlag(Material.MaterialType mattype)
    {
        foreach(Material m in m_allMaterials)
        {
            if (m.MaterialFlags == mattype)
                return m;
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MaterialSlotPrefab : MonoBehaviour
{
    [SerializeField]
    protected Text m_CostField;
    [SerializeField]
    protected Text m_QualityField;
    [SerializeField]
    protected Text m_QuantityField;
    [SerializeField]
    protected Dropdown m_dropdown;

    [SerializeField]
    protected List<Material> m_materials;
    [SerializeField]
    protected ProductOrder m_productOrder;

    public bool CreateOptions(List<Material> materials)
    {
        m_dropdown.options.Clear();
        m_materials = materials;
        if (m_materials == default)
            return false;
        foreach (Material m in m_materials)
        {
            Dropdown.OptionData OD = new Dropdown.OptionData();
            OD.text = m.Name;
            m_dropdown.options.Add(OD);
        }
        if (m_dropdown.options.Count <= 0)
            return false;
        return true;
    }

    public void UpdateCurrentSlot()
    {
        //m_dropdown.value;
        m_dropdown.RefreshShownValue();
        m_dropdown.Select();
        m_CostField.text = (m_productOrder.Quantity*m_materials[m_dropdown.value].Cost).ToString();
        m_QualityField.text = m_materials[m_dropdown.value].Quality.ToString();
        m_QuantityField.text = m_productOrder.Quantity.ToString();
    }

    public bool Init(ProductOrder po)
    {
        if (po == default)
            return false;

        Material.MaterialType type = po.Material.MaterialFlags;
        ListOfAllMaterials list = GameObject.FindObjectOfType<ListOfAllMaterials>();
        if (list == default)
            return false;
        List<Material> materialsOfType = list.GetAllMaterialsOfFlag(type);
        m_productOrder = po;
        return CreateOptions(materialsOfType);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentSlot();//until I ensure the event works 
    }
}

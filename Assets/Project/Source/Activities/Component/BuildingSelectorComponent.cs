using UnityEngine;

public class BuildingSelectorComponent : MonoBehaviour
{
    private BuildingView _selectedBuilding;
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                BuildingView building = hit.collider.GetComponent<BuildingView>();

                if (building != null)
                {
                    SelectBuilding(building);
                }
                else
                {
                    UnselectBuilding();
                }
            }
            else
            {
                UnselectBuilding();
            }
        }
    }

    private void SelectBuilding(BuildingView building)
    {
        if (_selectedBuilding != null && _selectedBuilding != building)
        {
            _selectedBuilding.BuildingUnSelected();
        }

        _selectedBuilding = building;
        _selectedBuilding.BuildingSelected();
    }

    private void UnselectBuilding()
    {
        if (_selectedBuilding != null)
        {
            _selectedBuilding.BuildingUnSelected();
            _selectedBuilding = null;
        }
    }
}

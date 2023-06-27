using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RTSUnitManager : MonoBehaviour
{
	[SerializeField]
	private UnitSpawner unitSpawner = new UnitSpawner();

	public MouseDrag mouseDrag;

	private List<UnitController> selectedUnitList;				// 플레이어가 클릭 or 드래그로 선택한 유닛
	public	List<UnitController> UnitList { private set; get; } // 맵에 존재하는 모든 유닛


	int _unitMask = 1 << (int)Define.Layer.Unit;

	public void Init()
	{
		GameObject root = GameObject.Find("@UninManager");
		if (root == null)
		{
			root = new GameObject { name = "@UninManager" };
			Object.DontDestroyOnLoad(root);
		}

			selectedUnitList = new List<UnitController>();
		UnitList		 = unitSpawner.SpawnUnits();

		mouseDrag = new MouseDrag();
		//mouseDrag.dragRectangle = GameObject.Find("Canvas/DragRectangle").GetComponent<RectTransform>();
		//mouseDrag.Init(); 		

		Managers.Input.UnitMouseAction -= OnMouseEvent; 
		Managers.Input.UnitMouseAction += OnMouseEvent; 
    }

	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch(evt)
        {
			case Define.MouseEvent.LeftPointerDown:
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				// 광선에 부딪히는 오브젝트가 있을 때 (=유닛을 클릭했을 때)
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, _unitMask))
				{
					if (hit.transform.GetComponent<UnitController>() == null) return;

					if (Input.GetKey(KeyCode.LeftShift))
					{
						this.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
					}
					else
					{
						this.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
					}
				}
				// 광선에 부딪히는 오브젝트가 없을 때
				else
				{
					if (!Input.GetKey(KeyCode.LeftShift))
					{
						this.DeselectAll();
					}
				}

				mouseDrag.ButtonStart(); 
				break;

			case Define.MouseEvent.Drag:
				mouseDrag.ButtonDown(); 
				break;

			case Define.MouseEvent.PointerUp:
				mouseDrag.ButtonUp();
				break;				
        }

		
	}

		public void AddUnit(UnitController unit)
    {
		UnitList.Add(unit);
    }

	/// <summary>
	/// 마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 모든 유닛 해제
		DeselectAll();

		SelectUnit(newUnit);
	}

	/// <summary>
	/// Shift+마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ShiftClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 유닛을 선택했으면
		if ( selectedUnitList.Contains(newUnit) )
		{
			DeselectUnit(newUnit);
		}
		// 새로운 유닛을 선택했으면
		else
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 마우스 드래그로 유닛을 선택할 때 호출
	/// </summary>
	public void DragSelectUnit(UnitController newUnit)
	{
		// 새로운 유닛을 선택했으면
		if ( !selectedUnitList.Contains(newUnit) )
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 선택된 모든 유닛을 이동할 때 호출
	/// </summary>
	public void MoveSelectedUnits(Vector3 end)
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].MoveTo(end);
		}
	}

	public void SelectAll()
    {
		// 먼저 기존에 선택된 유닛들의 선택을 해제
		DeselectAll();

		// 모든 유닛을 선택
		foreach (UnitController unit in UnitList)
		{
			SelectUnit(unit);
		}
	}

	/// <summary>
	/// 모든 유닛의 선택을 해제할 때 호출
	/// </summary>
	public void DeselectAll()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].DeselectUnit();
		}

		selectedUnitList.Clear();
	}

	/// <summary>
	/// 매개변수로 받아온 newUnit 선택 설정
	/// </summary>
	private void SelectUnit(UnitController newUnit)
	{
		// 유닛이 선택되었을 때 호출하는 메소드
		newUnit.SelectUnit();
		// 선택한 유닛 정보를 리스트에 저장
		selectedUnitList.Add(newUnit);
	}

	/// <summary>
	/// 매개변수로 받아온 newUnit 선택 해제 설정
	/// </summary>
	private void DeselectUnit(UnitController newUnit)
	{
		// 유닛이 해제되었을 때 호출하는 메소드
		newUnit.DeselectUnit();
		// 선택한 유닛 정보를 리스트에서 삭제
		selectedUnitList.Remove(newUnit);
	}

    public void Release()
    {
        // Unsubscribe from the event
        Managers.Input.UnitMouseAction -= OnMouseEvent;

        // Destroy all units in the selectedUnitList and clear the list
		if(selectedUnitList != null)
		{
			foreach (var unit in selectedUnitList)
			{
				if(unit != null)
				Destroy(unit.gameObject);
			}
			selectedUnitList.Clear();
		}

        //// Destroy all units in the UnitList and clear the list
        //foreach (var unit in UnitList)
        //{
        //    Destroy(unit.gameObject);
        //}
        //UnitList.Clear();

        // Release the mouseDrag instance
        //mouseDrag = null;
    }
}


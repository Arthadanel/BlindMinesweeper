using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineField : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    private const int SideLength = 10;
    private const int Offset = 1;
    private float xStart = -SideLength / 2f + Offset / 2f;
    private float yStart = SideLength / 2f;
    private readonly List<Cell> _cells = new List<Cell>();

    private const int SecondTypeMinesMax = (SideLength * SideLength) / 2;
    private int _secondTypeMines = SecondTypeMinesMax;
    
    public Action<Color> DetonateCell;
    public void GenerateField()
    {
        if(_cells.Count>0){ClearField();}
        bool bgCheck = false;
        int mineCount = SideLength * SideLength;
        float x = xStart;
        float y = yStart;
        for (int i = 0; i < mineCount; i++)
        {
            _cells.Add(Instantiate(cellPrefab, new Vector3(x, y), Quaternion.identity,this.transform).GetComponent<Cell>());
            if ((i+1) % SideLength == 0)
            {
                y -= Offset;
                x = xStart;
            }
            else
            {
                x += Offset;
            }

            if ((SideLength % 2 != 0) || i % SideLength != 0)
                bgCheck = !bgCheck;

            bool mineCheck = false;
            if (_secondTypeMines > 0)
            {
                if (_secondTypeMines == SideLength * SideLength - i)
                    mineCheck = true;
                else
                    mineCheck = Random.Range(0, 2) == 0;
                
                if (mineCheck)
                {
                    _secondTypeMines--;
                }
            }

            _cells[i].SetColors(bgCheck, mineCheck);
        }

        _secondTypeMines = SecondTypeMinesMax;
    }
    
    private void ClearField()
    {
        foreach (var cell in _cells)
        {
            Destroy(cell.GetComponent<Transform>().gameObject);
        }
        _cells.Clear();
    }

    public void ChangeMinesVisibility(bool hide)
    {
        foreach (var cell in _cells)
        {
            cell.ChangeColor(hide);
        }
    }
}

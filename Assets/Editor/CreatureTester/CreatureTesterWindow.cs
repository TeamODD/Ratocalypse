using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using TeamOdd.Ratocalypse.TestScripts;
using UnityEditor;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

public class CreatureTesterWindow : EditorWindow
{
    [MenuItem("Window/CreatureTester")]
    public static void ShowWindow()
    {
        GetWindow<CreatureTesterWindow>("CreatureTester");
    }


    private TestObjectCreator _creator;
    private TileSelector _tileSelector;
    private Map _map;
    private Vector2Int _coord;
    private PlacementObject _placementObject;
    private DirectionalMovement _movement;
    private void OnGUI()
    {

        
        _creator = (TestObjectCreator)EditorGUILayout.ObjectField("Creator", _creator, typeof(TestObjectCreator), true);
        
        _coord = EditorGUILayout.Vector2IntField("Coord", _coord);

        if (GUILayout.Button("create"))
        {
            _creator.CreateObject(_coord);
        }

        
        _map = (Map)EditorGUILayout.ObjectField("Map", _map, typeof(Map), true);
        if(_map!=null)
        {
            _tileSelector = _map.GetComponent<TileSelector>();
        }
        
        _placementObject = (PlacementObject)EditorGUILayout.ObjectField("PlacementObject", _placementObject, typeof(PlacementObject), true);
        

        if (GUILayout.Button("Move"))
        {
            Pattern pattern = new Pattern(new List<Vector2Int> { new Vector2Int(0, 1),new Vector2Int(1, 0),new Vector2Int(-1, 0),new Vector2Int(0, -1) });
            _map.MapData.Print();
            Placement currentPlacement = _map.MapData.GetPlacement(_placementObject.Coord);
            _movement = new DirectionalMovement(_map.MapData.GetPlacement(_placementObject.Coord) ,_map.MapData, pattern);
            var selection = _movement.CreateSelection((ShapedCoordList currentCandidates,int index) =>
            {
                currentPlacement.SetCoord(currentCandidates.GetCoord(index));
            },
            (Placement placement)=>{
                ((CreatureData)currentPlacement).Attack((IDamageable)placement,10);
            });

            _tileSelector.Select(selection);
        }


    }
}
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.TestScripts;
using UnityEditor;
using UnityEngine;

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
    private void OnGUI()
    {

        
        _creator = (TestObjectCreator)EditorGUILayout.ObjectField("Creator", _creator, typeof(TestObjectCreator), true);
        
        _coord = EditorGUILayout.Vector2IntField("Coord", _coord);

        if (GUILayout.Button("create"))
        {
            _creator.CreateObject(_coord);
        }

        _tileSelector = (TileSelector)EditorGUILayout.ObjectField("TileSelector", _tileSelector, typeof(TileSelector), true);
        _map = (Map)EditorGUILayout.ObjectField("Map", _map, typeof(Map), true);

        if (GUILayout.Button("Select"))
        {
            MapAnalyzer analyzer = new MapAnalyzer(_map.MapData);
            Debug.Log(_map.MapData);
            _tileSelector.SelectAndMove(analyzer.Where((Vector2Int coord)=>coord.x%2==coord.y%2));
        }


    }
}
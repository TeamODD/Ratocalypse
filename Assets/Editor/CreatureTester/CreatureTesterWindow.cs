using TeamOdd.Ratocalypse.Creature;
using UnityEditor;
using UnityEngine;

public class CreatureTesterWindow : EditorWindow
{
    [MenuItem("Window/CreatureTester")]
    public static void ShowWindow()
    {
        GetWindow<CreatureTesterWindow>("CreatureTester");
    }

    private Creature _creature;
    private CreatureFactory _creatureFactory;

    private Vector2Int _coord;
    private void OnGUI()
    {

        
        _creatureFactory = (CreatureFactory)EditorGUILayout.ObjectField("CreatureFactory", _creatureFactory, typeof(CreatureFactory), true);
        
        _coord = EditorGUILayout.Vector2IntField("Coord", _coord);

        if (GUILayout.Button("create"))
        {
            _creatureFactory.CreateCreature(_coord);
        }
        
        _creature = (Creature)EditorGUILayout.ObjectField("Creature", _creature, typeof(Creature), true);


        if (GUILayout.Button("up"))
        {
            var coord = _creature.Coord;
            coord.y++;
            _creature.MoveTo(coord);
        }
        if (GUILayout.Button("down"))
        {
            var coord = _creature.Coord;
            coord.y--;
            _creature.MoveTo(coord);
        }
        if (GUILayout.Button("left"))
        {
            var coord = _creature.Coord;
            coord.x--;
            _creature.MoveTo(coord);
        }
        if (GUILayout.Button("right"))
        {
            var coord = _creature.Coord;
            coord.x++;
            _creature.MoveTo(coord);
        }
    }
}
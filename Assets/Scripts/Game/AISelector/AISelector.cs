
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using System.Linq;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.CreatureLib;
using System.Collections;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.AISelectorLib
{
    public class AISelector : MonoBehaviour, IMapSelector, ICardSelector
    {

        private MapData _mapData;
        private CreatureData _caster;

        public void Initialize(MapData mapData, CatData catData)
        {
            _mapData = mapData;
            _caster = catData;
        }

        public DeckData GetTarget()
        {
            throw new System.NotImplementedException();
        }

        public void Select(Selection<ShapedCoordList> coordSelection, Selection<List<MapData.Placement>> placementSelection)
        {
            var enemies = placementSelection.GetCandidates().Where((placement) =>
            {
                if(placement is CreatureData)
                {
                    return Utils.IsEnemy(_caster, placement as CreatureData);
                }
                return false;
            }).Cast<CreatureData>();
            enemies = enemies.Where((enemy) =>
            {
                var (has, _) = enemy.HasEffect("Stealth");
                return !has;
            });
            enemies = enemies.OrderBy((_) =>
            {
                return Random.Range(0, 100);
            });
            if (enemies.Count() == 0)
            {
                if (coordSelection.GetCandidates().Count == 0)
                {
                    coordSelection.Select(-1);
                    return;
                }
                coordSelection.Select(Random.Range(0, coordSelection.GetCandidates().Count));
                return;
            }

            enemies = enemies.OrderBy((enemy) =>
            {
                return MapAnalyzer.GetDistance(enemy, _caster);
            });

            var enemy = enemies.First();
            var index = placementSelection.GetCandidates().IndexOf(enemy);
            placementSelection.Select(index);
        }

        public void Select(Selection<ShapedCoordList> selection)
        {
            if (selection.GetCandidates().Count == 0)
            {
                selection.Select(-1);
                return;
            }
            selection.Select(Random.Range(0, selection.GetCandidates().Count));
        }

        public void Select(Selection<List<MapData.Placement>> selection)
        {
            var enemies = selection.GetCandidates().Where((placement) =>
            {
                if(placement is CreatureData)
                {
                    return Utils.IsEnemy(_caster, placement as CreatureData);
                }
                return false;
            }).Cast<CreatureData>();
            enemies = enemies.Where((enemy) =>
            {
                var (has, _) = enemy.HasEffect("Stealth");
                return !has;
            });
            enemies = enemies.OrderBy((_) =>
            {
                return Random.Range(0, 100);
            });
            if (enemies.Count() == 0)
            {
                selection.Select(-1);
                return;
            }

            enemies.OrderBy((enemy) =>
            {
                return MapAnalyzer.GetDistance(enemy, _caster);
            });

            var enemy = enemies.First();
            var index = selection.GetCandidates().IndexOf(enemy);
            selection.Select(index);
        }

        public void Select(Selection<List<int>> selection)
        {
            StartCoroutine(SlowSelect(selection));
        }

        private IEnumerator SlowSelect(Selection<List<int>> selection)
        {
            yield return new WaitForSeconds(1);
            selection.Select(0);
        }

        public void SetTarget(CreatureData caster, System.Action callback)
        {
            _caster = caster;
            callback?.Invoke();
        }
    }
}
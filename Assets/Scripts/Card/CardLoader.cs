using TeamOdd.Ratocalypse.CardLib.Cards.Templates;
using UnityEngine;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardLoader : MonoBehaviour
    {
        private CardOriginData _cardOriginData;
        [SerializeField]
        private string _path;
        private void Awake()
        {
            _cardOriginData = CardOriginData.Instance;
            AddCards();
        }

        private void AddCard(CardData data)
        {
            _cardOriginData.AddData(data);
        }

        private void AddCards()
        {
            var ratAttackData = new DataValue(2, MoveOrAttackRangeType.Rook);
            var ratAttack = new MoveOrAttackCardData(LoadTexture("RatAttack"), 33, ratAttackData, 0);
            AddCard(ratAttack);

            var ratSurvivalInstictData = new DataValue(2, MoveOrAttackRangeType.Knight);
            var ratSurvivalInstict = new MoveOrAttackCardData(LoadTexture("RatSurvivalInstict"), 34, ratSurvivalInstictData, 1);
            AddCard(ratSurvivalInstict);

            var ratRapidMoveData = new DataValue(2, MoveOrAttackRangeType.King);
            var ratRapidMove = new MoveOrAttackCardData(LoadTexture("RatRapidMove"), 35, ratRapidMoveData, 2);
            AddCard(ratRapidMove);

            var ratDefenseData = new DataValue(2, MoveOrAttackRangeType.Bishop);
            var ratDefense = new MoveOrAttackCardData(LoadTexture("RatDefense"), 36, ratDefenseData, 3);
            AddCard(ratDefense);

            var ratBasicTrainingData = new DataValue(2, MoveOrAttackRangeType.Bishop);
            var ratBasicTraining = new MoveOrAttackCardData(LoadTexture("RatBasicTraining"), 1, ratBasicTrainingData, 4);
            AddCard(ratBasicTraining);
        }

        private Texture2D LoadTexture(string name)
        {
            return Resources.Load<Texture2D>(_path + "/" + name);
        }
    }
}

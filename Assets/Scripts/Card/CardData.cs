namespace TeamOdd.Ratocalypse.Card
{
    public class CardData
    {
        public long CardDataId { get; }
        public CardDataValue DataValue { get; }

        public CardData(long cardDataId)
        {
            CardDataId = cardDataId;
            DataValue = new CardDataValue();
        }

        public CardData(long cardDataId, CardDataValue dataValue) : this(cardDataId)
        {
            DataValue = dataValue;
        }

        public virtual CardData Clone()
        {
            return new CardData(CardDataId, DataValue.Clone());
        }
    }

    public class CardDataValue
    {
        public int Cost { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public CardDataValue()
        {
            Cost = 0;
            Title = "";
            Description = "";
        }

        public CardDataValue(int cost, string title, string description)
        {
            Cost = cost;
            Title = title;
            Description = description;
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public virtual CardDataValue Clone()
        {
            return new CardDataValue(Cost, Title, Description);
        }
    }
}
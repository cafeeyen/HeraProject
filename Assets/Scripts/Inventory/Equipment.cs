public interface Equipment : Items
{
    Rarity rarity{get;set;}
	EquipmentType equipmentType{get;set;}

    int id { get; set; }
    float atk{get;set;}
	float def{get;set;}
	float hp{get;set;}
}

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public enum EquipmentType
{
    None,
    Hat,
    Glove,
    Suit
}

public enum Glove
{
    Pohpae01
}

public enum Hat
{
    Helmet01,
    PaperHat,
    StrawHat
}

public enum Suit
{
    Robe01
}
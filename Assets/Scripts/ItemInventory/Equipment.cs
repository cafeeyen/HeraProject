public interface Equipment : Items {
	int id{get;set;}
	
    Rarity rarity{get;set;}
	EquipmentType equipmentType{get;set;}

	float atk{get;set;}
	float def{get;set;}
	float hp{get;set;}
}


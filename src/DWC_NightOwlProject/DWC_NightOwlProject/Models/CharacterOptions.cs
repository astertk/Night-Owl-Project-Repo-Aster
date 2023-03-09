namespace DWC_NightOwlProject.Models;

public class CharacterOptions
{
    public static readonly string[] Abilities={"Str","Dex","Con","Int","Wis","Cha"};
    public static readonly string[] RaceOptions={"Dragonborn","Dwarf","Elf","Gnome","Half-Elf","Halfling","Half-Orc","Human","Tiefling"};
    public static readonly string[] ClassOptions={"Artificer","Barbarian","Bard","Cleric","Druid","Fighter","Monk","Paladin","Ranger",
                                                "Rogue","Sorcerer","Warlock","Wizard"};
    public static readonly int[,] StatBonuses=new int[9,6]{{2,0,0,0,0,1},{0,0,2,0,0,0},{0,2,0,0,0,0},{0,0,0,2,0,0},{0,0,0,0,0,2},
                    {0,2,0,0,0,0},{2,0,1,0,0,0},{1,1,1,1,1,1},{0,0,0,1,0,2}};
    public static readonly int[,] StatOrder=new int[13,6]{{5,1,2,0,3,4},{0,2,1,5,4,3},{5,1,2,3,4,0},{2,3,1,5,0,4},{4,2,1,3,0,5},{0,5,1,2,3,4},
                                            {3,1,2,4,0,5},{1,5,2,3,4,0},{4,1,2,3,0,5},{5,0,2,3,4,1},{5,1,2,4,3,0},{5,2,1,3,4,0},{5,1,2,0,3,4}};
    public static readonly string[] StrProficiencies= new string[] {"Strength Save","Athletics"};
    public static readonly string[] DexProficiencies= new string[] {"Dexterity Save","Acrobatics","Sleight of Hand","Stealth"};
    public static readonly string[] ConProficiencies= new string[] {"Constitution Save","Athletics"};
    public static readonly string[] IntProficiencies= new string[] {"Intelligence Save","Arcana","Investigation","Nature","History","Religion"};
    public static readonly string[] WisProficiencies= new string[] {"Wisdom Save","Animal Handling","Medicine","Insight","Perception","Survival"};
    public static readonly string[] ChaProficiencies= new string[] {"Charisma Save","Deception","Intimidation","Performance","Persuasion"};
}
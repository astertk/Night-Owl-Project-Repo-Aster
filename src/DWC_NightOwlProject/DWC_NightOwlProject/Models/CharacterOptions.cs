namespace DWC_NightOwlProject.Models;

public static class CharacterOptions
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
    private static readonly int maxLevel=20;
    private static readonly int numberOfClasses=ClassOptions.Length;
    private static readonly int numberOfRaces=RaceOptions.Length;
    private static readonly string featuresPath="features.txt";
    private static int[,] classFeatureIndices=new int[numberOfClasses,maxLevel];
    private static int[] raceFeatureIndices=new int[numberOfRaces];
    
    public static int ClassIndex(string s)
    {
        for(int i=0;i<ClassOptions.Length;i++)
        {
            if(s.Equals(ClassOptions[i]))
            {
                return i;
            }
        }
        return -1;
    }
    public static int RaceIndex(string s)
    {
        for(int i=0;i<RaceOptions.Length;i++)
        {
            if(s.Equals(RaceOptions[i]))
            {
                return i;
            }
        }
        return -1;
    }
    public static string Modifier(int i)
    {
        int mod;
        if(i>0&&i<=20)
        {
            if(i%2==0)
            {
                mod=(i-10)/2;
            }
            else
            {
                mod=(i-11)/2;
            }
            if(mod<0)
            {
                return ""+mod;
            }
            else
            {
                return "+"+mod;
            }
        }
        else
        {
            throw new ArgumentException("Ability score must a value 1-20");
        }
    }
    public static int ModifierValue(int i)
    {
        if(i>0&&i<=20)
        {
            if(i%2==0)
            {
                return (i-10)/2;
            }
            else
            {
                return (i-11)/2;
            }
        }
        else
        {
            throw new ArgumentException("Ability score must a value 1-20");
        }
    }
    public static int Roll(int i)
    {
        Random die=new Random();
        return die.Next(i);
    }
    public static int NewScore()
    {
        int[] rolls=new int[4];
        for(int i=0;i<4;i++)
        {
            rolls[i]=Roll(6)+1;
        }
        int lowest=0;
        for(int i=1;i<4;i++)
        {
            if(rolls[lowest]>rolls[i])
            {
                lowest=i;
            }
        }
        rolls[lowest]=0;
        return rolls.Sum();
    }
    public static void ConfigureFeatures()
    {

    }
}
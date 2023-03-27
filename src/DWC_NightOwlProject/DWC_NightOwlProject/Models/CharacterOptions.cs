using Microsoft.AspNetCore.Mvc.Rendering;

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
    public static IEnumerable<SelectListItem> RaceViewList;
    public static IEnumerable<SelectListItem> ClassViewList;
    public static readonly string Random="RANDOM";
    private static readonly int maxLevel=20;
    private static readonly int numberOfClasses=ClassOptions.Length;
    private static readonly int numberOfRaces=RaceOptions.Length;
    private static readonly string featuresPath="Models\\features.txt";
    private static int[,] classFeatureIndices=new int[numberOfClasses,maxLevel];
    private static int[] raceFeatureIndices=new int[numberOfRaces];
    private static List<string> featuresList;
    private static string classFeat="CLASSFEATURES";
    private static string raceFeat="RACEFEATURES";
    
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
    public static void ConfigureLists()
    {
        RaceViewList=raceList();
        ClassViewList=classList();
    }
    /*public static List<string> GetFeatures(string r, string c, int l)
    {
        List<string> features=new List<string>();
        features.Capacity=25;
        int rc=RaceIndex(r);
        int cls=ClassIndex(c);
        features.Add(featuresList[raceFeatureIndices[rc]]);
        for(int i=0;i<l;i++)
        {
            features.Add(featuresList[classFeatureIndices[cls,i]]);
        }
        return features;
    }*/
    /**public static void ConfigureFeatures()
    {
        RaceViewList=raceList();
        ClassViewList=classList();
        featuresList=new List<string>();
        featuresList.Capacity=(ClassOptions.Length*20)+RaceOptions.Length;
        IEnumerator<string> file=System.IO.File.ReadLines(featuresPath).GetEnumerator();
        bool hasNext=file.MoveNext();
        bool isClass=false;
        bool isRace=false;
        bool isFeature=false;
        int featIndex=0;
        int index=0;
        int level=0;
        while(hasNext)
        {
            string s=file.Current;
            isFeature=false;
            if(s.Equals(classFeat))
            {
                isClass=true;
                isRace=false;

            }
            else if(s.Equals(raceFeat))
            {
                isRace=true;
                isClass=false;
            }
            else if(ClassIndex(s)!=-1)
            {
                index=ClassIndex(s);
                level=0;
            }
            else if(RaceIndex(s)!=-1)
            {
                index=RaceIndex(s);
            }
            else
            {
                isFeature=true;
            }
            if(isFeature)
            {
                if(isClass)
                {
                    featuresList.Add(s);
                    classFeatureIndices[index,level]=featIndex;
                    level++;
                }
                else if(isRace)
                {
                    featuresList.Add(s);
                    raceFeatureIndices[index]=featIndex;
                }
                featIndex++;
            }
            hasNext=file.MoveNext();
        }
    }*/
    private static IEnumerable<SelectListItem> raceList()
    {
        List<SelectListItem> list= new List<SelectListItem>();
        list.Add(new SelectListItem("Random",Random));
        for(int i=0;i<RaceOptions.Count();i++)
        {
            list.Add(new SelectListItem(RaceOptions[i],RaceOptions[i]));
        }
        return list;
    }
    private static IEnumerable<SelectListItem> classList()
    {
        List<SelectListItem> list= new List<SelectListItem>();
        list.Add(new SelectListItem("Random",Random));
        for(int i=0;i<ClassOptions.Count();i++)
        {
            list.Add(new SelectListItem(ClassOptions[i],ClassOptions[i]));
        }
        return list;
    }
    /*private static void configureIndices()
    {
        int index=0;
        for(int i=0;i<classFeatureIndices.Length;i++)
        {
            for(int l=0;l<20;l++)
            {
                classFeatureIndices[i,l]=index;
                index++;
            }
        }
        for(int i=0;i<raceFeatureIndices.Length;i++)
        {
            raceFeatureIndices[i]=index;
            index++;
        }
        featuresList.Capacity=(ClassOptions.Length*20)+RaceOptions.Length;
    }*/
}
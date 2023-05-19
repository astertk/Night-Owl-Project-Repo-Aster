using System.Collections.Generic;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.Models;
public class SheetRandomizer
{
    public int[] Scores{get;set;}
    public string Name{get;set;}
    public string Race{get;set;}
    public string Class{get;set;}
    public int Level;
    public string[] Weapons{get;set;}
    public string Armor{get;set;}
    public bool Shield{get;set;}
    public int Proficiency=2;
    public Character Character;

    

    public String AllStats()
    {
        string stats="";
        for(int i=0;i<6;i++)
        {
            stats=stats+(CharacterOptions.Abilities[i]+": "+Scores[i]+", "+CharacterOptions.Modifier(Scores[i])+" ");
        }
        return stats;
    }
    public void Generate(String selectedRace, String selectedClass)
    {
        if(Name==null)
        {
            Name="Character";
        }
        Level=1;
        if(selectedRace.Equals(CharacterOptions.Random))
        {
            Race=CharacterOptions.RaceOptions[CharacterOptions.Roll(CharacterOptions.RaceOptions.Length)];   
        }
        else
        {
            Race=selectedRace;
        }
        if(selectedClass.Equals(CharacterOptions.Random))
        {
            Class=CharacterOptions.ClassOptions[CharacterOptions.Roll(CharacterOptions.ClassOptions.Length)]; 
        }
        else
        {
            Class=selectedClass;
        }
        int[] newStats=new int[6];
        for(int i=0;i<6;i++)
        {
            newStats[i]=CharacterOptions.NewScore();
        }
        int[] ordered=new int[6];
        for(int i=0;i<6;i++)
        {
            int maxIndex=0;
            for(int n=0;n<6;n++)
            {
                if(newStats[n]>newStats[maxIndex])
                {
                    maxIndex=n;
                }
            }
            ordered[i]=newStats[maxIndex];
            newStats[maxIndex]=0;
        }
        int c=CharacterOptions.ClassIndex(Class);
        for(int i=0;i<6;i++)
        {
            newStats[i]=ordered[CharacterOptions.StatOrder[c,i]];
        }
        
        Scores=newStats;
        int r=CharacterOptions.RaceIndex(Race);
        if(r==5)
        {
            int first=CharacterOptions.Roll(5);
            int second=CharacterOptions.Roll(5);
            Scores[first]=Scores[first]+1;
            Scores[second]=Scores[second]+1;
        }
        for(int i=0;i<6;i++)
        {
            Scores[i]=Scores[i]+CharacterOptions.StatBonuses[r,i];
        }
    }
    public string GetFile()
    {
        string file=Name+" Level "+Level+" "+Race+" "+Class+"\\n"+AllStats();
        List<string[]> weapons=GetWeapons();
        foreach(string[] s in weapons)
        {
            file=file+"\\n"+s[0]+": "+s[1];
        }
        return file;
    }
    public List<string[]> GetWeapons()
    {
        List<string[]> weapons=new List<string[]>();
        for(int i=1;i<4;i++)
        {
            string[] wep=new string[2];
            int quantity=Equipment.ClassEquipment[CharacterOptions.ClassIndex(Class),i,1];
            int wIndex=Equipment.ClassEquipment[CharacterOptions.ClassIndex(Class),i,0];
            if(quantity!=0)
            {
                wep[0]=Equipment.Weapons[wIndex]+(quantity>1?" ("+quantity+")":"");
                wep[1]=Equipment.WeaponDetails(wIndex,CharacterOptions.ModifierValue(Scores[1]),CharacterOptions.ModifierValue(Scores[0]),Proficiency);
                weapons.Add(wep);
            }
        }
        return weapons;
    }
    /*public List<string> GetFeatures()
    {
        return CharacterOptions.GetFeatures(Race,Class,Level);
    }*/
}
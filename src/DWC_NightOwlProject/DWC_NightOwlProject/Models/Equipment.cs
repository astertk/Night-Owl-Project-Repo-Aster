namespace DWC_NightOwlProject.Models;
public static class Equipment
{
    public static readonly string[] Weapons={"Dagger","Darts","Greataxe","Handaxe","Javelin","Light crossbow","Longbow","Longsword",
                                        "Mace","Quarterstaff","Rapier","Scimitar","Shortbow","Shortsword","Warhammer"};
    public static readonly string[] Armor={"Chain mail","Leather armor","Scale mail","Shield","Studded leather armor","Unarmored"};
    public static readonly int[] ArmorBonus={1,2,4,6,0};
    public static readonly string[] ArmorDexLimit={"No bonus","No limit","+2"};
    public static readonly bool[] ArmorDisadvantage={true,false};
    public static readonly int[] HitDie={4,6,8,10,12};
    public static readonly int[] Versatile={0,8,10};
    public static readonly string[] WeaponBonus={"Str","Dex","Finesse"};
    public static readonly string[] Range={"none","20/60","30/120","80/320","150/600"};
                                                //hit die, versatile hit die, ability modifier, range
    public static readonly int[,] WeaponStats=new int[,] {{0,0,2,1},{0,0,2,1},{4,0,0,0}, {1,0,0,1},{1,0,0,2},{2,0,1,3},{2,0,1,4},{2,2,0,0},
                                                {1,0,0,0},{1,1,0,0},{2,0,2,0},{1,0,2,0},{1,0,1,3},{1,0,2,0}, {2,2,0,0}}; 
                                                        //armor bonus, dex bonus limit, disadvantage
    public static readonly int[,] ArmorStats=new int[,] {{3,0,0},{0,1,1},{2,2,0},{1,1,0},{1,1,1},{4,1,1}};
                                                            //{{armor,shield},{weapon,quantity}...}
    public static readonly int[,,] ClassEquipment=new int[,,] {{{4,-1},{0,2},{5,1},{0,0}},  //artificer
                                                            {{5,-1},{2,1},{3,2},{4,5}}, //barbarian
                                                            {{1,-1},{11,1},{0,1},{0,0}}, //bard
                                                            {{2,1},{8,1},{5,1},{0,0}}, //cleric
                                                            {{1,1},{12,1},{0,0},{0,0}}, //druid
                                                            {{0,1},{5,1},{7,1},{0,0}}, //fighter
                                                            {{5,-1},{13,1},{1,10},{0,0}}, //monk
                                                            {{0,1},{7,1},{4,5},{0,0}}, //paladin
                                                            {{2,-1},{14,2},{6,1},{0,0}}, //ranger
                                                            {{1,-1},{10,1},{12,0},{0,2}}, //rogue
                                                            {{5,-1},{5,1},{0,2},{0,0}}, //sorcerer
                                                            {{1,-1},{5,1},{0,2},{3,1}}, //warlock
                                                            {{5,-1},{0,1},{0,0},{0,0}},};//wizard
    
    public static int GetAC(string armor, bool shield,int dex)
    {
        int index=armorIndex(armor);
        if(index==-1)
        {
            return -1;
        }
        else
        {
            int dexBonus=ArmorStats[index,1];
            int armorAC=ArmorStats[index,0];
            if(dexBonus==0)
            {
                return 10+armorAC;
            }
            else if(dexBonus==1)
            {
                return 10+armorAC+dex;
            }
            else
            {
                if(dex>2)
                {
                    return 12+armorAC;
                }
                else
                {
                    return 10+dex+armorAC;
                }
            }
        }
    }
    public static string WeaponDetails(int wIndex,int dex,int str,int prof)
    {
        int abilityBonus;
        int scoreOptions=WeaponStats[wIndex,2];
        if(scoreOptions==0)
        {
            abilityBonus=str;
        }
        else if(scoreOptions==1)
        {
            abilityBonus=dex;
        }
        else
        {
            if(dex>str)
            {
                abilityBonus=dex;
            }
            else
            {
                abilityBonus=str;
            }
        }
        string dmgString="1d"+HitDie[WeaponStats[wIndex,0]];
        int vers=Versatile[WeaponStats[wIndex,1]];
        if(vers==0)
        {
            dmgString=dmgString+"   ";
        }
        else
        {
            dmgString=dmgString+"/1d"+vers;
        }
        string rangeString;
        if(WeaponStats[wIndex,3]!=0)
        {
            rangeString="Range: "+Range[WeaponStats[wIndex,3]];
        }
        else
        {
            rangeString="            ";
        }
        int toHit=abilityBonus+prof;
        return "To hit: +"+toHit+"    Damage: "+dmgString+"+"+abilityBonus+""+rangeString;
        
    }
    private static int armorIndex(string a)
    {
        for(int i=0;i<Armor.Length;i++)
        {
            if(a.Equals(Armor[i]))
            {
                return i;
            }
        }
        return -1;
    }
    private static int weaponIndex(string w)
    {
        for(int i=0;i<Weapons.Length;i++)
        {
            if(w.Equals(Weapons[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
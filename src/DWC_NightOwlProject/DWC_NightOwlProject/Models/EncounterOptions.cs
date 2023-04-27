using Microsoft.AspNetCore.Mvc.Rendering;

namespace DWC_NightOwlProject.Models;
public class EncounterOptions
{
    public static string[] Biomes={"Arctic","Coastal","Desert","Grassland","Forest",
                            "Hill","Mountain","Swamp","Underground","Underwater","Urban"};
    public static string[] EncounterType={"Combat","Puzzle","Social"};

    
    public static IEnumerable<SelectListItem> BiomeList()
    {
        List<SelectListItem> list= new List<SelectListItem>();
        for(int i=0;i<Biomes.Count();i++)
        {
            list.Add(new SelectListItem(Biomes[i],Biomes[i]));
        }
        return list;
    }    
    public static IEnumerable<SelectListItem> TypeList()
    {
        List<SelectListItem> list= new List<SelectListItem>();
        for(int i=0;i<EncounterType.Count();i++)
        {
            list.Add(new SelectListItem(EncounterType[i],EncounterType[i]));
        }
        return list;
    }
}
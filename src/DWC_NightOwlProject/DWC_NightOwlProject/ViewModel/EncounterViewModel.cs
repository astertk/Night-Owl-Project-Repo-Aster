namespace DWC_NightOwlProject.ViewModel
{
public class EncounterViewModel
{
    public string Biome {get;set;}
    public string Type {get;set;}
    public string Result {get;set;}

    public string Prompt()
    {
        if(Biome!=null&&Type!=null)
        {
            return "Create a "+Type+" encounter for a Dungeons and Dragons game in a "+Biome+" environment";
        }
        return null;
    }
    public string Description()
    {
        return "A "+Type+" encounter in a "+Biome+" Environment";
    }
}
}
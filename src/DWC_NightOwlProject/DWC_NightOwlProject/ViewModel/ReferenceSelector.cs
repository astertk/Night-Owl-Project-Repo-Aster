using System.Collections.Generic;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace DWC_NightOwlProject.ViewModel
{
    public class ReferenceSelector
    {
        public List<Quest> Quests{get;set;}
        public List<Backstory> Backstories{get;set;}
        public List<Encounter> Encounters {get;set;}
        public string SelectedQuest {get;set;}
        public string SelectedBackstory {get;set;}
        public string SelectedEncounter{get;set;}
        public EncounterViewModel evm{get;set;}
        public string Prompt {get;set;}
        private string none="NONE";

        public ReferenceSelector()
        {
            evm=new EncounterViewModel();
        }
        public string promptEncounter()
        {
            string prompt="Generate an encounter for my Dungeons and Dragons campaign.";
            if(!SelectedBackstory.Equals(none))
            {
                prompt=prompt+"This encounter should be set in a world with this description: "+SelectedBackstory;
            }
            if(!SelectedQuest.Equals(none))
            {
                prompt=prompt+"This encounter should happen during this quest: "+SelectedQuest;
            }
            prompt=prompt+"Create a "+evm.Type+" encounter set in a "+evm.Biome+" environment using the provided information.";
            return prompt;
        }
        public string promptMap()
        {
            string prompt="Generate a map for my Dungeons and Dragons campaign.";
            if(!SelectedBackstory.Equals(none))
            {
                prompt=prompt+"This map should be for a location in a world with this description: "+SelectedBackstory;
            }
            if(!SelectedQuest.Equals(none))
            {
                prompt=prompt+"This map should be for a location in this quest: "+SelectedQuest;
            }
            if(!SelectedEncounter.Equals(none))
            {
                prompt=prompt+"This map should be for the location where this encounter happens: "+SelectedQuest;
            }
            prompt=prompt+"Create a map with the provided details and this description: "+Prompt;
            return prompt;
        }
        public string promptQuest()
        {
            string prompt="Generate a quest for my Dungeons and Dragons campaign.";
            if(!SelectedBackstory.Equals(none))
            {
                prompt=prompt+"This quest should be set in a world with this description: "+SelectedBackstory;
            }
            prompt=prompt+"This is what should happen in the quest: "+Prompt;
            return prompt;
        }
        public IEnumerable<SelectListItem> QuestList()
        {
            List<SelectListItem> list= new List<SelectListItem>();
            list.Add(new SelectListItem(none,none));
            for(int i=0;i<Quests.Count();i++)
            {
                list.Add(new SelectListItem(("Quest "+(i+1)),Quests[i].Completion));
            }
            return list;
        }
        public IEnumerable<SelectListItem> BackstoryList()
        {
            List<SelectListItem> list= new List<SelectListItem>();
            list.Add(new SelectListItem(none,none));
            for(int i=0;i<Backstories.Count();i++)
            {
                list.Add(new SelectListItem(("Backstory "+(i+1)),Backstories[i].Completion));
            }
            return list;
        }
        public IEnumerable<SelectListItem> EncounterList()
        {
            List<SelectListItem> list= new List<SelectListItem>();
            list.Add(new SelectListItem(none,none));
            for(int i=0;i<Encounters.Count();i++)
            {
                list.Add(new SelectListItem(("Encounter "+(i+1)),Encounters[i].Completion));
            }
            return list;
        }
    }
}
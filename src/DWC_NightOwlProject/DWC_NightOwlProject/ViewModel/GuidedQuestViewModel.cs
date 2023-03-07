using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.ViewModel
{
    public class GuidedQuestViewModel
    {
        public string OverallQuest {get;set;}
//        public string[] StepsList {get;set;}
//        public int ListLength{get;set;}
        public string CurrentStep {get;set;}
        public string Results{get;set;}
//        public string[] StepResults {get;set;}
//        public int ResultsLength {get;set;}
//        public static readonly int stepMax=3;
        private static readonly string submissionTemplate1="This is a quest in a Dungeons and Dragons game where ";
        private static readonly string submissionTemplate2=". Give me a description for the portion of the quest where ";

        public void AddResult(String s)
        {
            if(Results==null)
            {
                Results=s;
            }
            else
            {
                Results=Results+"\n \n"+s;
            }
        }

/*        public GuidedQuestViewModel()
        {
            StepResults=new string[stepMax];
            StepsList=new string[stepMax];
            ListLength=0;
            ResultsLength=0;
        }
        public bool AddStep()
        {
            if(ListLength<stepMax&&CurrentStep!=null)
            {
                StepsList[ListLength]=CurrentStep;
                ListLength++;
                CurrentStep=null;
                return true;
            }
            return false;
        }
        public bool AddResult(string s)
        {
            if(ResultsLength<stepMax)
            {
                StepResults[ResultsLength]=s;
                ResultsLength++;
                return true;
            }
            return false;
        }*/
        public string LatestStep()
        {
            return submissionTemplate1+OverallQuest+submissionTemplate2+CurrentStep;
        }
    }
}
using System;
using System.Text.RegularExpressions;

namespace DWC_NightOwlProject.ViewModel
{
    public class UserInputViewModel
    {
        public string UserInput {get;set;}
        private string illegalChars="/\'^@%;";

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(UserInput);
        }
        public string Sanitize(string s)
        {
            if(String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                foreach(char c in illegalChars)
                {
                    s.Replace(c,'\0');
                }
            }
            return s;
        }
    }
}
using System;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class Calculator
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add()
        {
             return FirstNumber + SecondNumber;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRegistrationSystem
{
    internal class Marks
    {
        public int english;
        public int math;
        public int science;

        public Marks(int english, int math, int science)
        {
            this.english = english;
            this.math = math;
            this.science = science;
        }

        public int Total()
        {
            return english + math + science;
        }

        public double Percentage()
        {
            return (Total() / 3.0);
        }
    }
}

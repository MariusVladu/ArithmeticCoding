using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticCoding
{
    public static class Intervals
    {
        public static bool ValuesAreInTheSameHalf(uint value1, uint value2)
        {
            return value1 >> 31 == value2 >> 31;
        }

        public static bool IsInTheFirstHalf(uint value)
        {
            return value >> 31 == 0;
        }

        public static bool IsInTheSecondHalf(uint value)
        {
            return value >> 31 == 1;
        }

        public static bool IsInTheFirstQuarter(uint value)
        {
            return value >> 30 == 0;
        }

        public static bool IsInTheSecondQuarter(uint value)
        {
            return value >> 30 == 1;
        }

        public static bool IsInTheThirdQuarter(uint value)
        {
            return value >> 30 == 2;
        }

        public static bool IsInTheForthQuarter(uint value)
        {
            return value >> 30 == 3;
        }
    }
}

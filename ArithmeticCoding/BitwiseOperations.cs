﻿namespace ArithmeticCoding
{
    public static class BitwiseOperations
    {
        public static uint ExtractSecondMostSignificantBitAndFill(uint value, byte fillBit)
        {
            return GetMostSignificantInPlace(value) + ((value << 2) >> 1) + fillBit;
        }

        public static uint GetMostSignificantInPlace(uint value)
        {
            return value & 2147483648;
        }
    }
}
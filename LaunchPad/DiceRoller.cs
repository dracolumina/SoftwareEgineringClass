using System;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Numeric;
using System.Text;
using Randomization.Framework;

namespace Randomization
{
    namespace Core
    {
        public class SecureRandom : RandomNumberGenerator
        {
            private readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();


            public int Next()
            {
                var data = new byte[sizeof(int)];
                rng.GetBytes(data);
                return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
            }

            public int Next(int maxValue)
            {
                return Next(0, maxValue);
            }

            public int Next(int minValue, int maxValue)
            {
                if (minValue > maxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return (int)Math.Floor((minValue + ((double)maxValue - minValue) * NextDouble()));
            }

            public double NextDouble()
            {
                var data = new byte[sizeof(uint)];
                rng.GetBytes(data);
                var randUint = BitConverter.ToUInt32(data, 0);
                return randUint / (uint.MaxValue + 1.0);
            }

            public override void GetBytes(byte[] data)
            {
                rng.GetBytes(data);
            }

            public override void GetNonZeroBytes(byte[] data)
            {
                rng.GetNonZeroBytes(data);
            }
        }


        public class CharacterStats
        {
            SecureRandom StatRandom = new SecureRandom();



            public int GetStat(RollMethod type)
            {
                int hold = 0;

                if (type == RollMethod.Average)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        hold = hold + StatRandom.Next(1, 7);
                    }
                }
                else if (type == RollMethod.Standard)
                {
                    List<int> shorthold = new List<int>();
                    for (int x = 0; x < 4; x++)
                    {
                        shorthold.Add(StatRandom.Next(1, 7));
                    }

                    shorthold.Sort();
                    shorthold.Reverse();
                    shorthold.RemoveAt(3);
                    foreach (var number in shorthold) { hold = hold + number; }
                }
                else if (type == RollMethod.HighPowered)
                {
                    List<int> shorthold = new List<int>();
                    for (int x = 0; x < 5; x++)
                    {
                        shorthold.Add(StatRandom.Next(1, 7));
                    }

                    shorthold.Sort();
                    shorthold.Reverse();
                    shorthold.RemoveAt(3);
                    shorthold.RemoveAt(3);
                    foreach (var number in shorthold) { hold = hold + number; }
                }
                else if (type == RollMethod.Adventurer)
                {
                    List<int> shorthold = new List<int>();
                    var totalcheck = 0;
                    while (shorthold.Count <= 4)
                    {
                        if (shorthold.Count <= 4)
                        {
                            var checkvalue = StatRandom.Next(1, 7);
                            if (totalcheck == 18) { shorthold.Add(checkvalue); break; }

                            if (checkvalue == 6) { totalcheck = totalcheck + checkvalue; shorthold.Add(checkvalue); }
                            else if (checkvalue <= 2) { }
                            else { shorthold.Add(checkvalue); }
                        }
                    }

                    shorthold.Sort();
                    shorthold.Reverse();
                    if (totalcheck == 18)
                    {
                        shorthold.RemoveAt(3);
                    }
                    else
                    {
                        shorthold.RemoveAt(3);
                        shorthold.RemoveAt(3);
                    }



                    foreach (var number in shorthold) { hold = hold + number; }
                }



                return hold;
            }
        }


        public class SingleRoll
        {
            SecureRandom DieRoll = new SecureRandom();


            public int Roll(int Quantity, DieType dice, int modifier)
            {
                int returntotal = 0;
                for (int x = 0; x < Quantity; x++)
                {
                    returntotal = returntotal + DieRoll.Next(1, (int)dice);
                }
                returntotal = returntotal + modifier;
                return returntotal;
            }
        }
    }
    namespace Framework
    {
        public enum DieType
        {
            D4 = 5,
            D6 = 7,
            D8 = 9,
            D10 = 11,
            D12 = 13,
            D20 = 21,
            D30 = 31,
            D100 = 101
        }
        public enum RollMethod
        {
            Average,
            Standard,
            HighPowered,
            Adventurer
        }
        public enum RaceType
        {
            Human,
            Dwarf,
            Elf,
            Gnome,
            Halfling,
            HalfElf,
            HalfOrc
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GME1011A3;

namespace HeroInheritance
{
    internal class HauntedDoll : Minion
    {
        private int _hatred;
        //Constructor 
        public HauntedDoll(int health, int armour, int hatred) : base(health, armour)
        {
            if (hatred <= 1)
            {
                hatred = 2;
            }
            _hatred = hatred;
        }

        //Haunted Dolls take quarter damage because they are only semi physical
        public override void TakeDamage(int damage)
        {
            _health -= damage / 4;
        }

        //Haunted Dolls do 1-3 damage by default
        public override int DealDamage()
        {
            Random rng = new Random();
            return rng.Next(1, 4);
        }

        //Haunted Doll's special.
        public int Curse()
        {
            Console.WriteLine("**An eerie lullaby echoes...**");
            Random rng = new Random();
            return rng.Next(1, 4) * _hatred;
        }

        public override string ToString()
        {
            return "Haunted Doll[" + base.ToString() + "]";
        }
    }
}


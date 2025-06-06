﻿using System.Collections.Generic;
using HeroInheritance;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Epic battle goes here :)
            Random rng = new Random();

            //Collect the hero's name
            Console.Write("What is your hero's name?: ");
            string name = Console.ReadLine();

            //Collect the hero's health
            Console.Write("What is your hero's health?: ");
            int health = int.Parse(Console.ReadLine());

            //Collect the hero's strength
            Console.Write("What is your hero's strength?: ");
            int strength = int.Parse(Console.ReadLine());

            Fighter hero = new Fighter(health, name, strength); 
            Console.WriteLine("Here is our heroic hero: " + hero + "\n\n");


            //Collect number of baddies
            Console.Write("How many baddies stand before " + name + ", our brave hero?: ");
            int numBaddies = int.Parse(Console.ReadLine());
            int numAliveBaddies = numBaddies;

            //List contains Minions! (goblins and skellies)
            List<Minion> baddies = new List<Minion>();


            Console.WriteLine("Type[Class][Health, Armour][Additional Stat]");
            for (int i = 0; i < numBaddies; i++)
            {


                //Each baddie has 33% chance of spawning.
                int odds = rng.Next(1,4);
                    if (odds == 1)
                    {
                        baddies.Add(new Goblin(rng.Next(30, 35), rng.Next(1, 5), rng.Next(1, 10)));
                    }
                    if (odds ==2)
                    {
                        //A skellie should have random health between 25 and 30, and 0 armour (remember
                        //skellie armour is 0 anyway)
                        baddies.Add(new Skellie(rng.Next(25, 30), 0));
                    }
                    if (odds ==3)
                    {
                        baddies.Add(new HauntedDoll(rng.Next(10, 20), rng.Next(1, 3), rng.Next(1, 10)));
                    }
                    
            }

            //this should work even after you make the changes above
            Console.WriteLine("Here are the baddies!!!");
            for(int i = 0; i < baddies.Count; i++)
            {
                Console.WriteLine(baddies[i]);
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Let the EPIC battle begin!!!");
            Console.WriteLine("----------------------------");


            //loop runs as long as there are baddies still alive and the hero is still alive!!
            while (numAliveBaddies > 0 && !hero.isDead())
            {
                //figure out which enemy we are going to battle - the first one that isn't dead
                int indexOfEnemy = 0;
                while (baddies[indexOfEnemy].isDead())
                {
                    indexOfEnemy++;
                }

                //hero deals damage first
                //Has a 33% chance that the hero uses their special, and 67% that they use their regular attack.
                int odds = rng.Next(1, 101);
                if (odds <= 33 && hero.GetStrength() > 0) //Special attack
                {
                    Console.WriteLine(hero.GetName() + " is GOING BERSERK on enemy #" + (indexOfEnemy + 1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);
                    int heroDamage = hero.Berserk();  //how much damage?
                    Console.WriteLine("Hero deals " + heroDamage + " special heroic damage.");
                    baddies[indexOfEnemy].TakeDamage(heroDamage); //baddie takes the damage
                }
                else if (odds <= 33 && hero.GetStrength() <= 0) //Special attack NO STRENGTH
                {
                    //If the hero doesn't have enough special power to use their special attack, they do their regular 
                    //attack instead - but make a note of it in the output. There's no way for the hero to get more special
                    //power points, but if you want to craft a way for that to happen, that's fine.
                    Console.WriteLine(hero.GetName() + " wants to go BERSERK on enemy #" + (indexOfEnemy + 1) + " of " + numBaddies + ", but has no more SPECIAL POWER... Eek, it's a " + baddies[indexOfEnemy].GetType().Name);
                    int heroDamage = hero.DealDamage();  //how much damage?
                    Console.WriteLine("Hero deals " + heroDamage + " regular heroic damage instead.");
                    baddies[indexOfEnemy].TakeDamage(heroDamage); //baddie takes the damage
                }
                else //Regular attack
                {
                    Console.WriteLine(hero.GetName() + " is attacking enemy #" + (indexOfEnemy + 1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);
                    int heroDamage = hero.DealDamage();  //how much damage?
                    Console.WriteLine("Hero deals " + heroDamage + " regular heroic damage.");
                    baddies[indexOfEnemy].TakeDamage(heroDamage); //baddie takes the damage
                }

                //NOTE to coders - armour affects how much damage goblins take, and skellies take
                //half damage - remember that when reviewing the output


                Console.ForegroundColor = ConsoleColor.Red;
                //did we vanquish the baddie we were battling?
                if (baddies[indexOfEnemy].isDead())
                {
                    numAliveBaddies--; //one less baddie to worry about.
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " has been dispatched to void.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else //baddie survived, now attacks the hero
                {
                    //Tell them how much health the baddie has.
                    Console.WriteLine("The " + baddies[indexOfEnemy].GetType().Name + " has " + baddies[indexOfEnemy].GetHealth() + " health remaining.");

                    Console.ForegroundColor = ConsoleColor.Red;
                    int oddsBaddie = rng.Next(1, 101);
                    //TODO: The baddie doesn't ever use their special attack - but they should. Change the above to 
                    //have a 33% chance that the baddie uses their special, and 67% that they use their regular attack.
                    if (baddies[indexOfEnemy].GetType() == typeof(Goblin))
                    {
                        if (oddsBaddie <= 33) //Special attack
                        {
                            int baddieDamage = ((Goblin)baddies[indexOfEnemy]).GoblinBite();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage

                        }
                        else //Regular attack
                        {
                            int baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage
                        }

                    }
                    if (baddies[indexOfEnemy].GetType() == typeof(Skellie))
                    {
                        if (oddsBaddie <= 33) //Special attack
                        {
                            int baddieDamage = ((Skellie)baddies[indexOfEnemy]).SkellieRattle();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage

                        }
                        else //Regular attack
                        {
                            int baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (baddies[indexOfEnemy].GetType() == typeof(HauntedDoll))
                    {
                        if (oddsBaddie <= 33) //Special attack
                        {
                            int baddieDamage = ((HauntedDoll)baddies[indexOfEnemy]).Curse();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage

                        }
                        else //Regular attack
                        {
                            int baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage); //hero takes damage
                        }

                    }



                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //let's look in on our hero.
                    Console.WriteLine(hero.GetName() + " has " + hero.GetHealth() + " health remaining.");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    if (hero.isDead()) //did the hero die
                    {
                        Console.WriteLine(hero.GetName() + " has died. All hope is lost.");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("-----------------------------------------");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            //if we made it this far, the hero is victorious! (that's what the message says.
            if (!hero.isDead())
                Console.WriteLine("\nAll enemies have been dispatched!! " + hero.GetName() + " is victorious!");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
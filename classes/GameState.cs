using Godot;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Extensions;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes
{
    /// <summary>Represents the current state of the game.</summary>
    internal static class GameState
    {
        internal static Hero CurrentHero = new Hero();
        internal static Vector2 HeroPosition = new Vector2(0, 0);
        internal static Enemy CurrentEnemy = new Enemy();
        internal static List<Enemy> AllEnemies = new List<Enemy>();
        internal static List<Item> AllItems = new List<Item>();
        internal static List<Item> AllHeadArmor = new List<Item>();
        internal static List<Item> AllBodyArmor = new List<Item>();
        internal static List<Item> AllHandArmor = new List<Item>();
        internal static List<Item> AllLegArmor = new List<Item>();
        internal static List<Item> AllFeetArmor = new List<Item>();
        internal static List<Item> AllRings = new List<Item>();
        internal static List<Item> AllWeapons = new List<Item>();
        internal static List<Item> AllFood = new List<Item>();
        internal static List<Item> AllDrinks = new List<Item>();
        internal static List<Item> AllPotions = new List<Item>();
        internal static List<Spell> AllSpells = new List<Spell>();
        internal static List<Hero> AllHeroes = new List<Hero>();
        internal static List<HeroClass> AllClasses = new List<HeroClass>();
        internal static Item DefaultWeapon = new Item();
        internal static Item DefaultHead = new Item();
        internal static Item DefaultBody = new Item();
        internal static Item DefaultHands = new Item();
        internal static Item DefaultLegs = new Item();
        internal static Item DefaultFeet = new Item();

        internal static PackedScene PreviousScene = new PackedScene();
        internal static List<PackedScene> History = new List<PackedScene>();

        #region Scene Navigation

        internal static void AddSceneToHistory(Node scene)
        {
            PackedScene packedScene = new PackedScene();
            packedScene.Pack(scene);
            History.Add(packedScene);
            SaveHero(CurrentHero);
        }

        internal static PackedScene GoBack()
        {
            PackedScene last = History.Last();
            History.Remove(last);
            SaveHero(CurrentHero);
            return last;
        }

        #endregion Scene Navigation

        /// <summary>Determines whether a Hero's credentials are authentic.</summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns>Returns true if valid login</returns>
        internal static bool CheckLogin(string username, string password)
        {
            Hero checkHero = AllHeroes.Find(hero => string.Equals(hero.Name, username, StringComparison.InvariantCultureIgnoreCase));
            if (checkHero != null && checkHero != new Hero() && PBKDF2.ValidatePassword(password, checkHero.Password))
            {
                CurrentHero = checkHero;
                return true;
            }
            return false;
        }

        /// <summary>Loads almost everything from the database.</summary>
        internal static void LoadAll()
        {
            //AllClasses = JSONInteraction.LoadClasses().OrderBy(o => o.Name).ToList();
            //AllHeadArmor = JSONInteraction.LoadArmor<Item>("head").OrderBy(o => o.Name).ToList();
            //AllBodyArmor = JSONInteraction.LoadArmor<Item>("body").OrderBy(o => o.Name).ToList();
            //AllHandArmor = JSONInteraction.LoadArmor<Item>("hand").OrderBy(o => o.Name).ToList();
            //AllLegArmor = JSONInteraction.LoadArmor<Item>("leg").OrderBy(o => o.Name).ToList();
            //AllFeetArmor = JSONInteraction.LoadArmor<Item>("feet").OrderBy(o => o.Name).ToList();
            //AllRings = JSONInteraction.LoadRings().OrderBy(o => o.Name).ToList();
            //AllWeapons = JSONInteraction.LoadWeapons().OrderBy(o => o.Name).ToList();
            //AllDrinks = JSONInteraction.LoadDrinks().OrderBy(o => o.Name).ToList();
            //AllFood = JSONInteraction.LoadFood().OrderBy(o => o.Name).ToList();
            //AllPotions = JSONInteraction.LoadPotions().OrderBy(o => o.Name).ToList();
            //AllSpells = JSONInteraction.LoadSpells().OrderBy(o => o.Name).ToList();
            //AllEnemies = JSONInteraction.LoadEnemies().OrderBy(o => o.Name).ToList();

            ////JSONInteraction.WriteAll(AllClasses, AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllWeapons, AllDrinks, AllFood, AllPotions, AllSpells, AllEnemies);

            //// TODO Maybe set up an AllDefaultEquipment List to compare against.
            //// TODO Make it to where your fists can't take durability damage in battle.
            //// TODO Trash on Inventory screen.
            //// TODO Set up a history of PackedScenes and a navigation service for them.
            //// TODO Set up InventoryScene to where I can instance it, and be able to access the slotList on Merchants and looting enemy corpses.
            //// TODO Make it to where you can take off default armor, but not default weapon.

            //AllItems.AddRanges(AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllFood, AllDrinks, AllPotions, AllWeapons);

            //DefaultWeapon = AllWeapons.Find(weapon => weapon.Name == "Fists");
            //DefaultHead = AllHeadArmor.Find(armor => armor.Name == "Cloth Helmet");
            //DefaultBody = AllBodyArmor.Find(armor => armor.Name == "Cloth Shirt");
            //DefaultHands = AllHandArmor.Find(armor => armor.Name == "Cloth Gloves");
            //DefaultLegs = AllLegArmor.Find(armor => armor.Name == "Cloth Pants");
            //DefaultFeet = AllFeetArmor.Find(armor => armor.Name == "Cloth Shoes");

            //AllHeroes = JSONInteraction.LoadHeroes().OrderBy(o => o.Name).ToList();
        }

        /// <summary>Gets a specific Enemy based on its name.</summary>
        /// <param name="name">Name of Enemy</param>
        /// <returns>Enemy</returns>
        private static Enemy GetEnemy(string name) => new Enemy(AllEnemies.Find(enemy => enemy.Name == name));

        #region Item Management

        /// <summary>Gets a specific Item based on its name.</summary>
        /// <param name="name">Item name</param>
        /// <returns>Item</returns>
        private static Item GetItem(string name) => AllItems.Find(itm => itm.Name == name);

        /// <summary>Retrieves a List of all Items of specified Type.</summary>
        /// <param name="type">Type</param>
        /// <returns>List of specified Type.</returns>
        public static List<Item> GetItemsOfType(ItemType type) => new List<Item>(AllItems.FindAll(itm => itm.Type == type));

        #endregion Item Management

        #region Hero Management

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        internal static void ChangeHeroDetails(Hero oldHero, Hero newHero)
        {
            //JSONInteraction.ChangeHeroDetails(oldHero, newHero);
            AllHeroes.Replace(oldHero, newHero);
        }

        /// <summary>Deletes a Hero from the game and database.</summary>
        /// <param name="deleteHero">Hero to be deleted</param>
        /// <returns>Whether deletion was successful</returns>
        internal static void DeleteHero(Hero deleteHero)
        {
            //JSONInteraction.DeleteHero(deleteHero);
            AllHeroes.Remove(deleteHero);
        }

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        internal static void NewHero(Hero newHero)
        {
            if (newHero.Equipment.Head == null || newHero.Equipment.Head == new Item())
                newHero.Equipment.Head = AllHeadArmor.Find(armor => armor.Name == DefaultHead.Name);
            if (newHero.Equipment.Body == null || newHero.Equipment.Body == new Item())
                newHero.Equipment.Body = AllBodyArmor.Find(armor => armor.Name == DefaultBody.Name);
            if (newHero.Equipment.Hands == null || newHero.Equipment.Hands == new Item())
                newHero.Equipment.Hands = AllHandArmor.Find(armor => armor.Name == DefaultHands.Name);
            if (newHero.Equipment.Legs == null || newHero.Equipment.Legs == new Item())
                newHero.Equipment.Legs = AllLegArmor.Find(armor => armor.Name == DefaultLegs.Name);
            if (newHero.Equipment.Feet == null || newHero.Equipment.Feet == new Item())
                newHero.Equipment.Feet = AllFeetArmor.Find(armor => armor.Name == DefaultFeet.Name);
            if (newHero.Equipment.Weapon == null || newHero.Equipment.Weapon == new Item())
            {
                switch (newHero.Class.Name)
                {
                    case "Wizard":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Fire Bolt"));
                        break;

                    case "Cleric":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Heal Self"));
                        break;

                    case "Warrior":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;

                    case "Rogue":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Bow");
                        break;

                    default:
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;
                }
            }

            newHero.Gold = 250;
            for (int i = 0; i < 3; i++)
                newHero.AddItem(AllPotions.Find(itm => itm.Name == "Minor Healing Potion"));

            //JSONInteraction.SaveHero(newHero);
            AllHeroes.Add(newHero);
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        internal static bool SaveHero(Hero saveHero)
        {
            //JSONInteraction.SaveHero(saveHero);

            int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
            AllHeroes[index] = saveHero;

            return true;
        }

        #endregion Hero Management

        #region Exploration Events

        /// <summary>Event where the Hero finds gold.</summary>
        /// <param name="minGold">Minimum amount of gold to be found</param>
        /// <param name="maxGold">Maximum amount of gold to be found</param>
        /// <returns>Returns text regarding gold found</returns>
        internal static string EventFindGold(int minGold, int maxGold)
        {
            int foundGold = minGold == maxGold ? minGold : Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Gold += foundGold;
            SaveHero(CurrentHero);
            return $"You find {foundGold:N0} gold!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="isSold">Is the item sold?</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem(int minValue, int maxValue, bool isSold = true)
        {
            List<Item> availableItems = AllItems.Where(itm => itm.Value >= minValue && itm.Value <= maxValue && itm.IsSold == isSold).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.AddItem(availableItems[item]);
            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="names">List of names of available Items</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.AddItem(availableItems[item]);

            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="type">Type</param>
        /// <param name="isSold">Is the item sold?</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem(int minValue, int maxValue, ItemType type, bool isSold = true)
        {
            List<Item> availableItems = new List<Item>(AllItems.FindAll(itm => itm.Type == type));
            availableItems = availableItems.FindAll(itm => itm.Value >= minValue && itm.Value <= maxValue && itm.IsSold == isSold).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);
            CurrentHero.AddItem(availableItems[item]);
            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero encounters a hostile animal.</summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type == "Animal").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="minLevel">Minimum level of Enemy.</param>
        /// <param name="maxLevel">Maximum level of Enemy.</param>
        internal static void EventEncounterEnemy(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type != "Boss").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="names">Array of names</param>
        internal static void EventEncounterEnemy(params string[] names) =>
            EventEncounterEnemy(names.Select(GetEnemy).ToList());

        internal static void EventEncounterEnemy(List<Enemy> availableEnemies)
        {
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Gold > 0)
                CurrentEnemy.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Gold / 2, CurrentEnemy.Gold);
        }

        /// <summary>Event where the Hero encounters a water stream and restores health and magic.</summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return
            "You stumble across a stream. You stop to drink some of the water and rest a while. You feel recharged!";
        }

        #endregion Exploration Events
    }
}
﻿using Newtonsoft.Json;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents an <see cref="Item"/> that a <see cref="Hero"/> can interact with in the game.</summary>
    public class Item
    {
        // TODO Implement durability and other new features, maybe weapon/armor smiths.

        #region Modifying Properties

        /// <summary>Name of the <see cref="Item"/>.</summary>
        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        /// <summary>Description of the <see cref="Item"/>.</summary>
        [JsonProperty(Order = 2)]
        public string Description { get; set; }

        /// <summary>Type of the <see cref="Item"/>.</summary>
        [JsonProperty(Order = 3)]
        public ItemType Type { get; set; }

        /// <summary>Amount of  the <see cref="Item"/> inflicts.</summary>
        [JsonProperty(Order = 4)]
        public int Damage { get; set; }

        /// <summary>Amount of damage the <see cref="Item"/> can defend against.</summary>
        [JsonProperty(Order = 5)]
        public int Defense { get; set; }

        /// <summary>Amount the <see cref="Item"/> weighs.</summary>
        [JsonProperty(Order = 6)]
        public int Weight { get; set; }

        /// <summary>Amount the <see cref="Item"/> is worth.</summary>
        [JsonProperty(Order = 7)]
        public int Value { get; set; }

        /// <summary>The current durability of an <see cref="Item"/>.</summary>
        [JsonProperty(Order = 8)]
        public int CurrentDurability { get; set; }

        /// <summary>The maximum durability of an <see cref="Item"/>.</summary>
        [JsonProperty(Order = 9)]
        public int MaximumDurability { get; set; }

        /// <summary>Amount of bonus Strength the <see cref="Item"/> grants.</summary>
        [JsonProperty(Order = 10)]
        public int Strength { get; set; }

        /// <summary>Amount of bonus Vitality the <see cref="Item"/> grants.</summary>
        [JsonProperty(Order = 11)]
        public int Vitality { get; set; }

        /// <summary>Amount of bonus Dexterity the <see cref="Item"/> grants.</summary>
        [JsonProperty(Order = 12)]
        public int Dexterity { get; set; }

        /// <summary>Amount of bonus Wisdom the <see cref="Item"/> grants.</summary>
        [JsonProperty(Order = 13)]
        public int Wisdom { get; set; }

        /// <summary>Amount of health this <see cref="Item"/> restores.</summary>
        [JsonProperty(Order = 14)]
        public int RestoreHealth { get; set; }

        /// <summary>Amount of health this <see cref="Item"/> restores.</summary>
        [JsonProperty(Order = 15)]
        public int RestoreMagic { get; set; }

        /// <summary>Does this <see cref="Item"/> cure?</summary>
        [JsonProperty(Order = 16)]
        public bool Cures { get; set; }

        /// <summary>The minimum level a <see cref="Hero"/> is required to be to use the <see cref="Item"/>.</summary>
        [JsonProperty(Order = 17)]
        public int MinimumLevel { get; set; }

        /// <summary>Can the <see cref="Item"/> be sold to a shop?</summary>
        [JsonProperty(Order = 18)]
        public bool CanSell { get; set; }

        /// <summary>Is the <see cref="Item"/> sold in a shop?</summary>
        [JsonProperty(Order = 19)]
        public bool IsSold { get; set; }

        /// <summary><see cref="HeroClass"/>es permitted to use this <see cref="Item"/>.</summary>
        [JsonIgnore]
        public List<HeroClass> AllowedClasses { get; set; } = new List<HeroClass>();

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Item"/>, set up to import from JSON.</summary>
        [JsonProperty(Order = 20)]
        public string AllowedClassesJson
        {
            get => AllowedClasses?.Count > 0 ? string.Join(",", AllowedClasses) : "";
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    AllowedClasses = new List<HeroClass>();
                    AllowedClasses.AddRange(from string itm in value.Split(',')
                                            select GameState.AllClasses.Find(cls => cls.Name == itm.Trim()));
                }
            }
        }

        /// <summary>Location of the Texture to be used for this <see cref="Item"/>.</summary>
        [JsonProperty(Order = 21)]
        public string Texture { get; set; }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Damage the <see cref="Item"/> inflicts, formatted.</summary>
        [JsonIgnore]
        public string DamageToString => Damage.ToString("N0");

        /// <summary>Damage the <see cref="Item"/> inflicts, formatted, with preceding text.</summary>
        [JsonIgnore]
        public string DamageToStringWithText => Damage > 0 ? $"Damage: {DamageToString}" : "";

        /// <summary>Amount of damage the <see cref="Item"/> can defend against, formatted.</summary>
        [JsonIgnore]
        public string DefenseToString => Defense.ToString("N0");

        /// <summary>Amount of damage the <see cref="Item"/> can defend against, formatted, with preceding text.</summary>
        [JsonIgnore]
        public string DefenseToStringWithText => Defense > 0 ? $"Defense: {DefenseToString}" : "";

        /// <summary>Returns text relating to the amount of Health restored by the <see cref="Consumable"/>.</summary>
        [JsonIgnore]
        public string RestoreHealthToString => RestoreHealth > 0 ? $"Restores {RestoreHealth:N0} Health." : "";

        /// <summary>Returns text relating to the amount of Magic restored by the <see cref="Consumable"/>.</summary>
        [JsonIgnore]
        public string RestoreMagicToString => RestoreMagic > 0 ? $"Restores {RestoreMagic:N0} Magic." : "";

        /// <summary>Minimum level the <see cref="Hero"/> need to be to use the <see cref="Item"/>, with preceding text.</summary>
        [JsonIgnore]
        public string MinimumLevelToString => $"Minimum Level: {MinimumLevel}";

        /// <summary>Returns text regarding if the <see cref="Consumable"/> can heal the user.</summary>
        [JsonIgnore]
        public string CuresToString => Cures ? $"Cures ailments." : "";

        /// <summary>Returns text regarding all effects this <see cref="Consumable"/> will induce.</summary>
        [JsonIgnore]
        public string EffectsToString
        {
            get
            {
                string effects = "";
                if (RestoreHealth > 0)
                {
                    effects += RestoreHealthToString;
                }
                if (RestoreMagic > 0)
                {
                    if (effects.Length > 0)
                        effects += "\n";
                    effects += RestoreMagicToString;
                }
                if (Cures)
                {
                    if (effects.Length > 0)
                        effects += "\n";
                    effects += CuresToString;
                }

                return effects;
            }
        }

        /// <summary>The current durability of an <see cref="Item"/>, with thousands separators.</summary>
        [JsonIgnore]
        public string CurrentDurabilityToString => CurrentDurability.ToString("N0");

        /// <summary>The maximum durability of an <see cref="Item"/>, with thousands separators.</summary>
        [JsonIgnore]
        public string MaximumDurabilityToString => MaximumDurability.ToString("N0");

        /// <summary>The durability of an <see cref="Item"/>, formatted.</summary>
        [JsonIgnore]
        public string Durability => $"{CurrentDurabilityToString} / {MaximumDurabilityToString}";

        /// <summary>The durability of an <see cref="Item"/>, formatted.</summary>
        [JsonIgnore]
        public string DurabilityString => $"Durability: {Durability}";

        /// <summary>The durability ratio of an <see cref="Item"/>.</summary>
        [JsonIgnore]
        public decimal DurabilityRatio => CurrentDurability * 1m / MaximumDurability;

        /// <summary>The weight of the <see cref="Item"/> with thousands separators.</summary>
        [JsonIgnore]
        public string WeightToString => Weight.ToString("N0");

        /// <summary>The weight of the <see cref="Item"/> with thousands separators and preceding text.</summary>
        [JsonIgnore]
        public string WeightToStringWithText => $"Weight: {WeightToString}";

        /// <summary>The value of the <see cref="Item"/> with thousands separators.</summary>
        [JsonIgnore]
        public string ValueToString => Value.ToString("N0");

        /// <summary>The value of the <see cref="Item"/> with thousands separators and preceding text.</summary>
        [JsonIgnore]
        public string ValueToStringWithText => $"Value: {ValueToString}";

        /// <summary>The value of the Item.</summary>
        [JsonIgnore]
        public int SellValue => Value / 2 * CurrentDurability / MaximumDurability;

        /// <summary>The sell value of the <see cref="Item"/> with thousands separators.</summary>
        [JsonIgnore]
        public string SellValueToString => SellValue.ToString("N0");

        /// <summary>The sell value of the <see cref="Item"/> with thousands separators with preceding text.</summary>
        [JsonIgnore]
        public string SellValueToStringWithText => $"Sell Value: {SellValueToString}";

        /// <summary>Returns text relating to the sellability of the <see cref="Item"/>.</summary>
        [JsonIgnore]
        public string CanSellToString => CanSell ? "Sellable" : "Not Sellable";

        /// <summary>Returns the Strength and preceding text.</summary>
        [JsonIgnore]
        public string StrengthToString => Strength > 0 ? $"Strength: {Strength}" : "";

        /// <summary>Returns the Vitality and preceding text.</summary>
        [JsonIgnore]
        public string VitalityToString => Vitality > 0 ? $"Vitality: {Vitality}" : "";

        /// <summary>Returns the Dexterity and preceding text.</summary>
        [JsonIgnore]
        public string DexterityToString => Dexterity > 0 ? $"Dexterity: {Dexterity}" : "";

        /// <summary>Returns the Wisdom and preceding text.</summary>
        [JsonIgnore]
        public string WisdomToString => Wisdom > 0 ? $"Wisdom: {Wisdom}" : "";

        /// <summary>Returns all bonuses in string format.</summary>
        [JsonIgnore]
        public string BonusToString
        {
            get
            {
                string[] bonuses =
                {
                    StrengthToString, VitalityToString,
                    DexterityToString, WisdomToString
                };

                return string.Join("\n", bonuses.Where(bonus => bonus.Length > 0));
            }
        }

        /// <summary>Displays the Type of the <see cref="Item"/>, formatted.</summary>
        [JsonIgnore]
        public string TypeToString
        {
            get
            {
                switch (Type)
                {
                    case ItemType.MeleeWeapon:
                        return "Melee Weapon";

                    case ItemType.RangedWeapon:
                        return "Ranged Weapon";

                    case ItemType.HeadArmor:
                        return "Head Armor";

                    case ItemType.BodyArmor:
                        return "Body Armor";

                    case ItemType.HandArmor:
                        return "Hand Armor";

                    case ItemType.LegArmor:
                        return "Leg Armor";

                    case ItemType.FeetArmor:
                        return "Feet Armor";

                    default:
                        return Type.ToString();
                }
            }
        }

        /// <summary>Text to be displayed when hovering over an <see cref="Item"/>.</summary>
        [JsonIgnore]
        public string TooltipText => this != new Item()
                    ? Name + "\n"
                        + Description + "\n"
                        + TypeToString + "\n"
                        + ((Damage > 0) ? DamageToStringWithText + "\n" : "")
                        + ((Defense > 0) ? DefenseToStringWithText + "\n" : "")
                        + ((Weight > 0) ? WeightToStringWithText + "\n" : "")
                        + ValueToStringWithText + "\n"
                        + DurabilityString + "\n"
                        + (EffectsToString.Length > 0 ? EffectsToString + "\n" : "")
                        + (MinimumLevel > 1 ? MinimumLevelToString + "\n" : "")
                        + (AllowedClasses.Count > 0 ? AllowedClassesToString + "\n" : "")
                        + BonusToString
                    : "";

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Item"/>, formatted</summary>
        [JsonIgnore]
        public string AllowedClassesToString => String.Join(",", AllowedClasses);

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(Item left, Item right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase)
                && left.Type == right.Type
                && left.Damage == right.Damage
                && left.Defense == right.Defense
                && left.Weight == right.Weight
                && left.Value == right.Value
                && left.CurrentDurability == right.CurrentDurability
                && left.MaximumDurability == right.MaximumDurability
                && left.Strength == right.Strength
                && left.Vitality == right.Vitality
                && left.Dexterity == right.Dexterity
                && left.Wisdom == right.Wisdom
                && left.RestoreHealth == right.RestoreHealth
                && left.RestoreMagic == right.RestoreMagic
                && left.Cures == right.Cures
                && left.MinimumLevel == right.MinimumLevel
                && left.CanSell == right.CanSell
                && left.IsSold == right.IsSold
                && !left.AllowedClasses.Except(right.AllowedClasses).Any();
        }

        public override bool Equals(object obj) => Equals(this, obj as Item);

        public bool Equals(Item otherItem) => Equals(this, otherItem);

        public static bool operator ==(Item left, Item right) => Equals(left, right);

        public static bool operator !=(Item left, Item right) => !Equals(left, right);

        public override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of <see cref="Item"/>.</summary>
        public Item()
        {
        }

        /// <summary>Initializes an instance of <see cref="Item"/> by assigning values to Properties.</summary>
        /// <param name="name">Name of the <see cref="Item"/></param>
        /// <param name="description">Description of the <see cref="Item"/></param>
        /// <param name="itemType">Type of the <see cref="Item"/></param>
        /// <param name="damage">Amount of damage the <see cref="Item"/> inflicts</param>
        /// <param name="defense">Amount of damage the <see cref="Item"/> can defend against</param>
        /// <param name="weight">Amount the <see cref="Item"/> weighs</param>
        /// <param name="value">Amount the <see cref="Item"/> is worth</param>
        /// <param name="currentDurability">The current durability of an <see cref="Item"/></param>
        /// <param name="maximumDurability">The maximum durability of an <see cref="Item"/></param>
        /// <param name="strength">Amount of bonus Strength the <see cref="Item"/> grants</param>
        /// <param name="vitality">Amount of bonus Vitality the <see cref="Item"/> grants</param>
        /// <param name="dexterity">Amount of bonus Dexterity the <see cref="Item"/> grants</param>
        /// <param name="wisdom">Amount of bonus Wisdom the <see cref="Item"/> grants</param>
        /// <param name="restoreHealth">Amount of health this <see cref="Item"/> restores</param>
        /// <param name="restoreMagic">Amount of magic this <see cref="Item"/> restores</param>
        /// <param name="cures">Does this <see cref="Item"/> cure?</param>
        /// <param name="minimumLevel">The minimum level a <see cref="Hero"/> is required to be to use the <see cref="Item"/></param>
        /// <param name="canSell">Can the <see cref="Item"/> be sold to a shop?</param>
        /// <param name="isSold">Is the <see cref="Item"/> sold in a shop?</param>
        /// <param name="allowedClasses"><see cref="HeroClass"/>es permitted to use this <see cref="Item"/></param>
        /// <param name="texture">Location of the Texture to be used for this <see cref="Item"/></param>
        internal Item(string name, string description, ItemType itemType, int damage, int defense, int weight, int value, int currentDurability, int maximumDurability, int strength, int vitality, int dexterity, int wisdom, int restoreHealth, int restoreMagic, bool cures, int minimumLevel, bool canSell, bool isSold, List<HeroClass> allowedClasses, string texture)
        {
            Name = name;
            Description = description;
            Type = itemType;
            Damage = damage;
            Defense = defense;
            Weight = weight;
            Value = value;
            CurrentDurability = currentDurability;
            MaximumDurability = maximumDurability;
            Strength = strength;
            Vitality = vitality;
            Dexterity = dexterity;
            Wisdom = wisdom;
            RestoreHealth = restoreHealth;
            RestoreMagic = restoreMagic;
            Cures = cures;
            MinimumLevel = minimumLevel;
            CanSell = canSell;
            IsSold = isSold;
            AllowedClasses = new List<HeroClass>(allowedClasses);
            Texture = texture;
        }

        /// <summary>Replaces this instance of <see cref="Item"/> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Item"/> to replace this instance</param>
        public Item(Item other) : this(other.Name, other.Description, other.Type, other.Damage, other.Defense, other.Weight, other.Value, other.CurrentDurability, other.MaximumDurability, other.Strength, other.Vitality, other.Dexterity, other.Wisdom, other.RestoreHealth, other.RestoreMagic, other.Cures, other.MinimumLevel, other.CanSell, other.IsSold, new List<HeroClass>(other.AllowedClasses), other.Texture)
        {
        }

        #endregion Constructors
    }
}
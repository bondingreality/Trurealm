
using System;
[Flags]
public enum KeyBinderKeys
{
    UP = 1<<1,
    DOWN = 1 << 2,
    LEFT = 1 << 3,
    RIGHT = 1 << 4,
    ACT1 = 1 << 5,
    ACT2 = 1 << 6,
    ACT3 = 1 << 7
}
[Flags]
public enum PrimaryVitals
{
    health = 1 << 1,
    fatigue = 1 << 2,
    mind = 1 << 3,
    speed = 1 << 4
}
[Flags]
public enum PrimaryStats
{
    Strength = 1 << 1,
    Endurance = 1 << 2,
    Agility = 1 << 3,
    Dexterity = 1 << 4,
    Intelligence = 1 << 5,
    Wisdom = 1 << 6
}

[Flags]
public enum RarityLevel
{
    Common = 1 << 1,
    Uncommon = 1 << 2,
    Rare = 1 << 3,
    Epic = 1 << 4,
    Legendary = 1 << 5,
    Unique = 1 << 6
}
public enum RarityAttribute
{
    color = 1 << 1
}
[Flags]
public enum QualityLevel
{
    Unrepairable = 1 << 1,
    Broken = 1 << 2,
    Bad = 1 << 3,
    Poor = 1 << 4,
    Average = 1 << 5,
    Good = 1 << 6,
    Great = 1 << 7,
    Perfect = 1 << 8,
    Unbreakable = 1 << 9
}
public enum QualityAttribute
{
    color = 1 << 1,
    minPercentage = 1 << 2,
    maxPercentage = 1 << 3
}
[Flags]
public enum StatLevel
{
    I = 1 << 1,
    H = 1 << 2,
    G = 1 << 3,
    F = 1 << 4,
    E = 1 << 5,
    D = 1 << 6,
    C = 1 << 7,
    B = 1 << 8,
    A = 1 << 9,
    S = 1 << 10,
    SS = 1 << 11,
    SSS = 1 << 12,
    Unique = 1 << 13,
    Godly = 1 << 14
}
public enum StatLevelAttribute
{
    color = 1 << 1,
    minRange = 1 << 2,
    maxRange = 1 << 3,
    reqExp2LvlExpo = 1 << 4,
    onLvlUpBonusMultiplier = 1 << 5
}
public enum StatModType
{
    Flat = 1 << 1,
    Percent = 1 << 2
}
public enum StatModProperty
{
    Value = 1 << 1,
    Base = 1 << 2,
    Min = 1 << 3,
    Max = 1 << 4
}
public enum StatModEffect
{
    Add = 1 << 1,
    Subtract = 1 << 2,
    Multiply = 1 << 3,
    Divide = 1 << 4
}
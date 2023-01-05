using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    public abstract class Card
    {
        // BASIC CARD INFO ==========================================

        public string Title { get; private set; }

        public string Description { get; private set; }

        public CardType Type { get; private set; }

        public CardEffect Effect { get; private set; }

        // EFFECT ITEMS =============================================
        // Numbers for the possible effect a card can have on another
        // player. Card can have all the following effects or none.

        public int VitalEffect { get; private set; }

        public int IntoxEffect { get; private set; }

        public int GoldEffect { get; private set; }

        // GAMBLING CARD INFO =======================================
        // Information for the case in which this is a gambling card

        public GamblingType GamblingType { get; private set; }

        public Card()
        {
            Type = CardType.Anytime;
            Title = "Default Card";
            Description = "Default Description";

            VitalEffect = 0;
            IntoxEffect = 0;
            GoldEffect = 0;
        }

        public Card(CardType type, string title, string description, int vitalEffect, int intoxEffect, int goldEffect)
        {
            Type = type;
            Title = title;
            Description = description;

            VitalEffect = vitalEffect;
            IntoxEffect = intoxEffect;
            GoldEffect = goldEffect;
        }
    }

    public enum CardType
    {
        Sometimes,
        Anytime,
        Gambling
    }

    public enum CardEffect
    {
        None,
        Vitality,
        Intox,
        Gold,
        GoldandPay,
        RedirectDamage,
        Negate,
        Ignore,
        Tip
    }

    public enum GamblingType
    {
        In,
        Cheating,
        WinningHand
    }
}

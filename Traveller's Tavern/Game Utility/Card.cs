using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gameutil.SometimesCard;

namespace gameutil
{
    public abstract class Card
    {
        // BASIC CARD INFO ==========================================

        public string Title { get; private set; }

        public string Description { get; private set; }

        public CardEffect Effect { get; private set; }

        // EFFECT ITEMS =============================================
        // Numbers for the possible effect a card can have on another
        // player. Card can have all the following effects or none.

        public int VitalEffect { get; private set; }

        public int IntoxEffect { get; private set; }

        public int GoldEffect { get; private set; }

        public Card()
        {
            Title = "Default Card";
            Description = "Default Description";

            VitalEffect = 0;
            IntoxEffect = 0;
            GoldEffect = 0;
        }

        public Card(string title, string description, int vitalEffect, int intoxEffect, int goldEffect)
        {
            Title = title;
            Description = description;

            VitalEffect = vitalEffect;
            IntoxEffect = intoxEffect;
            GoldEffect = goldEffect;
        }
    }

    public class SometimesCard : Card
    {
        public delegate bool CardConditionChecker(Card card);

        public CardConditionChecker CheckCardConditions { get; private set; }

        /// <summary>
        /// Default constructor for a new Sometimes Card. Used only for serializing a SometimesCard from data.
        /// Sets the CheckCardConditions to throw an exception, as that member should be overrided during
        /// serialiazation - if it's not, some error has occurred.
        /// </summary>
        /// <exception cref="SometimesCardLoadingException"></exception>
        public SometimesCard() : base()
        {
            CheckCardConditions = (Card card) => throw new SometimesCardLoadingException();
        }

        public SometimesCard(string title, string description, int vitalEffect,
            int intoxEffect, int goldEffect, CardConditionChecker checkCardConditions) :
            base(title, description, vitalEffect, intoxEffect, goldEffect)
        {
            CheckCardConditions = checkCardConditions;
        }
    }

    public class GamblingCard : Card
    {
        public GamblingType GamblingType { get; private set; }

        // TODO: Gonna need a default constructor here chief

        public GamblingCard(string title, string description, int vitalEffect,
            int intoxEffect, int goldEffect, GamblingType gamblingType) :
            base(title, description, vitalEffect, intoxEffect, goldEffect)
        {
            GamblingType = gamblingType;
        }

        public bool CheckForControl(GamblingCard currentCard)
        {
            // If this card is an In card, it takes control if the most
            // recently played gambling card is In or Cheating
            if (GamblingType == GamblingType.In
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Cheating))
                return true;

            // If this card is a Cheating card, it takes control if the most
            // recently played gambling card is In, Winning Hand, or Cheating
            if (GamblingType == GamblingType.Cheating
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Cheating
                || currentCard.GamblingType == GamblingType.WinningHand))
                return true;

            // If this card is a Cheating card, it takes control if the most
            // recently played gambling card is In or Cheating
            if (GamblingType == GamblingType.WinningHand
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Cheating))
                return true;

            return false;
        }
    }

    public enum CardType
    {
        Action,
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

    public class SometimesCardLoadingException : Exception
    {
        public SometimesCardLoadingException() 
            : base("Data for a sometimes card was not loaded correctly. " +
                  "Possible corrupted data or error in a library.")
        {
        }

        public SometimesCardLoadingException(string message)
            : base(message)
        {
        }

        public SometimesCardLoadingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

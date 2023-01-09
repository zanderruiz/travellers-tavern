using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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

        public delegate void CardEffects();

        // Stores a delegate for the actions/effects that this card takes
        public CardEffects Effects { get; private set; }

        public Card()
        {
            Title = "Default Card";
            Description = "Default Description";

            VitalEffect = 0;
            IntoxEffect = 0;
            GoldEffect = 0;

            Effects = () => throw new CardLoadingException();
        }

        public Card(string title, string description, int vitalEffect, int intoxEffect, int goldEffect, CardEffects effects)
        {
            Title = title;
            Description = description;

            VitalEffect = vitalEffect;
            IntoxEffect = intoxEffect;
            GoldEffect = goldEffect;

            Effects = effects;
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
        /// <exception cref="CardLoadingException"></exception>
        public SometimesCard() : base()
        {
            CheckCardConditions = (Card card) => throw new CardLoadingException();
        }

        public SometimesCard(string title, string description, int vitalEffect,
            int intoxEffect, int goldEffect, CardEffects effects, CardConditionChecker checkCardConditions) :
            base(title, description, vitalEffect, intoxEffect, goldEffect, effects)
        {
            CheckCardConditions = checkCardConditions;
        }
    }

    public class AnytimeCard : Card
    {

    }

    public class GamblingCard : Card
    {
        public GamblingType GamblingType { get; private set; }

        // TODO: Gonna need a default constructor here chief

        public GamblingCard(string title, string description, int vitalEffect,
            int intoxEffect, int goldEffect, CardEffects effects, GamblingType gamblingType) :
            base(title, description, vitalEffect, intoxEffect, goldEffect, effects)
        {
            GamblingType = gamblingType;
        }

        public bool CheckForControl(GamblingCard currentCard)
        {
            // If this card is an In card, it takes control if the most
            // recently played gambling card is In or Cheating
            if (GamblingType == GamblingType.In
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Raise
                || currentCard.GamblingType == GamblingType.Cheating))
                return true;

            // If this card is a Cheating card, it takes control if the most
            // recently played gambling card is In, Cheating, Raise, or Winning Hand
            if (GamblingType == GamblingType.Cheating
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Cheating
                || currentCard.GamblingType == GamblingType.Raise
                || currentCard.GamblingType == GamblingType.WinningHand))
                return true;

            // If this card is a Raise card, it takes control if the most
            // recently played gambling card is In, Raise, or Cheating
            if (GamblingType == GamblingType.Raise
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Raise
                || currentCard.GamblingType == GamblingType.Cheating))
                return true;

            // If this card is a Cheating card, it takes control if the most
            // recently played gambling card is In, Cheating, or Raise
            if (GamblingType == GamblingType.WinningHand
                && (currentCard.GamblingType == GamblingType.In
                || currentCard.GamblingType == GamblingType.Cheating
                || currentCard.GamblingType == GamblingType.Raise))
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
        Raise,
        WinningHand
    }

    public class CardLoadingException : Exception
    {
 
        public CardLoadingException() 
            : base("Data for a card was not loaded correctly. " +
                  "Possible corrupted data or error in a library.")
        {
        }

        public CardLoadingException(string message)
            : base(message)
        {
        }

        public CardLoadingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

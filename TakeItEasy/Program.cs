using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TakeItEasy
{
    public class Load
    {
        public Load(int weight)
        {
            Weight = weight;
        }
        public int Weight { get; set; }

        public bool IsHeavy()
        {
            return (Weight > 500) ? true : false;
        }
    }

    public enum GenderType { Male, Female }

    public enum IntentionType { WannaHoldMe, WannaScoldMe, SaysShesAFriendOfMine }

    interface IPerson
    {
        GenderType Gender { get; set; }
        IntentionType Intention { get; set; }
    }

    public class Person : IPerson
    {
        public GenderType Gender { get; set; }
        public IntentionType Intention { get; set; }

        public Person(GenderType gender, IntentionType intention)
        {
            Gender = gender;
            Intention = intention;
        }
    }

    public static class Assert
    {
        public static bool AreEqual<T>(T a, T b)
        {
            if (a.Equals(b))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Expected {0}, but was {1}", a, b);
                return false;
            }
        }

    }


    public class Wheels
    {
        public Wheels()
        {
            SoundVolume = 100;
        }
        public int SoundVolume { get; set; }

        public bool MakingMeCrazy()
        {
            return SoundVolume >= 50;
        }
    }

    class Narrator
    {
        private Load _load;
        private Random _rnd;
        private List<IPerson> _peopleOnMyMind;

        private Wheels _myWheels;
        public Wheels MyWheels { get { return _myWheels; }}

        private string _firstName { get; set; }
        private string _lastName { get; set; }

        private string _currentActivity;

        public IEnumerable<IPerson> OnMyMind
        {
            get { return _peopleOnMyMind; }
        }
        public Narrator(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            _rnd = new Random();
            _load = new Load(501);

            _myWheels = new Wheels();
            _peopleOnMyMind = new List<IPerson>();

            return;
            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaHoldMe));
            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaHoldMe));
            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaHoldMe));
            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaHoldMe));

            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaScoldMe));
            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.WannaScoldMe));

            _peopleOnMyMind.Add(new Person(GenderType.Female, IntentionType.SaysShesAFriendOfMine));
        }

        public void LoosenLoad()
        {
            Console.WriteLine("Trying to loosen my load...");
            _load.Weight -= _rnd.Next(1, 50);

            var loosenLoad = _rnd.Next(0, 1) == 1;

            if (loosenLoad)
            {
                var loosenLoadAmount = _rnd.Next(10, 15);
                Console.WriteLine("Loosened Load by {0} dB.", loosenLoadAmount);
            }

            Console.WriteLine("Was unable to loosen load this time.");
        }

        public void Run(string where)
        {
            _currentActivity = String.Format("Running {0}", where);
            Console.WriteLine("I've been ", _currentActivity);
        }

        internal void TakeItEasy()
        {
            Thread.Sleep(_rnd.Next(100, 300));

            var lowerSoundOfWheels = _rnd.Next(0, 1) == 1;

            if (lowerSoundOfWheels)
            {
                var lowerSoundOfWheelsAmount = _rnd.Next(10, 15);
                Console.WriteLine("Taking it easy lowered the sound of your own wheels by {0} dB.", lowerSoundOfWheelsAmount);
            }

            Console.WriteLine("Taking it easy did not lower the sound of your own wheels this time.");

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var me = new Narrator("Glenn", "Frey");

            me.Run("Down the road.");

            try
            {
                me.LoosenLoad();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var womennOnMyMind = me.OnMyMind.Where(w => w.Gender.Equals(GenderType.Female));

            Assert.AreEqual<int>(7, womennOnMyMind.Count());

            Assert.AreEqual<int>(4, womennOnMyMind.Count(w => w.Intention.Equals(IntentionType.WannaHoldMe)));
            Assert.AreEqual<int>(2, womennOnMyMind.Count(w => w.Intention.Equals(IntentionType.WannaScoldMe)));
            Assert.AreEqual<int>(1, womennOnMyMind.Count(w => w.Intention.Equals(IntentionType.SaysShesAFriendOfMine)));

            me.TakeItEasy();
            me.TakeItEasy();

            while (me.MyWheels.MakingMeCrazy())
            {
                me.TakeItEasy();
            }

            //Take It easy, take it easy
            //Don't let the sound of your own wheels
            //drive you crazy
            //Lighten up while you still can
            //don't even try to understand
            //Just find a place to make your stand
            //and take it easy
            //Well, I'm a standing on a corner
            //in Winslow, Arizona
            //and such a fine sight to see
            //It's a girl, my Lord, in a flatbed
            //Ford slowin' down to take a look at me
            //Come on, baby, don't say maybe
            //I gotta know if your sweet love is
            //gonna save me
            //We may lose and we may win though
            //we will never be here again
            //so open up, I'm climbin' in,
            //so take it easy
            //Well I'm running down the road trying to loosen
            //my load, got a world of trouble on my mind
            //lookin' for a lover who won't blow my
            //cover, she's so hard to find
            //Take it easy, take it easy
            //don't let the sound of your own
            //wheels make you crazy
            //come on baby, don't say maybe
            //I gotta know if your sweet love is
            //gonna save me, oh oh oh
            //Oh we got it easy
            //We oughta take it easy 

            Console.ReadKey();
        }
    }
}

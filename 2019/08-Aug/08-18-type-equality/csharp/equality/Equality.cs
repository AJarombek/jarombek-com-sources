/**
 * Investigate equality among reference types and value types in the .NET Framework
 * Sources: [C# 7.0 In a Nutshell: Page 282-290]
 * Author: Andrew Jarombek
 * Date: 7/24/2019
 */

using System;
using System.Collections;
using static System.Diagnostics.Debug;

namespace equality
{
    /// <summary>
    /// Class representing a ball of yarn.
    /// </summary>
    class Yarn : IEquatable<Yarn>, IComparable<Yarn>, IComparable
    {
        public readonly int Length;
        public readonly YarnWeight Weight;

        /// <summary>
        /// Constructor for a ball of yarn.
        /// </summary>
        /// <param name="length">The length of the ball of yarn in yards.</param>
        /// <param name="weight">Weight (thickness grade) of the yarn.</param>
        public Yarn(int length, YarnWeight weight)
        {
            this.Length = length;
            this.Weight = weight;
        }
        
        /// <summary>
        /// Yarn.YarnWeight which describes the finite number of yarn weights.  The enum names are the standard
        /// weighting names and their values are the standard weighting numbers.
        /// </summary>
        public enum YarnWeight
        {
            Lace = 0, SuperFine = 1, Fine = 2, LightWeight = 3, 
            Medium = 4, Bulky = 5, SuperBulky = 6, Jumbo = 7
        }

        /// <inheritdoc/>
        public int CompareTo(Yarn other)
        {
            // Rule 1 of IComparable - when Equals() returns true CompareTo() should return 0.
            if (Equals(other))
            {
                return 0;
            }
            
            var result = Length.CompareTo(other.Length);
            return result == 0 ? Weight.CompareTo(other.Weight) : result;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            // Shorthand without casting for: obj is Yarn && Equals((Yarn) obj)
            return obj is Yarn yarn && Equals(yarn);
        }

        /// <summary>
        /// Determines if this Yarn is equal to another Yarn object using value equality.
        /// Implementation of the Equals() method defined in the <code>IEquatable&lt;Yarn&gt;</code> interface.
        /// </summary>
        /// <param name="other">Another Yarn object to check for value equality with.</param>
        /// <returns>
        /// <code>true</code> if the two Yarn objects are equal in value, <code>false</code> otherwise.
        /// </returns>
        public bool Equals(Yarn other) => Length == other.Length && Weight.Equals(other.Weight);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + Length.GetHashCode();
            hash = hash * 31 + Weight.GetHashCode();
            return hash;
        }
        
        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (!(obj is Yarn))
            {
                throw new InvalidOperationException("Argument obj is not of type Yarn.");
            }

            return CompareTo((Yarn) obj);
        }
        
        /// <summary>
        /// Overload the &lt; operator to check if one Yarn object is less than another.
        /// </summary>
        /// <param name="y1">The first yarn object to compare (written before the &lt; operator).</param>
        /// <param name="y2">The second yarn object to compare (written after the &lt; operator).</param>
        /// <returns></returns>
        public static bool operator < (Yarn y1, Yarn y2) => y1.CompareTo(y2) < 0;

        /// <summary>
        /// Overload the > operator to check if one Yarn object is greater than another.
        /// </summary>
        /// <param name="y1">The first yarn object to compare (written before the > operator).</param>
        /// <param name="y2">The second yarn object to compare (written after the > operator).</param>
        /// <returns></returns>
        public static bool operator > (Yarn y1, Yarn y2) => y1.CompareTo(y2) > 0;
    }

    /// <summary>
    /// Class representing a custom string implementation where the == operator is overloaded to perform value equality.
    /// </summary>
    sealed class MyString
    {
        private readonly string Content;

        /// <summary>
        /// Constructor for a custom string class.
        /// </summary>
        /// <param name="content">The content of the string.  Uses object composition with string.</param>
        public MyString(string content)
        {
            Content = content;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is MyString str && str.Content.Equals(Content);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return 17 * 31 + Content.GetHashCode();
        }

        /// <summary>
        /// Overload the == operator to check for value equality instead of reference equality.
        /// </summary>
        /// <param name="s1">The first string to compare content values.</param>
        /// <param name="s2">The second string to compare values.</param>
        /// <returns>
        /// <code>true</code> if the two MyString objects are equal in value, <code>false</code> otherwise.
        /// </returns>
        public static bool operator == (MyString s1, MyString s2) => s1.Equals(s2);
        
        /// <summary>
        /// Overload the != operator to check for value inequality instead of reference inequality.
        /// </summary>
        /// <param name="s1">The first string to compare content values.</param>
        /// <param name="s2">The second string to compare values.</param>
        /// <returns>
        /// <code>true</code> if the two MyString objects are NOT equal in value, <code>false</code> otherwise.
        /// </returns>
        public static bool operator != (MyString s1, MyString s2) => !s1.Equals(s2);
    }

    /// <summary>
    /// Struct representing a roll of wrapping paper.  By default structs can't use the == operator like classes can.
    /// A solution is letting the struct implement IEquatable to determine value equality
    /// and overload the == and != operators.
    /// </summary>
    struct WrappingPaper: IEquatable<WrappingPaper>
    {
        public string Brand { get; }
        public string Pattern { get; }

        /// <summary>
        /// Construct a new Wrapping Paper struct which has a company brand and visual pattern.
        /// </summary>
        /// <param name="brand">The company that made the wrapping paper.</param>
        /// <param name="pattern">The visual pattern on the wrapping paper.</param>
        public WrappingPaper(string brand, string pattern)
        {
            Brand = brand;
            Pattern = pattern;
        }
        
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is WrappingPaper other && Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + Brand.GetHashCode();
            hash = hash * 31 + Pattern.GetHashCode();
            return hash;
        }

        /// <inheritdoc/>
        public bool Equals(WrappingPaper other)
        {
            return other is WrappingPaper wrappingPaper 
                   && wrappingPaper.Brand.Equals(Brand) 
                   && wrappingPaper.Pattern.Equals(Pattern);
        }
        
        /// <summary>
        /// Overload the == operator to check for value equality instead of reference equality.
        /// </summary>
        /// <param name="wp1">The first wrapping paper struct to compare content values.</param>
        /// <param name="wp2">The second wrapping paper struct to compare values.</param>
        /// <returns>
        /// <code>true</code> if the two WrappingPaper structs are equal in value, <code>false</code> otherwise.
        /// </returns>
        public static bool operator == (WrappingPaper wp1, WrappingPaper wp2) => wp1.Equals(wp2);
        
        /// <summary>
        /// Overload the != operator to check for value inequality instead of reference inequality.
        /// </summary>
        /// <param name="wp1">The first wrapping paper struct to compare content values.</param>
        /// <param name="wp2">The second wrapping paper struct to compare values.</param>
        /// <returns>
        /// <code>true</code> if the two WrappingPaper structs are NOT equal in value, <code>false</code> otherwise.
        /// </returns>
        public static bool operator != (WrappingPaper wp1, WrappingPaper wp2) => !wp1.Equals(wp2);
    }
    
    public static class Equality
    {
        public static void Execute()
        {
            // Value types can only use value equality (==).  This is similar to how Java primitives
            // can only use the == operator.
            // Java - []
            int one = 1;
            int two = 2;
            Assert(one != two);
            
            // Unlike Java, int is an alias for Int32, which is a struct.  Therefore, it can use the Equals() method.
            // In Java, int is a primitive value and doesn't have any methods.  To add to the confusion, in C# value
            // types can also be called primitive types since they are handled as basic assembly code
            // instructions [pg. 29]  Value (primitive) types in C# include int, double, char, bool, byte, etc.
            Assert(!one.Equals(two));
            
            // However, Equals() for value types always use value equality unless the type is boxed.
            object objOne = one;
            object objTwo = two;

            int anotherOne = 1;
            object objOneAgain = anotherOne;
            
            Assert(objOne != objTwo);
            Assert(objOne.Equals(objTwo) == false);
            
            Assert(objOne != objOneAgain); // Since the integers are boxed, == checks for reference equality.
            Assert(objOne.Equals(objOneAgain)); // And Equals() checks for value equality.
            
            // The differences between Equals() and == aren't as straightforward in C# as they are in Java.
            // For example, with the URI reference type == checks for value equality.  This is because C# allows for
            // operator overloading.  Java does not permit operator overloading.
            Uri jarombekCom = new Uri("https://jarombek.com");
            Uri jarombekCom2 = new Uri("https://jarombek.com");
            Assert(jarombekCom == jarombekCom2); // If this was reference equality, it would return false.
            
            // Similar to Java, object has a static method for computing null safe equality with the Equals() method.
            object obj1 = null;
            object obj2 = new object();

            try
            {
                // This command should always fail with a NullReferenceException
                obj1.Equals(obj2);
                
                Assert(false);
            }
            catch (NullReferenceException e)
            {
                Assert(e != null);
                Assert(true);
            }
            
            // The static method is null-safe.  However, it boxes value types, making it a
            // costly operation performance wise.  For basic types, creating a similar static Equals method with
            // the IEquatable interface is a faster workaround.
            bool isEqual = object.Equals(obj1, obj2);
            bool isRefEqual = ReferenceEquals(obj1, obj2);
            Assert(!isEqual && !isRefEqual);
            
            // Yarn is a custom class which implements IEquatable<T>.  It also overrides object.Equals().
            Yarn pinkYarn = new Yarn(210, Yarn.YarnWeight.SuperBulky);
            Yarn multiColorYarn = new Yarn(70, Yarn.YarnWeight.Bulky);
            Yarn anotherPinkYarn = new Yarn(210, Yarn.YarnWeight.SuperBulky);
            
            Assert(pinkYarn.Equals(anotherPinkYarn));
            Assert(pinkYarn != anotherPinkYarn); // == still maintains reference equality
            
            Assert(!pinkYarn.Equals(multiColorYarn));

            // We can overload == and != (if you overload ==, != must also be overloaded)
            // changing their behavior.  In my custom string class MyString, == and != check for value equality.
            string day = "Friday the 26th";
            string dayAgain = "Friday the 26th";
            
            MyString myday = new MyString("Saturday the 27th");
            MyString mydayAgain = new MyString("Saturday the 27th");
            
            // Strings are actually a bad example.  Unlike Java,
            // == is overloaded for strings to check for value equality.  I would argue this is
            // a confusing aspect of operator overloading, and is one of the main reasons Java doesn't allow it.
            Assert(day == dayAgain && myday == mydayAgain);
            
            // Structural equality (value equality) can be applied to arrays with the IStructuralEquatable interface.
            // This interface uses the same equality methodology which is used for structs where all their properties
            // are checked for equality.
            string[] strings = { "went", "kayaking", "today" };
            string[] strings2 = { "went", "kayaking", "today" };
            
            // False for normal reference equality.
            Assert(!strings.Equals(strings2));

            // True for value equality.
            IStructuralEquatable seStrings = strings;
            Assert(seStrings.Equals(strings2, StringComparer.CurrentCulture));
            
            // Use the WrappingPaper custom struct to demonstrate struct equality (value equality)
            var wrappingPaper1 = new WrappingPaper("Hallmark", "Disney Princess");
            var wrappingPaper2 = wrappingPaper1;
            var wrappingPaper3 = new WrappingPaper("Hallmark", "Disney Princess");
            var wrappingPaper4 = new WrappingPaper("Unknown", "Plain");
            
            // Since structs don't implement == or != by default, they check for value equality just like Equals().
            Assert(wrappingPaper1 == wrappingPaper2);
            Assert(wrappingPaper1.Equals(wrappingPaper2));
            
            Assert(wrappingPaper2 == wrappingPaper3);
            Assert(wrappingPaper2.Equals(wrappingPaper3));
            
            Assert(wrappingPaper3 != wrappingPaper4);
            Assert(!wrappingPaper3.Equals(wrappingPaper4));
        }
    }
}
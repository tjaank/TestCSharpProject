using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestApplication.Classes;


namespace TestApplication
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            decimal d1;
            decimal d2;
            int maxDigitLimit = 3; // specify to any digit

            d1 = randomDecimal(rnd, maxDigitLimit);
            d2 = randomDecimal(rnd, maxDigitLimit);

            lblRandomDecimal1.Text = d1.ToString();
            lblRandomDecimal2.Text = d2.ToString();
            lblSubtractionResult.Text = (d1 - d2).ToString();

            string ClientIP = Request.UserHostAddress;

            Fraction f1 = new Fraction("1/2");
            Fraction f2 = new Fraction("2/3");

            lblSubtractionResult.Text = (f1 + f2).ToString();
        }
        public decimal randomDecimal(Random randomNumberGenerator, int maxDigitLimit)
        {
            int precision = randomNumberGenerator.Next(2, 5);
            int scale = randomNumberGenerator.Next(2, precision);

            if (randomNumberGenerator == null)
                throw new ArgumentNullException("randomNumberGenerator");
            if (!(precision >= 1 && precision <= 28))
                throw new ArgumentOutOfRangeException("precision", precision, "Precision must be between 1 and 28.");
            if (!(scale >= 0 && scale <= precision))
                throw new ArgumentOutOfRangeException("scale", precision, "Scale must be between 0 and precision.");

            Decimal d = 0m;
            for (int i = 0; i < precision; i++)
            {
                int r = randomNumberGenerator.Next(0, 10);
                d = d * 10m + r;
            }
            for (int s = 0; s < scale; s++)
            {
                d /= 10m;
            }
            if (randomNumberGenerator.Next(2) == 1)
                d = decimal.Negate(d);

            if (d.ToString().Replace(".", string.Empty).Replace("-", string.Empty).Length != 3)
            {
                d = getXDigitDecimal(d, maxDigitLimit);
            }

            return d;
        }
        public decimal getXDigitDecimal(decimal inputNumber, int maxDigitLimit)
        {
            Random rnd = new Random();
            decimal returnNumber = inputNumber;
            bool signed = inputNumber < 0 ? true : false;
            string num = inputNumber.ToString().Replace("-", string.Empty);

            int integerPortion = (int)Math.Truncate(inputNumber);
            int intLength = integerPortion.ToString().Length;

            decimal decimalPortion = (decimal)(inputNumber - integerPortion);
            int decLength = decimalPortion.ToString().Length;

            int decimalPointIndex = num.IndexOf(".");

            if (decimalPointIndex > 0)
            {
                if (num.Replace(".", string.Empty).Length > maxDigitLimit)
                {
                    num = num.Replace(".", string.Empty).Substring(0, maxDigitLimit);
                    num = num.Insert(rnd.Next(1, maxDigitLimit - 1), ".");
                }
                else
                {
                    for (int i = 0; i <= (maxDigitLimit - (num.Length - 1)); i++)
                    {
                        num = num.Insert(0, rnd.Next(1, 9).ToString());
                    }
                    //num = num.Substring(0, maxDigitLimit);
                }
            }
            else
            {
                if (num.Length > maxDigitLimit)
                {
                    num = num.Substring(0, maxDigitLimit);
                }
                else if (num.Length < maxDigitLimit)
                {
                    for (int i = 0; i <= (maxDigitLimit - num.Length); i++)
                    {
                        num += rnd.Next(1, 9).ToString();
                    }
                    //num = num.Substring(0, maxDigitLimit);
                }
                num = num.Insert(rnd.Next(0, maxDigitLimit - 1), ".");
            }
            if (signed)
                num = num.Insert(0, "-");

            return Convert.ToDecimal(num);
        }

        protected void btnDoIt_Click(object sender, EventArgs e)
        {
            Fraction f1 = new Fraction(txt1a.Text + "/" + txt1b.Text);
            Fraction f2 = new Fraction(txt2a.Text + "/" + txt2b.Text);
            switch(ddlFunction.SelectedValue)
            {
                case "+":
                    lblResult.Text = (f1 + f2).ToString();
                    break;
                case "-":
                    lblResult.Text = (f1 - f2).ToString();
                    break;
                case "/":
                    lblResult.Text = (f1 / f2).ToString();
                    break;
                case "*":
                    lblResult.Text = (f1 * f2).ToString();
                    break;
                default: break;
            }
        }
    }
    public class Fraction
    {
        /// <summary>
        /// Class attributes/members
        /// </summary>
        long m_iNumerator;
        long m_iDenominator;

        /// <summary>
        /// Constructors
        /// </summary>
        public Fraction()
        {
            Initialize(0, 1);
        }

        public Fraction(long iWholeNumber)
        {
            Initialize(iWholeNumber, 1);
        }

        public Fraction(double dDecimalValue)
        {
            Fraction temp = ToFraction(dDecimalValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(string strValue)
        {
            Fraction temp = ToFraction(strValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(long iNumerator, long iDenominator)
        {
            Initialize(iNumerator, iDenominator);
        }

        /// <summary>
        /// Internal function for constructors
        /// </summary>
        private void Initialize(long iNumerator, long iDenominator)
        {
            Numerator = iNumerator;
            Denominator = iDenominator;
            ReduceFraction(this);
        }

        /// <summary>
        /// Properites
        /// </summary>
        public long Denominator
        {
            get
            { return m_iDenominator; }
            set
            {
                if (value != 0)
                    m_iDenominator = value;
                else
                    throw new FractionException("Denominator cannot be assigned a ZERO Value");
            }
        }

        public long Numerator
        {
            get
            { return m_iNumerator; }
            set
            { m_iNumerator = value; }
        }

        public long Value
        {
            set
            {
                m_iNumerator = value;
                m_iDenominator = 1;
            }
        }

        /// <summary>
        /// The function returns the current Fraction object as double
        /// </summary>
        public double ToDouble()
        {
            return ((double)this.Numerator / this.Denominator);
        }

        /// <summary>
        /// The function returns the current Fraction object as a string
        /// </summary>
        public override string ToString()
        {
            string str;
            if (this.Denominator == 1)
                str = this.Numerator.ToString();
            else
                str = this.Numerator + "/" + this.Denominator;
            return str;
        }
        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        public static Fraction ToFraction(string strValue)
        {
            int i;
            for (i = 0; i < strValue.Length; i++)
                if (strValue[i] == '/')
                    break;

            if (i == strValue.Length)		// if string is not in the form of a fraction
                // then it is double or integer
                return (Convert.ToDouble(strValue));
            //return ( ToFraction( Convert.ToDouble(strValue) ) );

            // else string is in the form of Numerator/Denominator
            long iNumerator = Convert.ToInt64(strValue.Substring(0, i));
            long iDenominator = Convert.ToInt64(strValue.Substring(i + 1));
            return new Fraction(iNumerator, iDenominator);
        }


        /// <summary>
        /// The function takes a floating point number as an argument 
        /// and returns its corresponding reduced fraction
        /// </summary>
        public static Fraction ToFraction(double dValue)
        {
            try
            {
                checked
                {
                    Fraction frac;
                    if (dValue % 1 == 0)	// if whole number
                    {
                        frac = new Fraction((long)dValue);
                    }
                    else
                    {
                        double dTemp = dValue;
                        long iMultiple = 1;
                        string strTemp = dValue.ToString();
                        while (strTemp.IndexOf("E") > 0)	// if in the form like 12E-9
                        {
                            dTemp *= 10;
                            iMultiple *= 10;
                            strTemp = dTemp.ToString();
                        }
                        int i = 0;
                        while (strTemp[i] != '.')
                            i++;
                        int iDigitsAfterDecimal = strTemp.Length - i - 1;
                        while (iDigitsAfterDecimal > 0)
                        {
                            dTemp *= 10;
                            iMultiple *= 10;
                            iDigitsAfterDecimal--;
                        }
                        frac = new Fraction((int)Math.Round(dTemp), iMultiple);
                    }
                    return frac;
                }
            }
            catch (OverflowException)
            {
                throw new FractionException("Conversion not possible due to overflow");
            }
            catch (Exception)
            {
                throw new FractionException("Conversion not possible");
            }
        }

        /// <summary>
        /// The function replicates current Fraction object
        /// </summary>
        public Fraction Duplicate()
        {
            Fraction frac = new Fraction();
            frac.Numerator = Numerator;
            frac.Denominator = Denominator;
            return frac;
        }

        /// <summary>
        /// The function returns the inverse of a Fraction object
        /// </summary>
        public static Fraction Inverse(Fraction frac1)
        {
            if (frac1.Numerator == 0)
                throw new FractionException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

            long iNumerator = frac1.Denominator;
            long iDenominator = frac1.Numerator;
            return (new Fraction(iNumerator, iDenominator));
        }


        /// <summary>
        /// Operators for the Fraction object
        /// includes -(unary), and binary opertors such as +,-,*,/
        /// also includes relational and logical operators such as ==,!=,<,>,<=,>=
        /// </summary>
        public static Fraction operator -(Fraction frac1)
        { return (Negate(frac1)); }

        public static Fraction operator +(Fraction frac1, Fraction frac2)
        { return (Add(frac1, frac2)); }

        public static Fraction operator +(int iNo, Fraction frac1)
        { return (Add(frac1, new Fraction(iNo))); }

        public static Fraction operator +(Fraction frac1, int iNo)
        { return (Add(frac1, new Fraction(iNo))); }

        public static Fraction operator +(double dbl, Fraction frac1)
        { return (Add(frac1, Fraction.ToFraction(dbl))); }

        public static Fraction operator +(Fraction frac1, double dbl)
        { return (Add(frac1, Fraction.ToFraction(dbl))); }

        public static Fraction operator -(Fraction frac1, Fraction frac2)
        { return (Add(frac1, -frac2)); }

        public static Fraction operator -(int iNo, Fraction frac1)
        { return (Add(-frac1, new Fraction(iNo))); }

        public static Fraction operator -(Fraction frac1, int iNo)
        { return (Add(frac1, -(new Fraction(iNo)))); }

        public static Fraction operator -(double dbl, Fraction frac1)
        { return (Add(-frac1, Fraction.ToFraction(dbl))); }

        public static Fraction operator -(Fraction frac1, double dbl)
        { return (Add(frac1, -Fraction.ToFraction(dbl))); }

        public static Fraction operator *(Fraction frac1, Fraction frac2)
        { return (Multiply(frac1, frac2)); }

        public static Fraction operator *(int iNo, Fraction frac1)
        { return (Multiply(frac1, new Fraction(iNo))); }

        public static Fraction operator *(Fraction frac1, int iNo)
        { return (Multiply(frac1, new Fraction(iNo))); }

        public static Fraction operator *(double dbl, Fraction frac1)
        { return (Multiply(frac1, Fraction.ToFraction(dbl))); }

        public static Fraction operator *(Fraction frac1, double dbl)
        { return (Multiply(frac1, Fraction.ToFraction(dbl))); }

        public static Fraction operator /(Fraction frac1, Fraction frac2)
        { return (Multiply(frac1, Inverse(frac2))); }

        public static Fraction operator /(int iNo, Fraction frac1)
        { return (Multiply(Inverse(frac1), new Fraction(iNo))); }

        public static Fraction operator /(Fraction frac1, int iNo)
        { return (Multiply(frac1, Inverse(new Fraction(iNo)))); }

        public static Fraction operator /(double dbl, Fraction frac1)
        { return (Multiply(Inverse(frac1), Fraction.ToFraction(dbl))); }

        public static Fraction operator /(Fraction frac1, double dbl)
        { return (Multiply(frac1, Fraction.Inverse(Fraction.ToFraction(dbl)))); }

        public static bool operator ==(Fraction frac1, Fraction frac2)
        { return frac1.Equals(frac2); }

        public static bool operator !=(Fraction frac1, Fraction frac2)
        { return (!frac1.Equals(frac2)); }

        public static bool operator ==(Fraction frac1, int iNo)
        { return frac1.Equals(new Fraction(iNo)); }

        public static bool operator !=(Fraction frac1, int iNo)
        { return (!frac1.Equals(new Fraction(iNo))); }

        public static bool operator ==(Fraction frac1, double dbl)
        { return frac1.Equals(new Fraction(dbl)); }

        public static bool operator !=(Fraction frac1, double dbl)
        { return (!frac1.Equals(new Fraction(dbl))); }

        public static bool operator <(Fraction frac1, Fraction frac2)
        { return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator; }

        public static bool operator >(Fraction frac1, Fraction frac2)
        { return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator; }

        public static bool operator <=(Fraction frac1, Fraction frac2)
        { return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator; }

        public static bool operator >=(Fraction frac1, Fraction frac2)
        { return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator; }


        /// <summary>
        /// overloaed user defined conversions: from numeric data types to Fractions
        /// </summary>
        public static implicit operator Fraction(long lNo)
        { return new Fraction(lNo); }
        public static implicit operator Fraction(double dNo)
        { return new Fraction(dNo); }
        public static implicit operator Fraction(string strNo)
        { return new Fraction(strNo); }

        /// <summary>
        /// overloaed user defined conversions: from fractions to double and string
        /// </summary>
        public static explicit operator double(Fraction frac)
        { return frac.ToDouble(); }

        public static implicit operator string(Fraction frac)
        { return frac.ToString(); }

        /// <summary>
        /// checks whether two fractions are equal
        /// </summary>
        public override bool Equals(object obj)
        {
            Fraction frac = (Fraction)obj;
            return (Numerator == frac.Numerator && Denominator == frac.Denominator);
        }

        /// <summary>
        /// returns a hash code for this fraction
        /// </summary>
        public override int GetHashCode()
        {
            return (Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF));
        }

        /// <summary>
        /// internal function for negation
        /// </summary>
        private static Fraction Negate(Fraction frac1)
        {
            long iNumerator = -frac1.Numerator;
            long iDenominator = frac1.Denominator;
            return (new Fraction(iNumerator, iDenominator));

        }

        /// <summary>
        /// internal functions for binary operations
        /// </summary>
        private static Fraction Add(Fraction frac1, Fraction frac2)
        {
            try
            {
                checked
                {
                    long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
                    long iDenominator = frac1.Denominator * frac2.Denominator;
                    return (new Fraction(iNumerator, iDenominator));
                }
            }
            catch (OverflowException)
            {
                throw new FractionException("Overflow occurred while performing arithemetic operation");
            }
            catch (Exception)
            {
                throw new FractionException("An error occurred while performing arithemetic operation");
            }
        }

        private static Fraction Multiply(Fraction frac1, Fraction frac2)
        {
            try
            {
                checked
                {
                    long iNumerator = frac1.Numerator * frac2.Numerator;
                    long iDenominator = frac1.Denominator * frac2.Denominator;
                    return (new Fraction(iNumerator, iDenominator));
                }
            }
            catch (OverflowException)
            {
                throw new FractionException("Overflow occurred while performing arithemetic operation");
            }
            catch (Exception)
            {
                throw new FractionException("An error occurred while performing arithemetic operation");
            }
        }

        /// <summary>
        /// The function returns GCD of two numbers (used for reducing a Fraction)
        /// </summary>
        private static long GCD(long iNo1, long iNo2)
        {
            // take absolute values
            if (iNo1 < 0) iNo1 = -iNo1;
            if (iNo2 < 0) iNo2 = -iNo2;

            do
            {
                if (iNo1 < iNo2)
                {
                    long tmp = iNo1;  // swap the two operands
                    iNo1 = iNo2;
                    iNo2 = tmp;
                }
                iNo1 = iNo1 % iNo2;
            } while (iNo1 != 0);
            return iNo2;
        }

        /// <summary>
        /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
        /// and denominator by their GCD
        /// </summary>
        public static void ReduceFraction(Fraction frac)
        {
            try
            {
                if (frac.Numerator == 0)
                {
                    frac.Denominator = 1;
                    return;
                }

                long iGCD = GCD(frac.Numerator, frac.Denominator);
                frac.Numerator /= iGCD;
                frac.Denominator /= iGCD;

                if (frac.Denominator < 0)	// if -ve sign in denominator
                {
                    //pass -ve sign to numerator
                    frac.Numerator *= -1;
                    frac.Denominator *= -1;
                }
            } // end try
            catch (Exception exp)
            {
                throw new FractionException("Cannot reduce Fraction: " + exp.Message);
            }
        }

    }	//end class Fraction


    /// <summary>
    /// Exception class for Fraction, derived from System.Exception
    /// </summary>
    public class FractionException : Exception
    {
        public FractionException()
            : base()
        { }

        public FractionException(string Message)
            : base(Message)
        { }

        public FractionException(string Message, Exception InnerException)
            : base(Message, InnerException)
        { }
    }	//end class FractionException
    public static class RandomDecimal
    {
        public static int NextInt32(this Random rng)
        {
            unchecked
            {
                int firstBits = rng.Next(0, 1 << 2) << 2;
                int lastBits = rng.Next(0, 1 << 2);
                return firstBits | lastBits;
            }
        }

        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(4);
            bool sign = rng.Next(2) == 1;
            decimal d = new decimal(rng.NextInt32(),
                               rng.NextInt32(),
                               rng.NextInt32(),
                               sign,
                               scale);
            //while (!(d.ToString().IndexOf(".") > 0))
            //{
            //    d = new decimal(rng.NextInt32(),
            //                   rng.NextInt32(),
            //                   rng.NextInt32(),
            //                   sign,
            //                   scale);
            //}
            if (!(d.ToString().IndexOf(".") > 0))
            {
                d = formatDecimal(d);
            }

            return d;
        }
        private static decimal formatDecimal(decimal val)
        {
            decimal newValue = ((decimal)(val * 100)) / 100m;
            return Convert.ToDecimal((val.ToString("N2")));
        }
        public static decimal randomDecimal(Random randomNumberGenerator)
        {
            int precision = randomNumberGenerator.Next(2, 5);
            int scale = randomNumberGenerator.Next(2, precision);

            if (randomNumberGenerator == null)
                throw new ArgumentNullException("randomNumberGenerator");
            if (!(precision >= 1 && precision <= 28))
                throw new ArgumentOutOfRangeException("precision", precision, "Precision must be between 1 and 28.");
            if (!(scale >= 0 && scale <= precision))
                throw new ArgumentOutOfRangeException("scale", precision, "Scale must be between 0 and precision.");

            Decimal d = 0m;
            for (int i = 0; i < precision; i++)
            {
                int r = randomNumberGenerator.Next(0, 10);
                d = d * 10m + r;
            }
            for (int s = 0; s < scale; s++)
            {
                d /= 10m;
            }
            if (randomNumberGenerator.Next(2) == 1)
                d = decimal.Negate(d);
            return d;
        }

    }
}

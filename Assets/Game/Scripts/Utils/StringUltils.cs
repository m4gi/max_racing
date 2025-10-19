using System.Globalization;
using UnityEngine;

public static class StringUltils 
{
    /// <summary>
    /// Formats a large number into a short, readable string (K, M, B, T).
    /// Example: 100000 -> "100K", 1500000 -> "1.5M"
    /// </summary>
    /// <param name="number">The number to format.</param>
    /// <returns>The formatted string.</returns>
    public static string FormatNumber(double number)
    {
        if (number < 1000)
        {
            // If the number is less than 1000, return it as an integer string.
            return ((int)number).ToString();
        }

        if (number < 1000000) // Less than 1 million -> format as K
        {
            // Divide by 1000 and append "K".
            // "0.#" will show one decimal place if needed (e.g., 1.5K)
            // and won't show it if it's a whole number (e.g., 100K instead of 100.0K).
            return (number / 1000f).ToString("0.#", CultureInfo.InvariantCulture) + "K";
        }

        if (number < 1000000000) // Less than 1 billion -> format as M
        {
            // Divide by 1 million and append "M".
            return (number / 1000000f).ToString("0.#", CultureInfo.InvariantCulture) + "M";
        }

        if (number < 1000000000000) // Less than 1 trillion -> format as B
        {
            // Divide by 1 billion and append "B".
            return (number / 1000000000f).ToString("0.#", CultureInfo.InvariantCulture) + "B";
        }
        
        // From 1 trillion upwards -> format as T
        return (number / 1000000000000f).ToString("0.#", CultureInfo.InvariantCulture) + "T";
    }
    
    /// <summary>
    /// Overload for formatting an integer.
    /// </summary>
    public static string FormatNumber(int number)
    {
        return FormatNumber((double)number);
    }

    /// <summary>
    /// Overload for formatting a long integer.
    /// </summary>
    public static string FormatNumber(long number)
    {
        return FormatNumber((double)number);
    }

    /// <summary>
    /// Overload for formatting a float.
    /// </summary>
    public static string FormatNumber(float number)
    {
        return FormatNumber((double)number);
    }
}

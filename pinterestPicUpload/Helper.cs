using System;

namespace PinBot
{

  /// <summary>
  /// Class for static methodes / helpers
  /// </summary>
  public static class Helper
  {
    /// <summary>
    /// Random TimeSpan between given arguments.
    /// </summary>
    /// <param name="ts1"></param>
    /// <param name="ts2"></param>
    /// <returns></returns>
    /// <remarks>ts1 > ts2 is still valid since random extensions take care of that.</remarks>
    public static TimeSpan GetRandomTimeSpan(TimeSpan ts1, TimeSpan ts2)
    {
      var r = new Random();
      return new TimeSpan(r.RandomLong(ts1.Ticks, ts2.Ticks));
    }
  }

  /// <summary>
  /// Extension to proivde random long.
  /// <remarks>http://stackoverflow.com/a/6651656/2248674</remarks>
  /// </summary>
  public static class RandomExtensions
  {
    public static long RandomLong(this Random rnd)
    {
      byte[] buffer = new byte[8];
      rnd.NextBytes(buffer);
      return BitConverter.ToInt64(buffer, 0);
    }

    public static long RandomLong(this Random rnd, long min, long max)
    {
      EnsureMinLEQMax(ref min, ref max);
      long numbersInRange = unchecked(max - min + 1);
      if (numbersInRange < 0)
        throw new ArgumentException("Size of range between min and max must be less than or equal to Int64.MaxValue");

      long randomOffset = RandomLong(rnd);
      if (IsModuloBiased(randomOffset, numbersInRange))
        return RandomLong(rnd, min, max); // Try again
      else
        return min + PositiveModuloOrZero(randomOffset, numbersInRange);
    }

    static bool IsModuloBiased(long randomOffset, long numbersInRange)
    {
      long greatestCompleteRange = numbersInRange * (long.MaxValue / numbersInRange);
      return randomOffset > greatestCompleteRange;
    }

    static long PositiveModuloOrZero(long dividend, long divisor)
    {
      long mod;
      Math.DivRem(dividend, divisor, out mod);
      if (mod < 0)
        mod += divisor;
      return mod;
    }

    static void EnsureMinLEQMax(ref long min, ref long max)
    {
      if (min <= max)
        return;
      long temp = min;
      min = max;
      max = temp;
    }
  }
}

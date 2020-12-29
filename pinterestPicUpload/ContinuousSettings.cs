using System;

namespace PinBot
{
  /// <summary>
  /// Model that keeps settings for continuous run
  /// </summary>
  class ContinuousSettings
  {
    /// <summary>
    /// Indicating whether event should occur more times
    /// </summary>
    public bool ContinuousRun { get; set; }

    /// <summary>
    /// Start of delay interval
    /// </summary>
    public TimeSpan DelayFrom { get; set; }

    /// <summary>
    /// End of delay interval
    /// </summary>
    public TimeSpan DelayTo { get; set; }

    /// <summary>
    /// Default parameterless constructor
    /// </summary>
    public ContinuousSettings() {}

    /// <summary>
    /// Constructior for GUI - with datetime for intervals
    /// </summary>
    /// <param name="continuousRun"></param>
    /// <param name="delayFrom"></param>
    /// <param name="delayTo"></param>
    public ContinuousSettings(bool continuousRun, DateTime delayFrom, DateTime delayTo)
    {
      ContinuousRun = continuousRun;
      DelayFrom = new TimeSpan(delayFrom.Hour, delayFrom.Minute, delayFrom.Second);
      DelayTo = new TimeSpan(delayTo.Hour, delayTo.Minute, delayTo.Second);
    }
  }
}
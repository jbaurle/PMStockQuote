using System;

namespace PMStockQuote
{
	class TimeoutWatch
	{
		bool _isInfinite;
		TimeSpan _timeout;
		DateTime _creationTime;

		public static TimeSpan Infinite { get { return TimeSpan.MaxValue; } }

		public bool IsExpired
		{
			get
			{
				if(_isInfinite)
					return false;

				return RemainingTimeout < TimeSpan.Zero;
			}
		}

		public TimeSpan RemainingTimeout
		{
			get
			{
				if(_isInfinite)
					return Infinite;

				return _timeout.Subtract(DateTime.Now.Subtract(_creationTime));
			}
		}

		public TimeoutWatch(TimeSpan timeout)
		{
			_creationTime = DateTime.Now;
			_timeout = timeout;

			if(_timeout.Equals(Infinite))
				_isInfinite = true;
		}

		public void Reset()
		{
			_creationTime = DateTime.Now;
		}

		public void ThrowIfTimeoutExpired(string exceptionMessage)
		{
			if(RemainingTimeout < TimeSpan.Zero)
				throw new TimeoutException(exceptionMessage);
		}

		public TimeSpan GetRemainingTimeoutAndThrowIfExpired(string exceptionMessage)
		{
			if(_isInfinite)
				return Infinite;

			ThrowIfTimeoutExpired(exceptionMessage);

			return RemainingTimeout;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using NP.ObjectComparison.Analyzers;
using NP.ObjectComparison.Analyzers.Results;
using NP.ObjectComparison.Exceptions;

namespace NP.ObjectComparison
{
	/// <summary>
	/// An object that tracks changes for a specific instance of <typeparamref name="TObject"/>
	/// </summary>
	/// <typeparam name="TObject">The type to be tracked.</typeparam>
	public class ComparisonTracker<TObject> : IComparisonTracker<TObject>, IDisposable
	{
		private bool _isDisposed = false;
		private readonly Func<TObject, TObject> _cloneFunc;
		private TObject _original;
		private TObject _current;
		private bool _isOriginalSet = false;
		private IObjectAnalyzer<TObject> _analyzer = new SimpleObjectAnalyzer<TObject>();
		private DiffSnapshot[] _analyzeResults;

		/// <inheritdoc />
		public TObject Original
		{
			get
			{
				ThrowIfDisposed();
				return _original;
			}
			private set
			{
				_original = value;
				_isOriginalSet = true;
			}
		}

		/// <inheritdoc />
		public TObject Current
		{
			get
			{
				ThrowIfDisposed();
				return _current;
			}
			set
			{
				ThrowIfDisposed();
				_current = value;

				if (!_isOriginalSet)
				{
					CloneToOriginal();
				}

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="current">The object to be tracked.</param>
		/// <remarks>
		/// The <see cref="TObject"/> has to implement <seealso cref="ICloneable"/> to use this constructor.
		/// </remarks>
		public ComparisonTracker(TObject current)
		{
			Current = current;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="current">The object to be tracked.</param>
		/// <param name="clone">A function that clones the <see cref="TObject"/></param>
		public ComparisonTracker(TObject current, Func<TObject, TObject> clone)
		{
			_cloneFunc = clone;
			Current = current;
		}
		/// <inheritdoc />
		public void Analyze()
		{
			ThrowIfDisposed();
			_analyzeResults = _analyzer.Analyze(Original, Current).ToArray();
		}

		/// <inheritdoc />
		public bool HasChanges(bool autoAnalyze = false)
		{
			ThrowIfDisposed();
			if (autoAnalyze)
			{
				Analyze();
			}
			return _analyzeResults.HasChanges();
		}

		/// <inheritdoc />
		public bool IsPatched()
		{
			ThrowIfDisposed();
			return false;
		}

		/// <inheritdoc />
		public void Patch()
		{
			ThrowIfDisposed();
		}

		/// <inheritdoc />
		public void Reset(bool toCurrent = false)
		{
			ThrowIfDisposed();
		}

		#region Clone

		private void CloneToOriginal()
		{
			Original = CloneCurrentValue();
		}

		private TObject CloneCurrentValue() => CloneValue(Current);

		private TObject CloneValue(TObject value)
		{
			if (_cloneFunc != null)
			{
				return _cloneFunc.Invoke(value);
			}

			if (value == null)
			{
				return default;
			}

			if (value is ICloneable cloneable)
			{
				return (TObject)cloneable.Clone();
			}

			throw new ObjectComparisonException(Resources.Errors.CannotClone);
		}

		#endregion

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(nameof(ComparisonTracker<TObject>));
			}
		}

		/// <inheritdoc />
		public void Dispose()
		{
			if (_isDisposed)
			{
				return;
			}
			_isDisposed = true;
		}

		#region Casts

		/// <summary>
		/// Casts to <typeparamref name="TObject" />.
		/// </summary>
		/// <param name="tracker"></param>
		/// <returns></returns>
		public static implicit operator TObject(ComparisonTracker<TObject> tracker) => tracker == null ? default : tracker.Current;

		/// <summary>
		/// Casts from <typeparamref name="TObject" />.
		/// </summary>
		/// <param name="currentValue"></param>
		/// <returns></returns>
		public static implicit operator ComparisonTracker<TObject>(TObject currentValue)
		{
			return new ComparisonTracker<TObject>(currentValue);
		}

		#endregion
	}
}
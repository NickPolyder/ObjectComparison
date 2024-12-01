using System.Collections.Generic;
using NP.ObjectComparison.Analyzers.Results;

namespace NP.ObjectComparison.Analyzers
{
	/// <summary>
	/// Provides analyzing functionality for <typeparamref name="TInstance"/>.
	/// </summary>
	/// <typeparam name="TInstance"></typeparam>
	public interface IObjectAnalyzer<in TInstance>
	{
		/// <summary>
		/// Analyzes two <typeparamref name="TInstance"/> objects and provides the diff result.
		/// </summary>
		/// <param name="originalInstance"></param>
		/// <param name="targetInstance"></param>
		/// <returns></returns>
		IEnumerable<DiffSnapshot> Analyze(TInstance originalInstance, TInstance targetInstance);
	}
}
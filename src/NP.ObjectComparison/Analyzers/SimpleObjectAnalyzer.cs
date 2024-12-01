using System.Collections.Generic;
using System.Reflection;
using NP.ObjectComparison.Analyzers.Results;

namespace NP.ObjectComparison.Analyzers
{
	public class SimpleObjectAnalyzer<TInstance>: IObjectAnalyzer<TInstance>
	{
		private static PropertyInfo[] _cachedProperties = typeof(TInstance).GetProperties();
		/// <inheritdoc />
		public IEnumerable<DiffSnapshot> Analyze(TInstance originalInstance, TInstance targetInstance)
		{
			if (originalInstance == null && targetInstance == null)
			{
				yield break;
			}else if (originalInstance == null)
			{
				yield return DiffSnapshot.Create(builder =>
				{
					builder
						.HasChanges(true)
						.SetOriginalValue(null)
						.SetNewValue(targetInstance)
						.SetName("$");
				});
				yield break;
			}
			else if (targetInstance == null)
			{
				yield return DiffSnapshot.Create(builder =>
				{
					builder
						.HasChanges(true)
						.SetOriginalValue(originalInstance)
						.SetNewValue(null)
						.SetName("$");
				});
				yield break;
			}

			foreach (var cachedProperty in _cachedProperties)
			{
				var originalValue = cachedProperty.GetValue(originalInstance);
				var targetValue = cachedProperty.GetValue(targetInstance);

				yield return DiffSnapshot.Create(builder =>
				{
					builder
						.HasChanges(object.Equals(originalValue, targetValue) == false)
						.SetOriginalValue(originalValue)
						.SetNewValue(targetValue)
						.SetPrefix("$")
						.SetName(cachedProperty.Name);
				});
				
			}
		}


	}
}
﻿using System.Collections.Generic;
using System.Linq;
using ObjectComparison.Results;

namespace ObjectComparison.Patch
{
	public abstract class BaseDictionaryPatch<TInstance, TKey, TValue> : IPatchInfo<TInstance>
	{
		protected readonly DictionaryObjectInfo<TInstance, TKey, TValue> ObjectInfo;

		protected BaseDictionaryPatch(DictionaryObjectInfo<TInstance, TKey, TValue> objectInfo)
		{
			ObjectInfo = objectInfo;
		}

		public IEnumerable<ObjectItem> Patch(TInstance originalInstance, TInstance targetInstance)
		{
			if (originalInstance == null)
			{
				yield break;
			}

			if (targetInstance == null)
			{
				yield break;
			}

			var original = ObjectInfo.Get(originalInstance);
			var target = ObjectInfo.Get(targetInstance);

			var deletedItems = HandleDeletedItems(original, target).ToArray();

			foreach (var item in deletedItems)
			{
				yield return item;
			}

			var addedItems = HandleAddedItems(original, target).ToArray();

			foreach (var item in addedItems)
			{
				yield return item;
			}

			var modifiedItems = HandleModifiedItems(original, target).ToArray();

			foreach (var item in modifiedItems)
			{
				yield return item;
			}

			var hasChanges = deletedItems.Length > 0
			                 || addedItems.Length > 0
			                 || modifiedItems.Any(item => item.HasChanges);

			if (hasChanges)
			{
				ObjectInfo.Set(originalInstance, target);
			}
		}

		protected virtual IEnumerable<ObjectItem> HandleDeletedItems(IDictionary<TKey, TValue> original, IDictionary<TKey, TValue> target)
		{
			var deletedKeys = original.Keys
				.Where(originalKey =>
					!target.Keys.Any(targetKey => ObjectInfo.IsKeyEquals(originalKey, targetKey)))
				.ToArray();
			if (deletedKeys.Length == 0)
			{
				yield break;
			}

			foreach (var deletedKey in deletedKeys)
			{
				var originalItem = original[deletedKey];
				var patchInfoBuilder = new ObjectItem.Builder()
					.SetName($"{ObjectInfo.GetName()}[{deletedKey}]")
					.SetOriginalValue(originalItem)
					.SetNewValue(null);

				patchInfoBuilder.HasChanges();
				yield return patchInfoBuilder.Build();
			}
		}

		protected virtual IEnumerable<ObjectItem> HandleAddedItems(IDictionary<TKey, TValue> original, IDictionary<TKey, TValue> target)
		{
			var addedKeys = target.Keys
				.Where(targetKey =>
					!original.Keys.Any(originalKey => ObjectInfo.IsKeyEquals(originalKey, targetKey)))
				.ToArray();
			
			if (addedKeys.Length == 0)
			{
				yield break;
			}

			foreach (var addedKey in addedKeys)
			{
				var targetItem = target[addedKey];
				var patchInfoBuilder = new ObjectItem.Builder()
					.SetName($"{ObjectInfo.GetName()}[{addedKey}]")
					.SetOriginalValue(null)
					.SetNewValue(targetItem);

				patchInfoBuilder.HasChanges();
				yield return patchInfoBuilder.Build();
			}
		}

		protected abstract IEnumerable<ObjectItem> HandleModifiedItems(IDictionary<TKey, TValue> original, IDictionary<TKey, TValue> target);
	}
}
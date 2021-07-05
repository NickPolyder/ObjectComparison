﻿using ObjectComparison.Patch.Strategies;

namespace ObjectComparison.Patch
{
	public static class PatchSingletons
	{
		public static readonly IPatchBuilderStrategy ObjectBuilderStrategy = new ObjectPatchBuilderStrategy();
		public static readonly IPatchBuilderStrategy ArrayBuilderStrategy = new ArrayPatchBuilderStrategy();
		public static readonly IPatchBuilderStrategy DictionaryBuilderStrategy = new DictionaryPatchBuilderStrategy();

	}
}
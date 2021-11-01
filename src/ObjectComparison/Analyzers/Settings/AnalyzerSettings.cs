﻿using System.Collections.Generic;

namespace ObjectComparison.Analyzers.Settings
{
	/// <summary>
	/// Settings associated with <see cref="AnalyzerBuilder{T}"/>.
	/// </summary>
	public class AnalyzerSettings
	{
		/// <summary>
		/// Depth settings.
		/// </summary>
		public DepthSettings Depth { get; set; } = new DepthSettings();

		/// <summary>
		/// Skip analyze settings.
		/// </summary>
		public SkipAnalyzeSettings SkipAnalyzeSettings { get; set; } = new SkipAnalyzeSettings();
	}
}
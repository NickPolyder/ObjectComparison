﻿using System;
using ObjectComparison.Analyzers;
using ObjectComparison.Analyzers.Infos;
using Shouldly;
using Xunit;

namespace ObjectComparison.Tests.Patch
{
	public class ObjectPatchTests
	{
		[Fact]
		public void Ctor_WhenObjectInfoIsNull_ThrowArgumentNullException()
		{
			// Arrange

			// Act
			var result = Record.Exception(() => new ObjectAnalyzer<object, object>(null));

			// Assert
			result.ShouldBeOfType<ArgumentNullException>()
				.ParamName.ShouldBe("objectInfo");
		}

		[Fact]
		public void Ctor_WhenObjectInfoIsNotNull_ThrowArgumentNullException()
		{
			// Arrange
			var objectInfo = new ObjectInfo<object, object>(() => "", _ => null, (x,y) => { });

			// Act
			var result = Record.Exception(() => new ObjectAnalyzer<object, object>(objectInfo));

			// Assert
			result.ShouldBeNull();
		}
	}
}
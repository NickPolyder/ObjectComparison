﻿using System;
using AutoFixture;
using ObjectPatcher.Results;
using Shouldly;
using Xunit;

namespace ObjectPatcher.Tests.Results
{
	public class PatchItemTests
	{
		private IFixture _fixture;

		public PatchItemTests()
		{
			_fixture = new Fixture();
		}

		[Fact]
		public void Build_WhenSetNameIsNotCalledShouldThrowException()
		{
			// Arrange
			var sut = new PatchItem.Builder();

			// Act
			var result = Record.Exception(() => sut.Build());

			// Assert
			result.ShouldBeOfType<ArgumentNullException>()
				.ParamName.ShouldBe("_name");
		}

		[Fact]
		public void Build_WhenSetNameValue_ShouldPopulate_NameValue()
		{
			// Arrange
			var nameValue = _fixture.Create<string>();
			var sut = new PatchItem.Builder()
				.SetName(nameValue);

			// Act
			var result = sut.Build();

			// Assert
			result.Name.ShouldBe(nameValue);
		}

		[Fact]
		public void Build_WhenSetOriginalValue_ShouldPopulate_OriginalValue()
		{
			// Arrange
			var originalValue = _fixture.Create<object>();
			var sut = new PatchItem.Builder()
				.SetName(_fixture.Create<string>())
				.SetOriginalValue(originalValue);

			// Act
			var result = sut.Build();

			// Assert
			result.OriginalValue.ShouldBe(originalValue);
		}

		[Fact]
		public void Build_WhenSetNewValue_ShouldPopulate_NewValue()
		{
			// Arrange
			var newValue = _fixture.Create<object>();
			var sut = new PatchItem.Builder()
				.SetName(_fixture.Create<string>())
				.SetNewValue(newValue);

			// Act
			var result = sut.Build();

			// Assert
			result.NewValue.ShouldBe(newValue);
		}

		[Fact]
		public void Build_WhenSetHasChanges_ShouldPopulate_HasChanges()
		{
			// Arrange
			var sut = new PatchItem.Builder()
				.SetName(_fixture.Create<string>())
				.HasChanges();

			// Act
			var result = sut.Build();

			// Assert
			result.HasChanges.ShouldBeTrue();
		}
	}
}
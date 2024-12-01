using AutoFixture;
using NP.ObjectComparison.Tests.Mocks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace NP.ObjectComparison.Tests;

[Trait("Category", "Comparison Tracker")]
public class ComparisonTrackerTests
{
	private readonly ITestOutputHelper _log;
	private readonly IFixture _fixture;

	public ComparisonTrackerTests(ITestOutputHelper log)
	{
		_log = log;
		_fixture = new AutoFixture.Fixture();
	}

	[Fact]
	public void A_Change_ToAnObject_ShouldResult_In_HasChanges_True()
	{
		// Assign
		var objToTrack = new TestObject();
		var sut = new ComparisonTracker<TestObject>(objToTrack);

		// Act
		objToTrack.FirstProperty = _fixture.Create<string>();

		sut.Analyze();

		// Assert
		sut.HasChanges().ShouldBe(true);
	}


	[Fact]
	public void No_Changes_ToAnObject_ShouldResult_In_HasChanges_False()
	{
		// Assign
		var objToTrack = new TestObject
		{
			FirstProperty = _fixture.Create<string>()
		};
		var sut = new ComparisonTracker<TestObject>(objToTrack);

		// Act
		sut.Analyze();

		// Assert
		sut.HasChanges().ShouldBe(false);
	}

	[Fact]
	public void A_Change_ToAnObject_WithoutInheritingFromEquatable_ShouldResult_In_HasChanges_True()
	{
		// Assign
		var objToTrack = new ATestObjectWithoutEquatable();
		var sut = new ComparisonTracker<ATestObjectWithoutEquatable>(objToTrack);

		// Act
		objToTrack.FirstProperty = _fixture.Create<string>();

		sut.Analyze();

		// Assert
		sut.HasChanges().ShouldBe(true);
	}


	[Fact]
	public void No_Changes_ToAnObject_WithoutInheritingFromEquatable_ShouldResult_In_HasChanges_False()
	{
		// Assign
		var objToTrack = new ATestObjectWithoutEquatable
		{
			FirstProperty = _fixture.Create<string>()
		};
		var sut = new ComparisonTracker<ATestObjectWithoutEquatable>(objToTrack);

		// Act
		sut.Analyze();

		// Assert
		sut.HasChanges().ShouldBe(false);
	}
}
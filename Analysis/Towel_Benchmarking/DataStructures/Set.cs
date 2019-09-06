﻿using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.SetHashArray)]
	public class SetHashArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			ISet<Person> set = new SetHashArray<Person>(
				(a, b) => a.Id == b.Id,
				x => x.Id.GetHashCode());
			foreach (Person person in RandomTestData)
			{
				set.Add(person);
			}
		}
	}

	[Benchmarks(Tag.DataStructures, Tag.SetHashLinked)]
	public class SetHashLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			ISet<Person> set = new SetHashLinked<Person>(
				(a, b) => a.Id == b.Id,
				x => x.Id.GetHashCode());
			foreach (Person person in RandomTestData)
			{
				set.Add(person);
			}
		}
	}
}

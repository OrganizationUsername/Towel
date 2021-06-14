﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Towel;
using static Towel.Statics;

namespace Towel_Generating
{
	public static class LinkGenerator
	{
		public static string Run(int size = Program.DefaultGenerationCount)
		{
			string generatorPath = Path.GetRelativePath(Path.Combine(Path.GetDirectoryName(sourcefilepath())!, "..", ".."), sourcefilepath());
 
			StringBuilder code = new();
			code.AppendLine($@"//------------------------------------------------------------------------------");
			code.AppendLine($@"// <auto-generated>");
			code.AppendLine($@"// This code was generated from ""{generatorPath}"".");
			code.AppendLine($@"// </auto-generated>");
			code.AppendLine($@"//------------------------------------------------------------------------------");
			code.AppendLine($@"");
			code.AppendLine($@"using System;");
			code.AppendLine($@"using static Towel.Statics;");
			code.AppendLine($@"");
			code.AppendLine($@"namespace Towel.DataStructures");
			code.AppendLine($@"{{");
			code.AppendLine($@"	/// <summary>Represents a link between objects.</summary>");
			code.AppendLine($@"	public interface Link : IDataStructure<object>, System.Runtime.CompilerServices.ITuple");
			code.AppendLine($@"	{{");
			code.AppendLine($@"		#region Properties");
			code.AppendLine($@"");
			code.AppendLine($@"		int System.Runtime.CompilerServices.ITuple.Length => Size;");
			code.AppendLine($@"");
			code.AppendLine($@"		/// <summary>The number of values in the tuple.</summary>");
			code.AppendLine($@"		int Size {{ get; }}");
			code.AppendLine($@"");
			code.AppendLine($@"		#endregion");
			code.AppendLine($@"	}}");
			for (int i = 1, I = 2; i <= size; i++, I++)
			{
				code.AppendLine($@"");
				code.AppendLine($@"	/// <summary>Represents a link between objects.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"	/// <typeparam name=""T{j}"">The type of #{j} value in the link.</typeparam>");
				}
				code.AppendLine($@"	public class Link<{Join(1..I, n => $"T{n}", ", ")}> : Link, ICloneable<Link<{Join(1..I, n => $"T{n}", ", ")}>>");
				code.AppendLine($@"	{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <summary>The #{j} value of the link.</summary>");
					code.AppendLine($@"		public T{j} Value{j} {{ get; set; }}");
				}
				code.AppendLine($@"");
				code.AppendLine($@"		#region Constructors");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Constructs a link of values.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <param name=""value{j}"">The #{j} value to be linked.</param>");
				}
				code.AppendLine($@"		public Link({Join(1..I, n => $"T{n} value{n}", ", ")})");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			Value{j} = value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Properties");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public int Size => {i};"); //public object this[int index] => throw new NotImplementedException();
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public object this[int index]");
				code.AppendLine($@"		{{");
				code.AppendLine($@"			get => index switch");
				code.AppendLine($@"			{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"				{j} => Value{j},");
				}
				code.AppendLine($@"				_ => throw new IndexOutOfRangeException($""{{nameof(index)}} < 1 || {i} < {{nameof(index)}}""),");
				code.AppendLine($@"			}};");
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Operators");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a tuple to a link.</summary>");
				code.AppendLine($@"		/// <param name=""tuple"">The tuple to convert to a link.</param>");
				code.AppendLine($@"		public static implicit operator Link<{Join(1..I, n => $"T{n}", ", ")}>({(i is 1 ? "ValueTuple<" :"(")}{Join(1..I, n => $"T{n}", ", ")}{(i is 1 ? ">" : ")")} tuple) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"tuple.Item{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a link to a tuple.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The link to convert to a tuple.</param>");
				code.AppendLine($@"		public static implicit operator {(i is 1 ? "ValueTuple<" : "(")}{Join(1..I, n => $"T{n}", ", ")}{(i is 1 ? ">" : ")")}(Link<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			{(i is 1 ? $"new(" : "(")}{Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a tuple to a link.</summary>");
				code.AppendLine($@"		/// <param name=""tuple"">The tuple to convert to a link.</param>");
				code.AppendLine($@"		public static implicit operator Link<{Join(1..I, n => $"T{n}", ", ")}>(Tuple<{Join(1..I, n => $"T{n}", ", ")}> tuple) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"tuple.Item{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a link to a tuple.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The link to convert to a tuple.</param>");
				code.AppendLine($@"		public static implicit operator Tuple<{Join(1..I, n => $"T{n}", ", ")}>(Link<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a class link to a struct link.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The class link to convert to a struct link.</param>");
				code.AppendLine($@"		public static implicit operator Link<{Join(1..I, n => $"T{n}", ", ")}>(LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Methods");
				code.AppendLine($@"");
				code.AppendLine($@"		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public System.Collections.Generic.IEnumerator<object> GetEnumerator()");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			yield return Value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Gets the types of the values of this link.</summary>");
				code.AppendLine($@"		/// <returns>The types of the values of this link.</returns>");
				code.AppendLine($@"		public Type[] Types() => new[] {{ {Join(1..I, n => $"typeof(T{n})", ", ")} }};");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public StepStatus StepperBreak<TStep>(TStep step = default)");
				code.AppendLine($@"			where TStep : struct, IFunc<object, StepStatus>");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			if (step.Invoke(Value{j}) is Break) return Break;");
				}
				code.AppendLine($@"			return Continue;");
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public Link<{Join(1..I, n => $"T{n}", ", ")}> Clone() => new({Join(1..I, n => $"Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public object[] ToArray() => new object[] {{ {Join(1..I, n => $"Value{n}", ", ")} }};");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public override int GetHashCode() => HashCode.Combine({Join(1..I, n => $"Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public override bool Equals(object obj) =>");
				code.AppendLine($@"			obj is LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> linkStruct && Equals(linkStruct) ||");
				code.AppendLine($@"			obj is Link<{Join(1..I, n => $"T{n}", ", ")}> link && Equals(link) ||");
				code.AppendLine($@"			obj is ValueTuple<{Join(1..I, n => $"T{n}", ", ")}> valueTuple && Equals(valueTuple) ||");
				code.AppendLine($@"			obj is Tuple<{Join(1..I, n => $"T{n}", ", ")}> tuple && Equals(tuple);");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Check for equality with another link.</summary>");
				code.AppendLine($@"		/// <param name=""b"">The other link to check for equality with.</param>");
				code.AppendLine($@"		/// <returns>True if equal; false if not.</returns>");
				code.AppendLine($@"		public bool Equals(Link<{Join(1..I, n => $"T{n}", ", ")}> b) =>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			Equate(Value{j}, b.Value{j}){(j == i ? ";" : " &&")}");
					//code.AppendLine($@"			Value{j}.Equals(b.Value{j}){(j == i ? ";" : " &&")}");
				}
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Deconstructs the link.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <param name=""value{j}"">The #{j} value of the link.</param>");
				}
				code.AppendLine($@"		public void Deconstruct({Join(1..I, n => $"out T{n} value{n}", ", ")})");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			value{j} = Value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"	}}");
				code.AppendLine($@"");
				code.AppendLine($@"	/// <summary>Represents a link between objects.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"	/// <typeparam name=""T{j}"">The type of #{j} value in the link.</typeparam>");
				}
				code.AppendLine($@"	public struct LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> : Link, ICloneable<LinkStruct<{Join(1..I, n => $"T{n}", ", ")}>>");
				code.AppendLine($@"	{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <summary>The #{j} value of the link.</summary>");
					code.AppendLine($@"		public T{j} Value{j} {{ get; set; }}");
				}
				code.AppendLine($@"");
				code.AppendLine($@"		#region Constructors");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Creates a link between objects.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <param name=""value{j}"">The #{j} value to be linked.</param>");
				}
				code.AppendLine($@"		public LinkStruct({Join(1..I, n => $"T{n} value{n}", ", ")})");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			Value{j} = value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Properties");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public int Size => {i};");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public object this[int index]");
				code.AppendLine($@"		{{");
				code.AppendLine($@"			get => index switch");
				code.AppendLine($@"			{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"				{j} => Value{j},");
				}
				code.AppendLine($@"				_ => throw new IndexOutOfRangeException($""{{nameof(index)}} < 1 || {i} < {{nameof(index)}}""),");
				code.AppendLine($@"			}};");
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Operators");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a tuple to a link.</summary>");
				code.AppendLine($@"		/// <param name=""tuple"">The tuple to convert to a link.</param>");
				code.AppendLine($@"		public static implicit operator LinkStruct<{Join(1..I, n => $"T{n}", ", ")}>({(i is 1 ? "ValueTuple<" : "(")}{Join(1..I, n => $"T{n}", ", ")}{(i is 1 ? ">" : ")")} tuple) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"tuple.Item{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a link to a tuple.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The link to convert to a tuple.</param>");
				code.AppendLine($@"		public static implicit operator {(i is 1 ? "ValueTuple<" : "(")}{Join(1..I, n => $"T{n}", ", ")}{(i is 1 ? ">" : ")")}(LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			{(i is 1 ? $"new(" : "(")}{Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a tuple to a link.</summary>");
				code.AppendLine($@"		/// <param name=""tuple"">The tuple to convert to a link.</param>");
				code.AppendLine($@"		public static implicit operator LinkStruct<{Join(1..I, n => $"T{n}", ", ")}>(Tuple<{Join(1..I, n => $"T{n}", ", ")}> tuple) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"tuple.Item{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a link to a tuple.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The link to convert to a tuple.</param>");
				code.AppendLine($@"		public static implicit operator Tuple<{Join(1..I, n => $"T{n}", ", ")}>(LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Converts a class link to a struct link.</summary>");
				code.AppendLine($@"		/// <param name=""link"">The class link to convert to a struct link.</param>");
				code.AppendLine($@"		public static implicit operator LinkStruct<{Join(1..I, n => $"T{n}", ", ")}>(Link<{Join(1..I, n => $"T{n}", ", ")}> link) =>");
				code.AppendLine($@"			new({Join(1..I, n => $"link.Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"");
				code.AppendLine($@"		#region Methods");
				code.AppendLine($@"");
				code.AppendLine($@"		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public System.Collections.Generic.IEnumerator<object> GetEnumerator()");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			yield return Value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Gets the types of the values of this link.</summary>");
				code.AppendLine($@"		/// <returns>The types of the values of this link.</returns>");
				code.AppendLine($@"		public Type[] Types() => new[] {{ {Join(1..I, n => $"typeof(T{n})", ", ")} }};");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public StepStatus StepperBreak<TStep>(TStep step = default)");
				code.AppendLine($@"			where TStep : struct, IFunc<object, StepStatus>");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			if (step.Invoke(Value{j}) is Break) return Break;");
				}
				code.AppendLine($@"			return Continue;");
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> Clone() => new({Join(1..I, n => $"Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public object[] ToArray() => new object[] {{ {Join(1..I, n => $"Value{n}", ", ")} }};");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public override int GetHashCode() => HashCode.Combine({Join(1..I, n => $"Value{n}", ", ")});");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <inheritdoc/>");
				code.AppendLine($@"		public override bool Equals(object obj) =>");
				code.AppendLine($@"			obj is LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> linkStruct && Equals(linkStruct) ||");
				code.AppendLine($@"			obj is Link<{Join(1..I, n => $"T{n}", ", ")}> link && Equals(link) ||");
				code.AppendLine($@"			obj is ValueTuple<{Join(1..I, n => $"T{n}", ", ")}> valueTuple && Equals(valueTuple) ||");
				code.AppendLine($@"			obj is Tuple<{Join(1..I, n => $"T{n}", ", ")}> tuple && Equals(tuple);");
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Check for equality with another link.</summary>");
				code.AppendLine($@"		/// <param name=""b"">The other link to check for equality with.</param>");
				code.AppendLine($@"		/// <returns>True if equal; false if not.</returns>");
				code.AppendLine($@"		public bool Equals(LinkStruct<{Join(1..I, n => $"T{n}", ", ")}> b) =>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			Equate(Value{j}, b.Value{j}){(j == i ? ";" : " &&")}");
					//code.AppendLine($@"			Value{j}.Equals(b.Value{j}){(j == i ? ";" : " &&")}");
				}
				code.AppendLine($@"");
				code.AppendLine($@"		/// <summary>Deconstructs the link.</summary>");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"		/// <param name=""value{j}"">The #{j} value of the link.</param>");
				}
				code.AppendLine($@"		public void Deconstruct({Join(1..I, n => $"out T{n} value{n}", ", ")})");
				code.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					code.AppendLine($@"			value{j} = Value{j};");
				}
				code.AppendLine($@"		}}");
				code.AppendLine($@"");
				code.AppendLine($@"		#endregion");
				code.AppendLine($@"	}}");
			}
			code.AppendLine($@"}}");
			return code.ToString();
		}
	}
}
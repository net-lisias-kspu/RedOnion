using MoonSharp.Interpreter;
using RedOnion.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace RedOnion.KSP.API
{
	[Description("Read-only list (or set). Enumerable (can be used in `foreach`)."
		+ "\nUsed e.g. for parts and all lists and sets you are not allowed to modify.")]
	public class ReadOnlyList<T> : IList<T>
	{
		protected ListCore<T> list;
		protected internal Action Refresh;
		protected internal bool Dirty { get; set; } = true;
		protected internal virtual void SetDirty() => Dirty = true;

		[Browsable(false), MoonSharpHidden]
		public void EnsureValid()
		{
			if (Dirty)
				DoRefresh();
		}

		protected virtual void DoRefresh()
		{
			Dirty = false;
			Refresh?.Invoke();
		}

		protected internal ReadOnlyList() { }
		protected internal ReadOnlyList(Action refresh) => Refresh = refresh;

		[Description("Number of elements in the list (or set).")]
		public int count => Count;
		[Browsable(false), MoonSharpHidden]
		public int Count
		{
			get
			{
				if (Dirty) DoRefresh();
				return list.Count;
			}
		}
		[Description("Get element by index. Will throw exception if index is out of range.")]
		public T this[int index]
		{
			get
			{
				if (Dirty) DoRefresh();
				return list[index];
			}
		}
		[Description("Get index of element. -1 if not found.")]
		public int indexOf(T item) => IndexOf(item);
		[Browsable(false), MoonSharpHidden]
		public int IndexOf(T item)
		{
			if (Dirty) DoRefresh();
			return list.IndexOf(item);
		}
		[Description("Test wether the list (or set) contains specified element.")]
		public bool contains(T item) => Contains(item);
		[Browsable(false), MoonSharpHidden]
		public virtual bool Contains(T item)
		{
			if (Dirty) DoRefresh();
			return list.Contains(item);
		}
		[Browsable(false), MoonSharpHidden]
		public void CopyTo(T[] array, int index)
		{
			if (Dirty) DoRefresh();
			list.CopyTo(array, index);
		}

		protected internal virtual void Clear() => list.Clear();
		protected internal virtual bool Add(T item)
		{
			list.Add(item);
			return true;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		[Browsable(false), MoonSharpHidden]
		public virtual IEnumerator<T> GetEnumerator()
		{
			if (Dirty) DoRefresh();
			return list.GetEnumerator();
		}

		bool ICollection<T>.IsReadOnly => true;
		T IList<T>.this[int index]
		{
			get => this[index];
			set => throw new NotImplementedException();
		}
		void ICollection<T>.Add(T item) => throw new NotImplementedException();
		void ICollection<T>.Clear() => throw new NotImplementedException();
		bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
		void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
		void IList<T>.RemoveAt(int index) => throw new NotImplementedException();
	}
}

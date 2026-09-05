using System;
using System.Collections;
using System.Collections.Generic;

namespace CircularBuffer;

public class CircularBuffer<T> : IEnumerable<T>, IEnumerable
{
	private readonly T[] i9EncLLYgnuWTav4vLX6iIc;

	private int _8uASgcP8GVc8OJp_0024K_0024HzZ4;

	private int QIxykOrn3_0024XrxDqArHSu5P4;

	private int tK5U38sqdhNGeOAf2tzt4zU;

	public int Capacity => i9EncLLYgnuWTav4vLX6iIc.Length;

	public bool IsFull => Size == Capacity;

	public bool IsEmpty => Size == 0;

	public int Size => tK5U38sqdhNGeOAf2tzt4zU;

	public T this[int index]
	{
		get
		{
			if (IsEmpty)
			{
				throw smethod_4(smethod_3(global::_003CModule_003E.smethod_25<string>(56641647u), (object)index));
			}
			if (index >= tK5U38sqdhNGeOAf2tzt4zU)
			{
				throw smethod_4(smethod_5(global::_003CModule_003E.smethod_27<string>(2686244366u), (object)index, (object)tK5U38sqdhNGeOAf2tzt4zU));
			}
			int num = veqBdTBb_0024Mzezq6_uFJjSgA(index);
			return i9EncLLYgnuWTav4vLX6iIc[num];
		}
		set
		{
			if (IsEmpty)
			{
				throw smethod_4(smethod_3(global::_003CModule_003E.smethod_29<string>(1982149856u), (object)index));
			}
			if (index >= tK5U38sqdhNGeOAf2tzt4zU)
			{
				throw smethod_4(smethod_5(global::_003CModule_003E.smethod_27<string>(2686244366u), (object)index, (object)tK5U38sqdhNGeOAf2tzt4zU));
			}
			int num = veqBdTBb_0024Mzezq6_uFJjSgA(index);
			i9EncLLYgnuWTav4vLX6iIc[num] = value;
		}
	}

	public CircularBuffer(int int_0)
		: this(int_0, new T[0])
	{
	}

	public CircularBuffer(int int_0, T[] gparam_0)
	{
		if (int_0 >= 1)
		{
			if (gparam_0 != null)
			{
				if (gparam_0.Length <= int_0)
				{
					i9EncLLYgnuWTav4vLX6iIc = new T[int_0];
					smethod_2((Array)gparam_0, (Array)i9EncLLYgnuWTav4vLX6iIc, gparam_0.Length);
					tK5U38sqdhNGeOAf2tzt4zU = gparam_0.Length;
					_8uASgcP8GVc8OJp_0024K_0024HzZ4 = 0;
					QIxykOrn3_0024XrxDqArHSu5P4 = ((tK5U38sqdhNGeOAf2tzt4zU != int_0) ? tK5U38sqdhNGeOAf2tzt4zU : 0);
					return;
				}
				throw smethod_0(global::_003CModule_003E.smethod_26<string>(2105935852u), global::_003CModule_003E.smethod_26<string>(620818847u));
			}
			throw smethod_1(global::_003CModule_003E.smethod_27<string>(2932873852u));
		}
		throw smethod_0(global::_003CModule_003E.smethod_25<string>(1070394896u), global::_003CModule_003E.smethod_25<string>(229840537u));
	}

	public T Front()
	{
		UpoIf4pGOARJnOBIlzuMg8U(global::_003CModule_003E.smethod_28<string>(771462395u));
		return i9EncLLYgnuWTav4vLX6iIc[_8uASgcP8GVc8OJp_0024K_0024HzZ4];
	}

	public T Back()
	{
		UpoIf4pGOARJnOBIlzuMg8U(global::_003CModule_003E.smethod_26<string>(3953483829u));
		return i9EncLLYgnuWTav4vLX6iIc[((QIxykOrn3_0024XrxDqArHSu5P4 != 0) ? QIxykOrn3_0024XrxDqArHSu5P4 : Capacity) - 1];
	}

	public void PushBack(T item)
	{
		if (IsFull)
		{
			i9EncLLYgnuWTav4vLX6iIc[QIxykOrn3_0024XrxDqArHSu5P4] = item;
			g_0h1BGDvKypfx3zokX97_U(ref QIxykOrn3_0024XrxDqArHSu5P4);
			_8uASgcP8GVc8OJp_0024K_0024HzZ4 = QIxykOrn3_0024XrxDqArHSu5P4;
		}
		else
		{
			i9EncLLYgnuWTav4vLX6iIc[QIxykOrn3_0024XrxDqArHSu5P4] = item;
			g_0h1BGDvKypfx3zokX97_U(ref QIxykOrn3_0024XrxDqArHSu5P4);
			tK5U38sqdhNGeOAf2tzt4zU++;
		}
	}

	public void PushFront(T item)
	{
		if (IsFull)
		{
			ezZoc2L_hKt5jYpvjocmG5A(ref _8uASgcP8GVc8OJp_0024K_0024HzZ4);
			QIxykOrn3_0024XrxDqArHSu5P4 = _8uASgcP8GVc8OJp_0024K_0024HzZ4;
			i9EncLLYgnuWTav4vLX6iIc[_8uASgcP8GVc8OJp_0024K_0024HzZ4] = item;
		}
		else
		{
			ezZoc2L_hKt5jYpvjocmG5A(ref _8uASgcP8GVc8OJp_0024K_0024HzZ4);
			i9EncLLYgnuWTav4vLX6iIc[_8uASgcP8GVc8OJp_0024K_0024HzZ4] = item;
			tK5U38sqdhNGeOAf2tzt4zU++;
		}
	}

	public void PopBack()
	{
		UpoIf4pGOARJnOBIlzuMg8U(global::_003CModule_003E.smethod_26<string>(4232587733u));
		ezZoc2L_hKt5jYpvjocmG5A(ref QIxykOrn3_0024XrxDqArHSu5P4);
		i9EncLLYgnuWTav4vLX6iIc[QIxykOrn3_0024XrxDqArHSu5P4] = default(T);
		tK5U38sqdhNGeOAf2tzt4zU--;
	}

	public void PopFront()
	{
		UpoIf4pGOARJnOBIlzuMg8U(global::_003CModule_003E.smethod_29<string>(1278866411u));
		i9EncLLYgnuWTav4vLX6iIc[_8uASgcP8GVc8OJp_0024K_0024HzZ4] = default(T);
		g_0h1BGDvKypfx3zokX97_U(ref _8uASgcP8GVc8OJp_0024K_0024HzZ4);
		tK5U38sqdhNGeOAf2tzt4zU--;
	}

	public T[] ToArray()
	{
		T[] array = new T[Size];
		int num = 0;
		ArraySegment<T>[] array2 = new ArraySegment<T>[2]
		{
			Dass6w6VTFSxyymeE7L2aP0(),
			X8CtLwmyF_QcE4nXlDQ7Swo()
		};
		for (int i = 0; i < array2.Length; i++)
		{
			ArraySegment<T> arraySegment = array2[i];
			smethod_6((Array)arraySegment.Array, arraySegment.Offset, (Array)array, num, arraySegment.Count);
			num += arraySegment.Count;
		}
		return array;
	}

	public IEnumerator<T> GetEnumerator()
	{
		ArraySegment<T>[] array = new ArraySegment<T>[2]
		{
			Dass6w6VTFSxyymeE7L2aP0(),
			X8CtLwmyF_QcE4nXlDQ7Swo()
		};
		ArraySegment<T>[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			ArraySegment<T> arraySegment = array2[i];
			for (int j = 0; j < arraySegment.Count; j++)
			{
				yield return arraySegment.Array[arraySegment.Offset + j];
			}
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private void UpoIf4pGOARJnOBIlzuMg8U(string string_0 = "Cannot access an empty buffer.")
	{
		if (IsEmpty)
		{
			throw smethod_7(string_0);
		}
	}

	private void g_0h1BGDvKypfx3zokX97_U(ref int int_0)
	{
		if (++int_0 == Capacity)
		{
			int_0 = 0;
		}
	}

	private void ezZoc2L_hKt5jYpvjocmG5A(ref int int_0)
	{
		if (int_0 == 0)
		{
			int_0 = Capacity;
		}
		int_0--;
	}

	private int veqBdTBb_0024Mzezq6_uFJjSgA(int int_0)
	{
		return _8uASgcP8GVc8OJp_0024K_0024HzZ4 + ((int_0 < Capacity - _8uASgcP8GVc8OJp_0024K_0024HzZ4) ? int_0 : (int_0 - Capacity));
	}

	private ArraySegment<T> Dass6w6VTFSxyymeE7L2aP0()
	{
		if (_8uASgcP8GVc8OJp_0024K_0024HzZ4 < QIxykOrn3_0024XrxDqArHSu5P4)
		{
			return new ArraySegment<T>(i9EncLLYgnuWTav4vLX6iIc, _8uASgcP8GVc8OJp_0024K_0024HzZ4, QIxykOrn3_0024XrxDqArHSu5P4 - _8uASgcP8GVc8OJp_0024K_0024HzZ4);
		}
		return new ArraySegment<T>(i9EncLLYgnuWTav4vLX6iIc, _8uASgcP8GVc8OJp_0024K_0024HzZ4, i9EncLLYgnuWTav4vLX6iIc.Length - _8uASgcP8GVc8OJp_0024K_0024HzZ4);
	}

	private ArraySegment<T> X8CtLwmyF_QcE4nXlDQ7Swo()
	{
		if (_8uASgcP8GVc8OJp_0024K_0024HzZ4 < QIxykOrn3_0024XrxDqArHSu5P4)
		{
			return new ArraySegment<T>(i9EncLLYgnuWTav4vLX6iIc, QIxykOrn3_0024XrxDqArHSu5P4, 0);
		}
		return new ArraySegment<T>(i9EncLLYgnuWTav4vLX6iIc, 0, QIxykOrn3_0024XrxDqArHSu5P4);
	}

	internal static ArgumentException smethod_0(string string_0, string string_1)
	{
		return new ArgumentException(string_0, string_1);
	}

	internal static ArgumentNullException smethod_1(string string_0)
	{
		return new ArgumentNullException(string_0);
	}

	internal static void smethod_2(Array array_0, Array array_1, int int_0)
	{
		Array.Copy(array_0, array_1, int_0);
	}

	internal static string smethod_3(string string_0, object object_0)
	{
		return string.Format(string_0, object_0);
	}

	internal static IndexOutOfRangeException smethod_4(string string_0)
	{
		return new IndexOutOfRangeException(string_0);
	}

	internal static string smethod_5(string string_0, object object_0, object object_1)
	{
		return string.Format(string_0, object_0, object_1);
	}

	internal static void smethod_6(Array array_0, int int_0, Array array_1, int int_1, int int_2)
	{
		Array.Copy(array_0, int_0, array_1, int_1, int_2);
	}

	internal static InvalidOperationException smethod_7(string string_0)
	{
		return new InvalidOperationException(string_0);
	}
}

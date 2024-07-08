//Класс SorterLib с методами сортировки
using Algorithms;
using System;

namespace Algorithms
{
    public static class SorterLib
    {
        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public static void InsertionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void SelectionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = array[j];
                    }
                }
                int temp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = temp;
            }
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);
                QuickSort(array, low, pi - 1);
                QuickSort(array, pi + 1, high);
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            return i + 1;
        }

        public static void MergeSort(int[] array)
        {
            MergeSort(array, 0, array.Length - 1);
        }

        private static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);

                Merge(array, left, middle, right);
            }
        }

        private static void Merge(int[] array, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, middle + 1, rightArray, 0, n2);

            int i = 0, j = 0;
            int k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }
    }
}

//Класс SearcherLib с методами поиска

using System;

namespace Algorithms
{
    public static class SearcherLib
    {
        public static int BinarySearch(int[] array, int key)
        {
            int left = 0, right = array.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (array[mid] == key)
                {
                    return mid;
                }
                else if (array[mid] < key)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }

        public static int InterpolationSearch(int[] array, int key)
        {
            int left = 0, right = array.Length - 1;
            while (left <= right && key >= array[left] && key <= array[right])
            {
                if (left == right)
                {
                    if (array[left] == key) return left;
                    return -1;
                }
                int pos = left + ((key - array[left]) * (right - left) / (array[right] - array[left]));
                if (array[pos] == key)
                {
                    return pos;
                }
                if (array[pos] < key)
                {
                    left = pos + 1;
                }
                else
                {
                    right = pos - 1;
                }
            }
            return -1;
        }

        public static int ExponentialSearch(int[] array, int key)
        {
            if (array.Length == 0)
                return -1;

            if (array[0] == key)
                return 0;

            int i = 1;
            while (i < array.Length && array[i] <= key)
                i *= 2;

            return BinarySearch(array, key, i / 2, Math.Min(i, array.Length));
        }

        private static int BinarySearch(int[] array, int key, int left, int right)
        {
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (array[mid] == key)
                {
                    return mid;
                }
                else if (array[mid] < key)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }
    }
}

//Используйте библиотеку в консольном приложении


using System;
using Algorithms;

namespace AlgorithmDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 34, 7, 23, 32, 5, 62 };
            Console.WriteLine("Original array:");
            PrintArray(array);

            SorterLib.BubbleSort(array);
            Console.WriteLine("Array after BubbleSort:");
            PrintArray(array);

            int key = 23;
            int index = SearcherLib.BinarySearch(array, key);
            Console.WriteLine(index >= 0
                ? $"Element {key} found at index {index} using BinarySearch"
                : $"Element {key} not found using BinarySearch");
        }

        static void PrintArray(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}

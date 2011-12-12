// NK1875.cpp : main project file.

#include "stdafx.h"

//using namespace System;
//
//int main(array<System::String ^> ^args)
//{
//    Console::WriteLine(L"Hello World");
//    return 0;
//}

/* qsort example */
#include <stdio.h>
#include <stdlib.h>

int __cdecl compare (const void * a, const void * b)
{
  return ( *(int*)a - *(int*)b );
}

int main()
{
	int TotalCases = 0, CurrentLength = 0, i = 0;
	scanf("%d", &TotalCases);
	while (i < TotalCases)
	{
		i++;
		scanf("%d", &CurrentLength);
		int *Vector1 = (int *)malloc((CurrentLength + 1) * sizeof(int));
		int *Vector2 = (int *)malloc((CurrentLength + 1) * sizeof(int));
		int j = 0;
		while (j < CurrentLength)
		{
			scanf("%d", &Vector1[j++]);
		}
		j = 0;
		while (j < CurrentLength)
		{
			scanf("%d", &Vector2[j++]);
		}
		//qsort(Vector1, CurrentLength, sizeof(int), compare);
		//qsort(Vector2, CurrentLength, sizeof(int), compare);
		sort(Vector1, Vector1 + CurrentLength);
		sort(Vector2, Vector2 + CurrentLength);
		long long sum = 0, head = 0, tail = 0;
		for(j = 0; j < CurrentLength; j++)
		{
			head = Vector1[j];
			tail = Vector2[CurrentLength - j - 1];
			sum += head * tail;
		}
		printf("Case #%d: %lld\n", i, sum);
		free(Vector1);
		free(Vector2);
	}
	return 0;
}

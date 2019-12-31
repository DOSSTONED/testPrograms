// Suduko_Enum_CPP.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;

void initialstand(int* p)
{
	for(int i = 0; i < 10; i++)
		p[i] = i;
}

int CheckSuduko(int* suduko)				// 0 for not right, 1 for right
{
	int stand[] = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[suduko[9 * i + j]] = 0;
		}
		for(int k = 0; k < 10; k++)
		{
			stand[0] += stand[k];
		}
		if(stand[0])
			return 0;
	}

	initialstand(stand);
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[suduko[9 * j + i]] = 0;
		}
		for(int k = 0; k < 10; k++)
		{
			stand[0] += stand[k];
		}
		if(stand[0])
			return 0;
	}

	initialstand(stand);
	for(int i = 0; i < 3; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 3; j++)
		{
			stand[suduko[27 * i + j]] = 0;
			stand[suduko[27 * i + j + 1]] = 0;
			stand[suduko[27 * i + j + 2]] = 0;
			stand[suduko[27 * (i + 1) + j]] = 0;
			stand[suduko[27 * (i + 1) + j + 1]] = 0;
			stand[suduko[27 * (i + 1) + j + 2]] = 0;
			stand[suduko[27 * (i + 2) + j]] = 0;
			stand[suduko[27 * (i + 2) + j + 1]] = 0;
			stand[suduko[27 * (i + 2) + j + 2]] = 0;
			for(int k = 0; k < 10; k++)
			{
				stand[0] += stand[k];
			}
			if(stand[0])
				return 0;
		}
	}
	return 1;
}

void addby1(int* a[], int length)
{
	int i = length - 1;
	int carry = 1;
	while(i)
	{
		if(carry)
		{
			if(*a[i] == 9)
			{
				*a[i] = 1;
			}
			else
			{
				*a[i] = *a[i] + 1;
				carry = 0;
			}
		}
		i--;
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
		
	system("MODE con: COLS=100 LINES=100");

	int Suduko[] = {
		0,0,2,0,5,0,0,0,6,
		0,9,0,0,0,0,0,0,0,
		0,0,4,0,0,7,0,5,0,
		0,0,7,0,4,0,0,0,3,
		0,5,0,1,0,0,0,8,0,
		2,0,0,0,0,0,4,0,0,
		0,1,0,9,0,0,7,0,0,
		0,0,0,0,0,0,0,9,0,
		8,0,0,0,7,0,3,0,0
	};
	int* CanBeTried[81];
	int CanBeTriedNumber = 0;


	CanBeTriedNumber = 0;
	for(int i = 0;i < 81; i++)
	{
		if(Suduko[i] == 0)
		{
			CanBeTried[CanBeTriedNumber] = &Suduko[i];
			CanBeTriedNumber++;
		}
	}

	while(!CheckSuduko(Suduko))
	{
		addby1(CanBeTried, CanBeTriedNumber);
		for(int i = 0; i < CanBeTriedNumber; i++)
		{
			cout<<*CanBeTried[i];
		}
		cout<<endl;
	}

	for(int i = 0; i < 9; i++)
	{
		for(int j = 0; j < 9; j++)
		{
			cout<<Suduko[9 * i + j]<<" ";
		}
		cout<<endl;
	}
	return 0;
}


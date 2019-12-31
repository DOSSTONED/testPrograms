// Sudoko_Enum_Optimised_CPP.cpp : 定义控制台应用程序的入口点。
//


// #5339


#include "stdafx.h"
#include <iostream>
#include <cmath>
using namespace std;

int Sudoko1[] = {
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
const int standard = 1022;

int CheckSudoko(int* Sudoko)				// 0 for not right, 1 for right
{
	
	int every9number = 0;
	for(int i = 0; i < 9; i++)
	{
		every9number = 0;
		for(int j = 0; j < 9; j++)
		{
			every9number += pow(2.0, Sudoko[9 * i + j]);
		}
		
		if(every9number != standard)
			return 0;
	}

	
	for(int i = 0; i < 9; i++)
	{
		every9number = 0;
		for(int j = 0; j < 9; j++)
		{
			every9number += pow(2.0, Sudoko[9 * j + i]);
		}
		
		if(every9number != standard)
			return 0;
	}

	
	for(int i = 0; i < 3; i++)
	{
		
		for(int j = 0; j < 3; j++)
		{
			every9number = 0;
			every9number += pow(2.0, Sudoko[27 * i + 3 * j]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 1]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 2]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 9]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 10]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 11]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 18]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 19]);
			every9number += pow(2.0, Sudoko[27 * i + 3 * j + 20]);
			
			if(every9number != standard)
				return 0;
		}
	}
	return 1;
}






void initialstand(int* p)
{
	for(int i = 0; i < 10; i++)
		p[i] = 0;
}


int JudgeSudoko(int* sudoko) // 1 for no problem, 0 for wrong sudoko
{
	int stand[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[sudoko[9 * i + j]]++;
		}
		for(int k = 1; k < 10; k++)
		{
			if(stand[k] > 1)
				return 0;
		}
		
	}

	initialstand(stand);
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[sudoko[9 * j + i]]++;
		}
		for(int k = 1; k < 10; k++)
		{
			if(stand[k] > 1)
				return 0;
		}
	}

	initialstand(stand);
	for(int i = 0; i < 3; i++)
	{
		
		for(int j = 0; j < 3; j++)
		{
			initialstand(stand);
			stand[sudoko[27 * i + 3 * j]]++;
			stand[sudoko[27 * i + 3 * j + 1]]++;
			stand[sudoko[27 * i + 3 * j + 2]]++;
			stand[sudoko[27 * i + 3 * j + 9]]++;
			stand[sudoko[27 * i + 3 * j + 10]]++;
			stand[sudoko[27 * i + 3 * j + 11]]++;
			stand[sudoko[27 * i + 3 * j + 18]]++;
			stand[sudoko[27 * i + 3 * j + 19]]++;
			stand[sudoko[27 * i + 3 * j + 20]]++;
			for(int k = 1; k < 10; k++)
			{
				if(stand[k] > 1)
				return 0;
			}
		}
	}
	return 1;
}


int fill1by1(int* a[], int length)
{
	int i = 0;
	for(i = 0; i < length; i++)
		if(!(*a[i]))
			break;
	int j = 0;
	for(j = 1; j < 10; j++)
	{
		*a[i] = j;
		if(JudgeSudoko(Sudoko1))
		{
			return 0;
		}
	}

		if(i == 0)
			return 1;
		else
		{
			*a[i] = 0;
			*a[i - 1] = *a[i - 1] + 1;
			int k = i - 1;
			while(k >= 0 && (*a[k]) > 9)
			{
				*a[k] = 0;
				*a[k - 1] = *a[k - 1] + 1;
				k--;
			}
		}
	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
		
	system("MODE con: COLS=100 LINES=100");

	
	int* CanBeTried[81];
	int CanBeTriedNumber = 0;


	CanBeTriedNumber = 0;
	for(int i = 0;i < 81; i++)
	{
		if(Sudoko1[i] == 0)
		{
			CanBeTried[CanBeTriedNumber] = &Sudoko1[i];
			CanBeTriedNumber++;
		}
	}

	while(!CheckSudoko(Sudoko1))
	{
		
		fill1by1(CanBeTried, CanBeTriedNumber);
		// to see the result

		/*
		for(int i = 0; i < CanBeTriedNumber; i++)
		{
			cout<<*CanBeTried[i];
		}
		cout<<endl;
		*/
	}

	for(int i = 0; i < 9; i++)
	{
		for(int j = 0; j < 9; j++)
		{
			cout<<Sudoko1[9 * i + j]<<" ";
		}
		cout<<endl;
	}
	return 0;
}



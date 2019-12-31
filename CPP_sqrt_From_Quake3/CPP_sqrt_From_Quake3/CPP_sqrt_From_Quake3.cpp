// CPP_sqrt_From_Quake3.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#include<math.h>
#include<assert.h>
#include<iostream>


float Q_rsqrt( float number );
const int maxRepeated = 99999;

int _tmain(int argc, _TCHAR* argv[])
{
	int round = 0;
	for (round = 1; round < 6; round++)
	{
		srand(round);
		int i = 0;
		clock_t start,finish;
		float array[maxRepeated], temp;
		double duration = 0;
		for (i = 0; i < maxRepeated; i++)
		{
			array[i] = (float)rand()/1000000;
		}
		
		start = clock(); 
		for (i = 0; i < maxRepeated; i++)
		{
			temp = sqrtf(array[i]);
		}
		finish = clock();
		duration   =   (double)(finish   -   start)   /   CLOCKS_PER_SEC; 
		printf("%f\t", duration);
		
		system("pause");
		
		start = clock(); 
		for (i = 0; i < maxRepeated; i++)
		{
			temp = Q_rsqrt(array[i]);
		}
		finish = clock();
		duration   =   (double)(finish   -   start)   /   CLOCKS_PER_SEC; 
		printf("%f\t", duration);
		system("pause");
		printf("\n");
	}
	return 0;
}


float Q_rsqrt( float number )
{
	long i;
	float x2, y;
	const float threehalfs = 1.5F;

	x2 = number * 0.5F;
	y  = number;
	i  = * ( long * ) &y;						// evil floating point bit level hacking
	i  = 0x5f3759df - ( i >> 1 );               // what the fuck?
	y  = * ( float * ) &i;
	y  = y * ( threehalfs - ( x2 * y * y ) );   // 1st iteration
//	y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

#ifndef Q3_VM
#ifdef __linux__
	assert( !isnan(y) ); // bk010122 - FPE?
#endif
#endif
	return y;
}

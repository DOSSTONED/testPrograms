// Pell_Function.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <cmath>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	// solve a^2-d*b^2==1

	double a = 0, b = 0;
	int d = 10;

	/*for(b = 1; b < 1000; b++)
	{
		a = sqrt(1 + d * b * b);
		if((a - (int)a) < 0.0000001)
		{
			printf("d = %d;b = %d;a = %d\n", d, (int)b, (int)a);
		}
	}*/

	int x = 1, y = 1;
	for(x = 1; x < 113; x++)
		for(y = 1; y < 113; y++)
			if((x*x+2*y*y+3)%225==0)
				printf("%d^2+2*%d^2+3=%d\n", x, y, x*x+2*y*y+3);
	system("pause");
	return 0;
}


// NK1877.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>
#include <math.h>


int main(array<System::String ^> ^args)
{
	int TotalCases = 0;
	int i = 0;
	int currentExp = 0;
	int An = 34, An_1 = 28, An_2 = 6;
	TotalCases = 200;
	//scanf("%d", &TotalCases);
	while(i < TotalCases)
	{
		An = 6*28-4*6, An_1 = 28, An_2 = 6;
		i++;
		currentExp = i;
		//scanf("%d", &currentExp);
		
		if(currentExp == 1)
		{
			printf("Case #%d: 005\r\n", i);
			continue;
		}
		if(currentExp == 2)
		{
			printf("Case #%d: 027\r\n", i);
			continue;
		}
		while(currentExp > 105)
		{
			currentExp = (currentExp - 5) % 100 + 5;
		}
		while (currentExp > 2)
		{
			currentExp--;
			An = (6 * An_1 - 4 * An_2) % 1000;
			An_2 = An_1;
			An_1 = An;

		}
		printf("Case #%d: %03d\r\n", i, (An < 1) ? An + 999 : An - 1);
		//printf("%03d", (An < 1) ? An + 999 : An - 1);
	}
    return 0;
}

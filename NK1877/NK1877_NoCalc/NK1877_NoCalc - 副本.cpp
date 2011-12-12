// NK1877_NoCalc.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>

int main(array<System::String ^> ^args)
{
	char* DisplayArray = "005027143751935607903991335047943471055447463991095607263151855527743351135407903791135647343471455847263191095807463551455527343951335207903591935247743471855247063391095007663951055527943551535007903391735847143471255647863591095207863351655527543151735807903191535447543471655047663791095407063751255527143751";
	int TotalCases = 0;
	int i = 0;
	int currentExp = 0;
	scanf("%d", &TotalCases);
	while(i < TotalCases)
	{
		i++;
		scanf("%d", &currentExp);
		while (currentExp > 104)
		{
			currentExp = currentExp - 100;
		}
		printf("Case #%d: %c%c%c\n", i, DisplayArray[3 * currentExp - 3], DisplayArray[3 * currentExp - 2], DisplayArray[3 * currentExp - 1]);
	}
    return 0;
}

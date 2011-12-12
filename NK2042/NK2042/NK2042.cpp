// NK2042.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>


int main(array<System::String ^> ^args)
{
	int TotalCases = 0, Round = 0, idx = 0;
	int Base = 1;
	int Length = 0;
	char Words[63];
	int Weight[36];
	scanf("%d", &TotalCases);
	long long SUM = 0;
	while(Round++ < TotalCases)
	{
		Length = scanf("%s", Words);
		for(idx = 0; idx < 36; idx++) Weight[idx] = -1;
		//for(idx = 0; idx < 63; idx++) Words[idx] = 0;
		Base = 0;
		SUM = 0;
		idx = 0;
		while (Words[idx] != 0)
		{
			if(Words[idx] <= '9')	// current word is a digit
				if(Weight[Words[idx] - '0'] == -1)
				{
					Weight[Words[idx] - '0'] = Base++;
				}
			if(Words[idx] >= 'a')	// current word is a letter
				if(Weight[Words[idx] - 'a' + 10] == -1)
				{
					Weight[Words[idx] - 'a' + 10] = Base++;
				}
			idx++;
		}
		if(idx == 1)
		{
			printf("Case #%d: 1\n", Round);
			continue;
		}	// else, idx >= 2
		for(idx = 0; idx < 36; idx++)	// swap the weight
		{
			if(Weight[idx] == 1)
			{
				Weight[idx] = 0;
				continue;
			}
			if(Weight[idx] == 0)
			{
				Weight[idx] = 1;
				continue;
			}
		}
		idx = 0;
		Base = (Base < 2) ? 2 : Base;
		while (Words[idx] != 0)
		{
			SUM = SUM * Base;
			int curPos = ((Words[idx] <= '9') ? Words[idx] - '0' : Words[idx] - 'a' + 10);
			SUM = SUM + Weight[curPos];
			idx++;
		}
		printf("Case #%d: %lld\n", Round, SUM);
	}
    return 0;
}

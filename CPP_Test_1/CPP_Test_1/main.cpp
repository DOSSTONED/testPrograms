#include <iostream>
#include <ctime>
#include <cmath>
using namespace std;

float giveRand();
const int Total = 200000;
float Q_rsqrt( float number );

int main(int argc, char* argv[])
{
	clock_t start,finish;
	float arrays[Total] = {0};
	float duration = 0, tmp = 0, totaltime = 0;
	int i = 0, j = 0, repeatedTimes = 500;

	srand(1);
	j = 0;
	while(j < repeatedTimes)
	{
		for(i = 0; i < Total; i++)
		{
			arrays[i] = giveRand() / 100000.0;
		}


		start = clock();
		for(i = 0; i < Total; i++)
		{
			tmp = sqrt(arrays[i]);
			//cout<<tmp<<endl;
		}
		finish = clock();
		
		duration = (float)(finish - start) / CLOCKS_PER_SEC;
		totaltime += duration;

		//cout<<duration<<"\t";
		j++;
	}

	cout<<endl<<"normal sqrt: "<<(totaltime )<<endl;		//   / repeatedTimes

	srand(1);
	j = 0;
	while(j < repeatedTimes)
	{
		for(i = 0; i < Total; i++)
		{
			arrays[i] = giveRand() / 100000.0;
		}


		start = clock();
		for(i = 0; i < Total; i++)
		{
			tmp = Q_rsqrt(arrays[i]);
			//cout<<tmp<<endl;
		}
		finish = clock();
		
		duration = (float)(finish - start) / CLOCKS_PER_SEC;
		totaltime += duration;

		//cout<<duration<<"\t";
		j++;
	}

	cout<<endl<<"Q_rsqrt sqrt: "<<(totaltime )<<endl;		//   / repeatedTimes


	system("pause");

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


float giveRand()
{
	if(RAND_MAX == 0x7fff)
		return (float)rand() * RAND_MAX + rand();
	else
		return (float)rand();
}
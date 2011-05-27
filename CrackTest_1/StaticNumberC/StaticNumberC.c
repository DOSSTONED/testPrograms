// StaticNumberC.c : Defines the entry point for the console application.
//

#include "stdio.h"

//using namespace std;

const static int c = 65754;
const int a = 4354;
static int b = 0;


void main()
{
	char str[100];
	printf("the const address is: %p\r\n", &a);
	printf("the static address is: %p\r\n", &b);
	printf("the const static address is: %p\r\n", &c);
	while(1)
	{
		printf("a = %d\r\n", a);
		scanf("%s", str);
		if(str[0] == 'c')
			break;
	}
	//return 0;
}

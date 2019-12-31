#include <stdio.h>
#include <windows.h>

DWORD WINAPI ThreadProc1(
						 LPVOID lpParameter
						 )
{
	printf("New thread 1.\n");
	return 0;
}


DWORD WINAPI ThreadProc2(
						 LPVOID lpParameter
						 )
{
	printf("New thread 2.\n");
	return 0;
}

int main()
{
	HANDLE newthread = CreateThread(NULL,0,ThreadProc1,0,0,NULL);

	Sleep(100);
	//flushall();
	printf("From main thread.\n");

	//system("pause");
	return 0;
}
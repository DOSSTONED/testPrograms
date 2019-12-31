// NK1053.cpp : 定义控制台应用程序的入口点。
//


#include "stdafx.h"
#include<iostream>
using namespace std;
const int Nmax=32769;
int prime[4000]={0x7fffffff};//3512 in total
int a[Nmax]={0};
int _tmain(int argc, _TCHAR* argv[])
{
	int n,i,STATUS=0,j;
	for(n=2;n<Nmax;n++)
	{
		STATUS=1;
		i=0;
		if(prime[0]==0)prime[0]=2;
		while(n>prime[i]&&prime[i])
		{
			if(!(n%prime[i]))
			{
				STATUS=0;
				break;
			}
			i++;
		}
		if(STATUS)
			prime[i]=n;
	}
	// Set primes;
	for(i=0;i<3513;i++)
		for(j=i;j<3513;j++)
			if(prime[i]+prime[j]<Nmax)
				a[prime[i]+prime[j]]++;

	while(cin>>n&&n)
		cout<<a[n]<<endl;
	cout<<endl;
	return 0;
}


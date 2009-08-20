// NK1249.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<iostream>
using namespace std;
int prime[7000]={0x7fffffff};//6542 in total
int rprime(int n,int p)
{
	if(n%p==0)return p;
	else
	{
		int i=0;
		while(prime[i]<=p)
			i++;
		while(prime[i]<=n)
			if(n%prime[i])
				i++;
			else
				return prime[i];
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	int n,i,STATUS=0;
	for(n=2;n<65536;n++)
	{STATUS=1;
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
	cin>>n;
	while(cin>>n)
	{
		if(n==rprime(n,2))cout<<n<<endl;
		else
		{
			cout<<rprime(n,2);n=n/rprime(n,2);
			while(n!=rprime(n,2))
			{
				
				cout<<"*"<<rprime(n,2);n=n/rprime(n,2);
			}
			cout<<"*"<<n<<endl;
		}
	}
	return 0;
}


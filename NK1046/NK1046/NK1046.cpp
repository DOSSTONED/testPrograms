// NK1046.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;
int a[81][81]={0};

int f(int n,int max)
{
	if(a[n][max]==0)
	{
		if(n==1||max==1)
		{
			return 1;

		}
		if(n<=max)
		{
			return f(n,n-1)+1;
		}	
		else
		{
			return f(n-max,max)+f(n,max-1);
		}
	}
	else return a[n][max];

	//if(a[n][max]==0)
	//a[n][max]=temp;
}



int _tmain(int argc, _TCHAR* argv[])
{
	int i,n,j;
	for(i=1;i<81;i++)
		for(j=1;j<=i;j++)
			a[i][j]=f(i,j);
	cout<<"finish";
	while(cin>>n)
	{
		
		a[n][n]=f(n,n);
		cout<<a[n][n]<<endl;
	}
	return 0;
}


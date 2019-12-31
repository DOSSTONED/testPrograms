// NK1534.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int s,t,w;
	cin>>s>>t>>w;

	/*其中s为所使用的最小的字母的序号
	t为所使用的最大的字母的序号
	w为数字的位数
	这3个数满足：1≤s≤26, 2≤w≤t-s
	*/
	char a[26]={0},temp[26];
	int i,j=5,k,l=0;
	cin>>temp;
	for(i=0;i<=t-s;i++)
	{
		a[int(temp[i])-97]=1;
	}
	while(j>0)
	{
		i=t;
		for(k=t-1;k>=s;k--)
		{
			if(k==s && a[k-1]==0)
				return 0;
			if(a[k]==1){i--;}
			if(a[k]==0 && a[k-1]==1)
			{
				a[k-1]=0;
				a[k]=1;
				for(l=0;l<t-i;l++)
				{
					a[i+l]=0;
					a[l+k+1]=1;
				}
				break;
			}
			
		}
		for(i=s-1;i<t;i++)
		{
			if(a[i])
				cout<<char(i+97);

		}
		cout<<endl;
		j--;
	}
	system("pause");
	return 0;
}


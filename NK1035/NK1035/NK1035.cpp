// NK1035.cpp : 定义控制台应用程序的入口点。
// 未完成

#include "stdafx.h"
#include<iostream>
#include<string>
using namespace std;
string s[10000];


int _tmain(int argc, _TCHAR* argv[])
{
	string tmp;
	int n,i,j=0,k=0;
	cin>>n;
	for(i=0;i<n;i++)
	{
		cin>>tmp;
		_strlwr_s(tmp,tmp.size());
		cout<<tmp<<endl;
		if(tmp.size()==25)
		{
			for(k=0;k<25;k++)
				if(tmp[k]>0x2f&&tmp[k]<0x3a)
					break;
		}
		else
		{
			s[j]=tmp;
		}
	}
	return 0;
}


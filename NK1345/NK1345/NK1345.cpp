// NK1345.cpp : 定义控制台应用程序的入口点。
// 

#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int n,i,l,j,c(0);
	string s("");
	while(cin>>n && n)
	{
		cin>>s;
		for(i=0;i<n;i++)
		{
			cout<<s[i];
			c=i;
			for(j=0;j<s.length()/n;j++)
			{
				if(j%2)
					c+=(i*2+1);
				else
					c+=(2*(n-i)-1);
				if(c>=s.length())break;
				cout<<s[c];
			}
		}
		cout<<endl;
	}
	return 0;
}

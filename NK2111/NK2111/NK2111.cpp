// NK2111.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	char article[256]={0}, input[256];
	int inlength=0;
	
	scanf("%[^\n]",&input);
	int n=-1,place=0,cur=0,max=0;
	while(input[cur])
	{
		cin>>n;
		int i;
		for(i=0;i<n;i++)
		{
			cin>>place;
			article[place]=input[cur];
			if(max<place)
				max=place;
		}
		cur++;
	}
	article[max+1]=0;
	cout<<article<<endl;
	return 0;
}


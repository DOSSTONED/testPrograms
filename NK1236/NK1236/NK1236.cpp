// NK1236.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include<iostream>
#include<cmath>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int a,b,tmp;
	while(cin>>a>>b&&(a||b))
	{
		tmp=(int(pow(double(a%9),double(b%8)))%9==0)?9:int(pow(double(a%9),double(b%8)))%9;
		cout<<tmp<<endl;
	}
	return 0;
}


// Test_cpp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

struct S {
	int i;
	int * p;
};




int _tmain(int argc, _TCHAR* argv[])
{
	/*S s;
	int *p =&s.i;
	p[0]=4;
	p[1]=3;
	s.p=p;
	s.p[1]=1;
	s.p[10]=2;*/

	S s;	
	int *p =&s.i;	
//	p[0]=4;	
//	p[1]=3;	
//	p[2]=20;
	s.p=p;
	s.i = 18;
//	s.p[1]=1;	
	s.p[3]=2;
	return 0;
}


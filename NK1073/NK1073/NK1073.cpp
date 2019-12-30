// NK1073.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
//#include <cmath>

using namespace std;

int GetRow(int a)
{
	int i=0;
	while( (a+1>i*(i+1)/2) || (a+1<=i*(i-1)/2) )
	{
		i++;
	}
	return i-1;
}



int _tmain(int argc, _TCHAR* argv[])
{
	int Rows;
	cin>>Rows;
	int *Tri=new int[(Rows+1)*Rows/2];
	int i;
	for(i=0;i<(Rows+1)*Rows/2;i++)
		cin>>Tri[i];


	int j,CurrentRow,CurrentPlace;
	for(j=(Rows-1)*Rows/2-1;j>=0;j--)
	{
		CurrentRow=GetRow(j);
		CurrentPlace=j-CurrentRow*(CurrentRow+1)/2;
		if( Tri[ (CurrentRow+1)*(CurrentRow+2)/2 + CurrentPlace ] > Tri[ (CurrentRow+1)*(CurrentRow+2)/2 + CurrentPlace + 1 ] )
		{
			Tri[j] += Tri[ (CurrentRow+1)*(CurrentRow+2)/2 + CurrentPlace ];
		}
		else
		{
			Tri[j] += Tri[ (CurrentRow+1)*(CurrentRow+2)/2 + CurrentPlace + 1 ];
		}
	}
	//cin>>Tri[0];
	cout<<Tri[0]<<endl;
	delete []Tri;
	return 0;
}


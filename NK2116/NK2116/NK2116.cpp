// NK2116.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int n,m,a[4],b[4],temp[4],order[4];
	cin >> n >> m;
	cin >> a[0] >> a[1] >> a[2] >> a[3];
	order[0] = a[0] % m;
	order[1] = a[1] % m;
	order[2] = a[2] % m;
	order[3] = a[3] % m;
	int i;
	for (i = 0; i < 4; ++ i) a[i] %= m;
	b[0] = b[3] = 1;
	b[1] = b[2] = 0;
	while(n > 0)
	{
		if(n % 2 ==0)
		{
			n = n / 2;
			temp[0] = order[0] * order[0] + order[1] * order[2];
			temp[1] = order[0] * order[1] + order[1] * order[3];
			temp[2] = order[2] * order[0] + order[3] * order[2];
			temp[3] = order[2] * order[1] + order[3] * order[3];
			order[0] = temp[0] % m;
			order[1] = temp[1] % m;
			order[2] = temp[2] % m;
			order[3] = temp[3] % m;
		}
		else
		{
			n = n - 1;
			temp[0] = b[0] * order[0] + b[1] * order[2];
			temp[1] = b[0] * order[1] + b[1] * order[3];
			temp[2] = b[2] * order[0] + b[3] * order[2];
			temp[3] = b[2] * order[1] + b[3] * order[3];
			b[0] = temp[0] % m;
			b[1] = temp[1] % m;
			b[2] = temp[2] % m;
			b[3] = temp[3] % m;
		}
	}

	cout << b[0] << ' ' << b[1] << endl;
	cout << b[2] << ' ' << b[3] << endl;
	return 0;
}


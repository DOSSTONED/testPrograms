// NK2118_2.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;


int p[40][2] = {0};
int status[40] = {-1}; // -1 for not scanned, 1 for ready to scan, 2 for scanned(fixed)
double dis[40] = {-1.0}; // -1 for unlimit
int n = 0;

bool canDirectlyReach(int p1[], int p2[])
{
	if( (p1[0] == p2[0]) && (p2[1] == p2[1]) )
		return true;
	else
	{
		int j = 0;
		for(j = 1; j <= n; j++)
		{
			if( (p[j % n + 1][1] - p[j][1]) * (p1[0] - p2[0]) == (p[j % n + 1][0] - p[j][0]) * (p1[1] - p2[1])   ) // the line is Parallel
				return true;
			else
			{
				double x, y;
				x = double( (p1[1]-p2[1])*(p[j%n+1][0]*p[j][1]-p[j][0]*p[j%n+1][1]) - (p[j][1]-p[j%n+1][1])*(p2[0]*p1[1]-p1[0]*p2[1]) ) / double( (p2[0] - p1[1]) * (p[j%n+1][1] - p[j][1]) - (p[j%n+1][0] - p[j][0]) * (p2[1] - p1[1]) );
				y = double( (p2[0]-p1[0])*(p[j%n+1][0]*p[j][1]-p[j][0]*p[j%n+1][1]) - (p[j%n+1][0]-p[j][0])*(p2[0]*p1[1]-p1[0]*p2[1]) ) / double( (p2[0] - p1[1]) * (p[j%n+1][1] - p[j][1]) - (p[j%n+1][0] - p[j][0]) * (p2[1] - p1[1]) );
				if(	
					(x-p1[0])*(x-p2[0]) < 0
					&&	(y-p1[1])*(y-p2[1]) < 0
					&&	(x-p[j][0])*(x-p[j%n+1][0]) < 0
					&&	(y-p[j][1])*(y-p[j%n+1][0]) < 0
					)
					return false;
			}
		}
	}
	return true;
}

int _tmain(int argc, _TCHAR* argv[])
{
	cin >> n;
	int i,j = 0, minindex = 0;
	for(i=1;i<=n;i++)
	{
		cin>>p[i][0]>>p[i][1];
		status[i] = -1;
		dis[i] = -1.0;
	}
	cin>>p[0][0]>>p[0][1];
	cin>>p[n+1][0]>>p[n+1][1];
	status[0] = 2;
	dis[0] = 0;
	status[n+1] = -1;
	dis[n+1] = -1;



	for(i=1;i<=n+1;i++)
	{
		if(canDirectlyReach(p[0],p[i]))
			status[i]=1;
	}

	i=1;
	while(1)
	{
		double mindis = -1;
		if(status[i]==-1)
			continue;
		if(status[i]==2)
		{
			for(j=1;j<=n+1;j++)
			{
				if(canDirectlyReach(p[i],p[j]))
					status[j]=1;
			}
		}


		int foundmin=0;
		if(status[i]==1)
		{
			for(j=0;j<=n+1;j++)
			{
				if(canDirectlyReach(p[i],p[j]))
				{
					if(status[j]==2)
					{
						if(dis[j]>=0 && mindis>=dis[j]+sqrt(double(pow(p[i][0]-p[j][0],2.0)+pow(p[j][1]-p[i][1],2.0))) )
						{
							mindis = dis[j]+sqrt(double(pow(p[i][0]-p[j][0],2.0)+pow(p[j][1]-p[i][1],2.0)));
							minindex = i;
							foundmin=1;
						}
						if(dis[j]>=0 && mindis<0 )
						{
							mindis = dis[j]+sqrt(double(pow(p[i][0]-p[j][0],2.0)+pow(p[j][1]-p[i][1],2.0)));
							minindex = i;
							foundmin=1;
						}
					}
				}
			}
		}
		if(foundmin==1)
		{
			status[minindex]=2;
			dis[minindex]=mindis;
		}




		i=i%(n+1)+1;
		int k=2;
		for(j=1;j<=n+1;j++)
		{
			if(status[j]<k)
				k=status[j];
		}
		if(k==2)
			break;
	}

	cout<<setiosflags(ios::fixed)<<setprecision(3)<<dis[n+1]<<endl;

	return 0;
}


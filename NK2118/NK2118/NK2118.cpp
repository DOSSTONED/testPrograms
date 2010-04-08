// NK2118.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <cmath>
#include <iomanip>
using namespace std;

int n=0, Nmax, Nmin;
double disKmax = 0.0, disKmin = 0.0, Kmax=-2147483647, Kmin=2147483647;
int p[40][2];

//int getDistanceSquared(int ax, int ay, int bx, int by)
//{
//	return (ax-bx)^2+(ay-by)^2;
//}

double getCosAngle(int ax, int ay, int p1x, int p1y, int p2x, int p2y)
{
	return (double)( (p1x-ax)*(p2x-ax) + (p1y-ay)*(p2y-ay) )/sqrt( (double) ((p1x-ax)*(p1x-ax)+(p1y-ay)*(p1y-ay)) * ((p2x-ax)*(p2x-ax)+(p2y-ay)*(p2y-ay)) );
}

double getK(int ax, int ay, int bx, int by)
{
	if(ax==bx)
	{
		if(ay>by)
			return -2147483647;
		else
			return 2147483647;
	}
	else
	{
		return ((double)(by-ay))/(double)(bx-ax);
	}
}

void findNextKmax(int start, int forward)
{
	int i,j=0;
	int maxlength=0;
	double max=-2147483647;
	double cosmin=2;
	for(i=1;i<=n+1;i++)
	{
		if(i==start)
			continue;
		if(forward==-1)
		{
			if(max<=getK(p[start][0],p[start][1],p[i][0],p[i][1]))
			{
				max=getK(p[start][0],p[start][1],p[i][0],p[i][1]);
				j=i;
			}
		}
		else
		{
			if(cosmin>=getCosAngle(p[start][0],p[start][1],p[i][0],p[i][1],p[forward][0],p[forward][1]))
			{
				cosmin=getCosAngle(p[start][0],p[start][1],p[i][0],p[i][1],p[forward][0],p[forward][1]);
				j=i;
				if(j==n+1)
					break;
			}
		}

	}

	disKmax+=sqrt(double( (p[start][0]-p[j][0])*(p[start][0]-p[j][0]) + (p[start][1]-p[j][1])*(p[start][1]-p[j][1]) ));
	if(j==n+1)
		return;
	findNextKmax(j,start);
}

void findNextKmin(int start, int forward)
{
	int i,j=0;
	int maxlength=0;
	double min=2147483647;
	double cosmin=2;
	for(i=1;i<=n+1;i++)
	{
		if(i==start)
			continue;
		if(i==forward)
			continue;
		if(forward==-1)
		{
			if(min>=getK(p[start][0],p[start][1],p[i][0],p[i][1]))
			{
				min=getK(p[start][0],p[start][1],p[i][0],p[i][1]);
				j=i;
			}
		}
		else
		{
			if(cosmin>=getCosAngle(p[start][0],p[start][1],p[i][0],p[i][1],p[forward][0],p[forward][1]))
			{
				cosmin=getCosAngle(p[start][0],p[start][1],p[i][0],p[i][1],p[forward][0],p[forward][1]);
				j=i;
				if(j==n+1)
					break;
			}
		}

	}

	disKmin+=sqrt(double( (p[start][0]-p[j][0])*(p[start][0]-p[j][0]) + (p[start][1]-p[j][1])*(p[start][1]-p[j][1]) ));
	if(j==n+1)
		return;
	findNextKmin(j,start);
}

int _tmain(int argc, _TCHAR* argv[])
{
	int overlayed=0;


	cin>>n;
	int i,j=0;
	for(i=1;i<=n;i++)
	{
		cin>>p[i][0]>>p[i][1];
	}
	cin>>p[0][0]>>p[0][1];
	cin>>p[n+1][0]>>p[n+1][1];
	for(i=1;i<=n;i++)
	{
		if( (p[n+1][0]==p[i][0])&&(p[n+1][1]==p[i][1]) )
		{
			overlayed++;
			break;
		}
	}
	if(overlayed>0)
	{
		for(j=i;j<=n;j++)
		{
			p[j][0]=p[j+1][0];
			p[j][1]=p[j+1][1];
		}
		n--;
		overlayed--;
	}

	for(i=1;i<=n;i++)
	{
		if( (p[0][0]==p[i][0])&&(p[0][1]==p[i][1]) )
		{
			overlayed++;
			break;
		}
	}
	if(overlayed>0)
	{
		for(j=i;j<=n;j++)
		{
			p[j][0]=p[j+1][0];
			p[j][1]=p[j+1][1];
		}
		n--;
		overlayed--;
	}
	findNextKmax(0,-1);
	findNextKmin(0,-1);
	if(disKmax>disKmin)
		disKmax=disKmin;
	cout<<setiosflags(ios::fixed)<<setprecision(3)<<disKmax<<endl;
	return 0;
}


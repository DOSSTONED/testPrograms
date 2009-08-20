// NK1752.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<iostream>
#include<vector>
using namespace std;



int _tmain(int argc, _TCHAR* argv[])
{
	int n,q;
	
	while(cin>>n)
	{
		if(n<=0)
			break;
		else
		{
			vector<int> v,t,b,e;//value and times and begins and ends
			cin>>q;
			int i,j,tmp,tmp1,max;
			for(i=0;i<n;i++)
			{
				cin>>tmp;
				if(v.empty())
				{
					v.push_back(tmp);
					t.push_back(1);
				}
				else
				{
					if(v[v.size()-1]==tmp)
						t[v.size()-1]++;
					else
					{
						v.push_back(tmp);
						t.push_back(1);
					}
				}
			}
			//input end//output begin
			for(i=0;i<q;i++)
			{
				cin>>tmp>>tmp1;
				max=0;
				j=0;
				while(j<v.size()&&v[j]<tmp)
				{
					j++;
				}
				while(j<v.size()&&v[j]<=tmp1)
				{
					max=max>t[j]?max:t[j];
					j++;
				}
				cout<<max<<endl;
			}
			
			
			
		}
	}
	system("pause");
	return 0;
}


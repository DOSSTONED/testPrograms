// NK1777.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <vector>
#include <stack>
using namespace std;
vector<int> relation[10010];
vector<int>  relationreverse[10010];
bool  visited[10010];
stack<int> Ss; // indicates the relationship graph
int fa[10010];// might be floor-level
int d[10010]={0};
int size[10010]={0};// indicates how many groups can be divided. each group is strong-communicative. size[i] represents the i-th group has size[i] members;
int N,M;


void dfs(int cownumber)
{
	visited[cownumber]=1;
	int i;
	for(i=0;i<relation[cownumber].size();i++)
		if(!visited[relation[cownumber][i]])
			dfs(relation[cownumber][i]);
	Ss.push(cownumber);
}

void dfs(int cownumber, int Fa)
{
	fa[cownumber]=Fa;
	visited[cownumber]=1;
	size[Fa]++;
	int i;
	for(i=0;i<relationreverse[cownumber].size();i++)
		if(!visited[ relationreverse[cownumber][i] ])
			dfs( relationreverse[cownumber][i] , Fa);
}

int _tmain(int argc, _TCHAR* argv[])
{
	cin>>N>>M;
	int i;
	for(i=0;i<M;i++)
	{
		int a,b;//relation : a thinks b is popluar
		cin>>a>>b;
		relation[a].push_back(b);
		relationreverse[b].push_back(a);
	}

	memset(visited,0,sizeof(visited));
	for(i=1;i<=N;i++)
		if (!visited[i])
			dfs(i);// get the graph by dfs


	memset(visited,0,sizeof(visited));
	int s=0;
	for(;!Ss.empty();Ss.pop())
		if(!visited[ Ss.top() ] )
			dfs( Ss.top() ,++s);// group all the cows

	for(i=1;i<=N;i++)
	{
		for(int j=0;j<relationreverse[i].size();j++)
			if(fa[i]!=fa[ relationreverse[i][j] ])
				d[ fa[ relationreverse[i][j] ] ]++;
	}
	d[0]=0;
	for(i=1;i<=s;i++)
	{
		if(!d[i])
		{
			if(d[0])
			{
				cout<<"0\n";
				return 0;
			}
			else
				d[0]=size[i];
		}
	}
	cout<<d[0]<<endl;

	system("pause");
	return 0;
}


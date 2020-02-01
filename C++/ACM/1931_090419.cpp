#include <iostream>
#include <vector>

using namespace std;

class DOS1;



class DOS
{
public:
	DOS()
	{
		U=0;K=0;L=0;
	}
	int set()
	{
		cin>>U;
		cin>>L;
		cin>>K;
		return 0;
	}
	/*int returnX(int i,DOS a)
	{
	if( (a.U - a.K * i) >=a.L )
	{
	return (a.U - a.K * i);
	}
	else{
	return -1;}
}*/
	int U;
	int L;
	int K;
};

int main()
{
	
	int M,i=0,j=0,k=0,num=0,stats=0,temp=0;
	cin>>M;
	DOS *a;
	a = new DOS [M];
	for(i=0;i<M;i++)
	{
		a[i].set ();
	}
	
	vector<int> total;
	for(i=0;i<M;i++)
	{
		for(j=0; ((a[i].U - a[i].K * j) >=a[i].L) ;j++)
		{
			temp=(a[i].U - a[i].K * j);
			num=total.size ();
			stats=0;
			for(k=0;k<num;k++)
			{
				if(temp==total[k])
				{
					total[k]=-1;stats=1;
				}
			}
			if(stats==0)
			{
				total.push_back(temp);
			}
			
		}
	}
	num=total.size ();
	for(i=0;i<num;i++)
	{
		if(total[i]>=0)
		{
			cout<<total[i]<<endl;
			return 0;
		}
	}
	
	cout<<"NONE"<<endl;
	
	return 0;
}

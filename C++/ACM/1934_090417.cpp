#include <iostream>

using namespace std;

int sort(int *in,int *rin,int n)
{
	int k=0;
	for(k=0;k<n;k++)
	{
		int j=0;
			for(j=k+1;j<n;j++)
			{
				if(in[j]<in[k])
				{
					int temp;
					temp=in[k];
					in[k]=in[j];
					in[j]=temp;
					
					temp=rin[k];
					rin[k]=rin[j];
					rin[j]=temp;
				}

			}
	}
	return 0;
}

int main()
{
	int No=0,max=0,i=0,sum=0;
	cin>>No;
	int *width,*r;
	width = new int [No];
	r = new int [No];
	for(i=0;i<No;i++)
	{
		cin>>width[i];
		cin>>r[i];
	}

	sort(r,width,No);

	for(i=0;i<No;i++)
	{
		sum=sum+width[i];
		if(sum<=r[i])
		{
			max++;
			cout<<width[i]<<endl;
		}
		else
		{
			sum=sum-width[i];
		}

	}

	cout<<max;
	return 0;
}

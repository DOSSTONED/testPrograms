#include<iostream>
#include<math.h>
using namespace std;

int print(int fromn,int ton)
{
	if(abs(ton-fromn)==1)
		cout<<fromn<<endl<<ton<<endl;
	else
	{
		print(fromn,(fromn+ton)/2);
		print(ton,(fromn+ton)/2+1);
	}
	return 0;
}

int main(int argc,char* argv[])
{
	int N;
	cin>>N;
	print(0, pow(2,N)-1);
	return 0;
}
